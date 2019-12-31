# Enterprise Information Managerment

## 目录

1. API网关
   - [Ocelot](#Ocelot)
2. 服务注册中心
   - [Consul](#Consul)
3. 认证鉴权
   - [IdentityServer](#IdentityServer)
4. 网络服务
   - [DnsClient.NET](#DnsClient.NET)
5. 运维监控
   - [AppMetrics](#AppMetrics)

## API网关

### Ocelot

通过 WebHostBuilder 将 json 配置文件添加进 asp.net core 的配置：

```C#
public static IWebHost BuildWebHost(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration( (hostingContext,builder) => {
            builder
            .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            .AddJsonFile("Ocelot.json");
        })
        .UseStartup<Startup>()
        .Build();
```

添加依赖注入和中间件：

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddOcelot();
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseOcelot().Wait();
}
```

<b style="color:red">注意：</b>hostname 和 ip 要统一，混合用会出问题！

## 服务注册中心

### Consul

```sh
# 启动
consul.exe agent -dev # 本地模式，将会使用127.0.0.1 的ip地址
consul.exe agent -dev -client 192.168.xx.xx  
# consul通讯默认端口为8500
```

访问：[http://localhost:8500/](http://localhost:8500/) 查看服务

## 部署

windows部署准备：ASP.NET Core Runtime

```sh
# Unix:
ASPNETCORE_URLS="https://*:5123" dotnet run

# Windows PowerShell:
$env:ASPNETCORE_URLS="https://*:5123" ; dotnet run

# Windows CMD (note: no quotes):
SET ASPNETCORE_URLS=https://*:5123 && dotnet run
```

在VS中调试的时候有很多修改Web应用运行端口的方法。但是在开发、调试微服务应用的时候可能需要同时在不同端口上开启多个服务器的实例,因此下面主要看看如何通过命令行指定Web应用的端口（默认5000）

可以通过设置临时环境变量ASPNETCORE URLS来改变默认的端口、域名，也就是执行 dotnet xxx.dll之前执行 `set ASPNETCORE_URLS=http://127.0.0.1:5001` 来设置环境变量。

如果需要在程序中读取端口、域名（后续服务治理会用到），用ASPNETCORE URLS环境变量就不太方便，可以自定义配置文件，自己读取设置。

[查看代码](./src/WebApplication1/Program.cs)

然后启动的时候：

```sh
dotnet xxx.dll --ip 127.0.0.1 --port 8889
```

`.Net Core` 因为跨平台，所以可以不依赖于IIS运行。可以用 `.Net Core` 内置的 kestrel 服务器运行网站，当然真正面对终端用户访问的时候一般通过 Nginx 等做反向代理。

### Ocelot网关

微服务没有网关的几点不足：

1）对于在微服务体系中、和Consul通讯的微服务来讲，使用服务名即可访问。但是对于手机、web端等外部访问者仍然需要和N多服务器交互，需要记忆他们的服务器地址、端口号等。一旦内部发生修改，很麻烦，而且有时候内部服务器是不希望外界直接访问的。

2）各个业务系统的人无法自由的维护自己负责的服务器；

3）现有的微服务都是“我家大门常打开”，没有做权限校验。如果把权限校验代码写到每个微服务上，那么开发工作量太大。

4）很难做限流、收费等。

ocelot 中文文档：[https://blog.csdn.net/sD7O95O/article/details/79623654](https://blog.csdn.net/sD7O95O/article/details/79623654)

资料：[http://www.csharpkit.com/apigateway.html](http://www.csharpkit.com/apigateway.html)

腾讯.Net大队长“张善友”是项目主力开发人员之一。

### 服务治理发现

- [consul](https://www.consul.io/)：Consul是注册中心，服务提供者、服务消费者等都要注册到Consul中，这样就可以实现服务提供者、服务消费者的隔离
- Eureka
- Zookeeper

**consul：**

```sh
consul.exe agent -dev
```

这是开发环境测试，生产环境要建集群，要至少一台Server，多台Agent consul

监控页面：[http://127.0.0.1:8500/consult](http://127.0.0.1:8500/consult)

主要做三件事：提供服务到ip地址的注册，提供服务到ip地址列表的查询，对提供服务方的健康检查(HealthCheck)。

**.Net Core连接Consul：**

1、安装 Consul nuget 包

```sh
Install-Package Consul
```

2、让Rest服务注册到Consul中

```C#
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
  // ...

  // 部署到不同服务器的时候不能写成127.0.0.1或者0.0.0.0，因为这是让服务消费者调用的地址
  string ip = Configuration["ip"];
  int port = int.Parse(Configuration["port"]);
  // 向consul注册服务
  ConsulClient client = new ConsulClient(ConfigurationOverview);
  Task<WriteResult> result = client.Agent.ServiceRegister(new AgentServiceRegistration()
  {
    // 服务编号，不能重复，用Guid最简单
    ID = "apiservice1" + Guid.NewGuid(),
    // 服务的名字
    Name = "apiservice1",
    // 我的ip地址（可以被其他应用访问的地址，本地测试可以用127.0.0.1，机房环境中一定要写自己的内网ip地址）
    Address = ip,
    // 我的端口
    Port = port,
    Check = new AgentServiceCheck()
    {
      // 服务停止多久后反注册
      DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
      // 健康检查时间间隔，或者称为心跳间隔
      Interval = TimeSpan.FromSeconds(10),
      // 健康检查地址
      HTTP = $"http://{ip}:{port}/api/health",
      Timeout = TimeSpan.FromSeconds(5)
    }
  });
}

private static void ConfigurationOverview(ConsulClientConfiguration obj)
{
  obj.Address = new Uri("http://127.0.0.1:8500");
  obj.Datacenter = "dc1";
}
```

注意不同实例一定要用不同的Id，即使是相同服务的不同实例也要用不同的id，上面的代码用Guid做Id，确保不重复。相同的服务用相同的Name。Address、Port是供服务消费者访问的服务器地址（或者IP地址）及端口号。Check则是做服务健康检查的。

在注册服务的时候还可以通过AgentServiceRegistration的Tags属性设置额外的标签。

通过命令行启动两个实例：

```sh
dotnet xxx.dll --ip 127.0.0.1 --port 5001
dotnet xxx.dll --ip 127.0.0.1 --port 5002
```

应用停止的时候反注册。

**熔断降级：**

熔断器如同电力过载保护器，它可以实现快速失败。

如果它在一段时间内侦测到许多类似的错误，会强迫其以后的多个调用快速失败，不再访问远程服务器，从而防止应用程序不断地尝试执行可能会失败的操作，使得应用程序继续执行而不用等待修正错误，或者浪费时间去等到长时间的超时产生。

降级的目的是当某个服务提供者发生故障的时候，向调用方返回一个错误响应或者替代响应。举例子：如视频播放器请求playsafe的替代方案；加载内容评论时如果出错，则以缓存中加载或者显示“评论暂时不可用”。

.Net Core中有一个被.Net基金会认可的库Polly，可以用来简化熔断降级的处理。主要功能：重试(Retry)、断路器(Circuit-breaker)、超时检测(Timeout)、缓存(Cache)、失败处理(FallBack)。

官网：[https://github.com/App-vNext/Polly](https://github.com/App-vNext/Polly)

介绍文章：[https://www.cnblogs.com/CreateMyself/p/7589397.html](https://www.cnblogs.com/CreateMyself/p/7589397.html)

```sh
Install-Package Polly -Version xxx
```

## 认证鉴权

### IdentityServer



### OIDC

- OIDC：Open ID Connect

  OIDC是基于OAuth2的，OAuth2只解决了授权的问题，没有解决认证问题，而OpenID是个认证协议，所以二者结合就是OIDC。

- OIDC= OAuth2 + OpenID

  OIDC在OAuth2的access_token的基础上增加了身份认证信息， 通过公钥私钥配合校验获取身份等其他信息—– 即idToken

## 网络服务

### DnsClient.NET

[DnsClient.NET](https://github.com/MichaCo/DnsClient.NET)是一个简单但功能强大且高性能的开源库，可用于.NET Framework进行DNS查找。

[更多说明](http://dnsclient.michaco.net/)



# 运维监控

## AppMetrics

**App.Metrics **是一款开源的支持.NET Core的监控插件，它还可以支持跑在.NET Framework上的应用程序（版本 >= 4.5.2）。官方文档地址：https://www.app-metrics.io/

**InfluxDB** 是一款开源的分布式时序、时间和指标数据库，使用go语言编写，无需外部依赖。官网地址：https://portal.influxdata.com

**Grafana** 是一个可视化面板（Dashboard），有着非常漂亮的图表和布局展示，功能齐全的度量仪表盘和图形编辑器，支持Graphite、zabbix、InfluxDB、Prometheus和OpenTSDB作为数据源。官网地址：https://grafana.com/

### InfluxDB的安装与配置

#### Linux下的安装

```sh
docker pull tutm/influxdb
```

#### Windows下的安装

1. 下载Windows版本（64位），下载地址：https://dl.influxdata.com/influxdb/releases/influxdb-1.7.1_windows_amd64.zip

2. 解压之后放到你想要放置的位置，然后编辑influxdb.conf配置文件：（因为其默认配置是针对Linux的）

   ```conf
   [meta]
   # Where the metadata/raft database is stored
   dir = "C:/APM/influxdb/meta"
   
   [data]
   # The directory where the TSM storage engine stores TSM files.
   dir = "C:/APM/influxdb/data"
   
   # The directory where the TSM storage engine stores WAL files.
   wal-dir = "C:/APM/influxdb/wal"
   ```

3. 进入cmd，以命令模式运行influxd：

   ```sh
   influxd -config influxdb.conf
   ```

4. 然后新开一个cmd，连上influxdb

   ```sh
   influx -host 127.0.0.1 -port 8086 -username "admin" -password "123456"
   ```

   然后创建一个database

   ```sql
   create database "LeadChinaPMAppMetricsDev";
   show databases;
   ```

   关于influxdb的更多命令，可以浏览参考资料关于influxdb的[InfluxDB入门教程](https://blog.csdn.net/zzti_erlie/article/details/76422871)。

### Grafana的安装与配置

#### Windows下的安装

1. 下载Windows版本（64位），下载地址：https://s3-us-west-2.amazonaws.com/grafana-releases/release/grafana-5.2.2.windows-amd64.zip

 2. 直接运行grafana-server.exe即可，默认绑定3000端口号。

 3. 浏览器打开serverip:3000，使用默认账号admin/admin（账号密码都是admin）登录

 4. 为InfluxDB添加数据源

    在Configuration中点击Add data source按钮，进入如下图所示的添加界面，输入你安装的InfluxDB数据库信息

    ![image-20191226190522040](D:\EIM\resource\image-20191226190522040.png)

　5. 为Grafana添加InfluxDB的Dashboard仪表盘的JSON文件

    这里有两种方式：一种是直接给URL=>https://grafana.com/dashboards/2125，另一种是我们手动下载这个URL的JSON，然后把JSON粘贴过来。

　6. 导入之后，查看这个Dashboard：

    ![image-20191226190737121](D:\EIM\resource\image-20191226190737121.png)


### AppMetrics的使用与API网关集成

**注意**：以下的配置和代码都只是在API网关（Ocelot）中做的，至于具体API服务中不需要做配置。

#### 1. 安装App.Metrics

通过NuGet安装以下几个package：

![image-20191226191908635](D:\EIM\resource\image-20191226191908635.png)

#### 2. 集成API网关

1. 添加配置文件关于InfluxDB的部分

   ```json
   "AppMetrics": {
       "IsOpen": true,
       "DatabaseName": "AppMetricsDB",
       "ConnectionString": "http://192.168.80.71:8086",
       "UserName": "admin",
       "Password": "edisonchou",
       "App": "MSAD",
       "Env": "Development"
     }
   ```

2. 修改StartUp类，注入AppMetrics并设置

   ```C#
   public void ConfigureServices(IServiceCollection services)
       {
          ......
   
           // AppMetrics
           bool isOpenMetrics = Convert.ToBoolean(Configuration["AppMetrics:IsOpen"]);
           if (isOpenMetrics)
           {
               string database = Configuration["AppMetrics:DatabaseName"];
               string connStr = Configuration["AppMetrics:ConnectionString"];
               string app = Configuration["AppMetrics:App"];
               string env = Configuration["AppMetrics:Env"];
               string username = Configuration["AppMetrics:UserName"];
               string password = Configuration["AppMetrics:Password"];
   
               var uri = new Uri(connStr);
               var metrics = AppMetrics.CreateDefaultBuilder().Configuration.Configure(options =>
               {
                   options.AddAppTag(app);
                   options.AddEnvTag(env);
               }).Report.ToInfluxDb(options =>
               {
                   options.InfluxDb.BaseUri = uri;
                   options.InfluxDb.Database = database;
                   options.InfluxDb.UserName = username;
                   options.InfluxDb.Password = password;
                   options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                   options.HttpPolicy.FailuresBeforeBackoff = 5;
                   options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                   options.FlushInterval = TimeSpan.FromSeconds(5);
               }).Build();
   
               services.AddMetrics(metrics);
               services.AddMetricsReportScheduler();
               services.AddMetricsTrackingMiddleware();
               services.AddMetricsEndpoints();
           }
       }
   
       // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
       {
           ......
   
           // AppMetrics
           bool isOpenMetrics = Convert.ToBoolean(Configuration["AppMetrics:IsOpen"]);
           if (isOpenMetrics)
           {
               app.UseMetricsAllEndpoints();
               app.UseMetricsAllMiddleware();
           }
   
           // Ocelot
           app.UseOcelot().Wait();
       }
   ```

### 运行效果展示

这时我们把API网关、ClientService和ProductService同时启动起来，然后通过浏览器不停的请求这两个服务的某个API接口。请求一段时间后，我们进入Grafana的Dashboard来查看：

![image-20191226192137472](C:\Users\Administrator.USER-20191011HE\AppData\Roaming\Typora\typora-user-images\image-20191226192137472.png)

可以看到，原本空荡荡的仪表盘已经满血复活，各种曲线和数据出来了。这里Error有数据是因为我的程序中有bug，出现了异常。

我们也可以设置Grafana的Alerting设置，让其可以为我们发送告警邮件（可以选择Include Image），当然你事先得改一下Grafana的配置文件，贴上你的SMTP服务器和账号密码信息。

![image-20191226192249315](D:\EIM\resource\image-20191226192249315.png)

### 参考资料

（1）顾镇印，《ASP.NET Core之跨平台的实时监控》

（2）老衲平僧，《InfluxDB+Grafana+AppMetrics监控系统》

（3）landon，《.NET Core 2.0+InfluxDB+Grafana+AppMetrics实现跨平台的实时性能监控》

（4）focus-lei，《.net core使用App.Metrics+InfluxDB+Grafana进行APM监控》

（5）桂素伟，《Ocelot监控》

（6）仰望星空脚踏实地，《InfluxDB入门教程》

（7）JackyRoc，《InfluxDB使用说明》

（8）InfluxDB官方文档：http://docs.influxdata.com/influxdb/v1.6/

## 缓存

### 本地缓存

### 客户端缓存

### 服务端缓存

- [Cache帮助类](./Framework/Com.Colin.Lib/CacheHelper.cs)，适用于 WebForm 和 MVC
- Remoting Singleton缓存
- Memcached
- Redis
- Tait

## 消息队列

- RabbitMQ
- RocketMQ
  - [测试类](./Core/Com.Colin.Demo/RocketMQClientTest.cs)
  - [rocketmq-client-dotnet](https://github.com/gaufung/rocketmq-client-dotnet), A rocketmq client in dot net core
  - [一个C#写的开源分布式消息队列(类RocketMQ)](https://blog.csdn.net/weixin_34267123/article/details/92072271)

## 分布式框架

### NServiceBus

同时启动 ClientConsole, Sales, Billing, Shipping 项目进行测试

## 领域驱动设计

请查看[CDF](./CDF.sln)项目和[ABP](./ABP.sln)项目

## 微服务系统

## 企业信息化系统

- [Aries](https://github.com/cyq1162/Aries)：.NET Develop Framework（适合场景：业务系统、信息系统、管理系统、ERP，含工作流，支持.NET Core）
- [sfgantt-gantt-chart](https://github.com/liaoqingmiao/sfgantt-gantt-chart)：向日葵 Gantt 是当前B/S 系统开发中先进的甘特图解决方案，它采用与Google maps相同的AJAX技术，实现了与Ms Project 甘特图一致的界面和功能，可广泛应用于 ERP 系统、MES系统、项目管理系统或其它的资源时间相关领域。
- [eBestMall](https://github.com/hongyukeji/eBestMall)：eBestMall是国内电子商务系统及服务解决方案新创品牌。为传统企业及创业者提供零售网店系统、网上商城系统、分销系统、B2B2C商城系统、微信分销系统、行业ERP等产品和解决方案。 http://www.ebestmall.com/
- [jeeplatform](https://github.com/u014427391/jeeplatform)

### OA系统

- [OPMS](https://github.com/lock-upme/OPMS)
- [perManaGement](https://github.com/mojooo/perManaGement)
- [FutureCRM](https://github.com/shigenwang/FutureCRM)
- [TXF_OA](https://github.com/iFuck/TXF_OA)

### ERP系统

JAVA实现：

- [production_ssm](https://github.com/megagao/production_ssm)
- [taozhaoping/ERP](https://github.com/taozhaoping/ERP)
- [open-erp](https://github.com/firebata/open-erp)

### MES系统

模仿[SharpMesSystem](http://mes.hslcommunication.cn/)实现

.NET实现：

- [fabview](https://github.com/fabview/fabview)
- [MesAoyou](https://github.com/JieChuangJia/MesAoyou)
- [MesTeam/Mes](https://github.com/MesTeam/Mes)

JAVA实现：

- [mes_1.0](https://github.com/binglin881211/mes_1.0)
- [qcadoo/mes](https://github.com/qcadoo/mes)
- [openMES](https://github.com/ming-hai/openMES)
- [industry4.0-mes](https://github.com/ricefishtech/industry4.0-mes)

前端：

- [mes-system](https://github.com/xuguangwen/mes-system)

### PLM系统
