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

```json
{
  "ReRoutes": [ // 路由
    {
      "DownstreamPathTemplate": "/api/email/{url}", // 下游服务配置
      "DownstreamScheme": "http",
      // 如果使用LoadBalancer的话这里可以填多项
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 4000
        },
        {
          "Host": "localhost",
          "Port": 5000
        }
      ],
      "UpstreamPathTemplate": "/youjian/{url}", // 上游服务配置
      "LoadBalancer": "LeastConnection",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      // 上游Host也是路由用来判断的条件之一，由客户端访问时的Host来进行区别
      "UpstreamHost": "localhost",
      // 对多个产生冲突的路由设置优先级，级别高的会被优先选择
      "Priority": 0,
      "QoSOptions": {
        "ExceptionsAllowedBeforeBreaking": 3,
        "DurationOfBreak": 10,
        "TimeoutValue": 5000
      }
    }, // 将用户的请求 /youjian/{url} 转发到 localhost:4000/api/email/{url}
    {
      "DownstreamPathTemplate": "/api/sms/{url}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 4001
        }
      ],
      "UpstreamPathTemplate": "/duanxin/{url}",
      "UpstreamHttpMethod": [ "Get", "Post" ],
      "QoSOptions": {
        "ExceptionsAllowedBeforeBreaking": 3,
        "DurationOfBreak": 10,
        "TimeoutValue": 5000
      }
    }
  ]
}
```

**万能模板：**

万能模板即所有请求全部转发，UpstreamPathTemplate 与 DownstreamPathTemplate 设置为 "/{url}"

万能模板的优先级最低，只要有其它的路由模板，其它的路由模板则会优先生效。

```json
{
  "ReRoutes": [ // 路由
    {
      "DownstreamPathTemplate": "/{url}",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/{url}",
      "UpstreamHttpMethod": [ "Get" ]
    }
  ]
}
```

**负载均衡：**

LoadBalancer将决定负载均衡的算法：

- LeastConnection – 将请求发往最空闲的那个服务器
- RoundRobin – 轮流发送
- NoLoadBalance – 总是发往第一个请求或者是服务发现

在负载均衡这里，我们还可以和 Consul 结合来使用服务发现。

**请求聚合：**

将多个 API 请求结果合并为一个返回。要实现请求聚合我们需要给其它参与的路由起一个 Key。

```json
{
  "ReRoutes": [
    {
      "DownstreamPathTemplate": "/",
      "UpstreamPathTemplate": "/laura",
      "UpstreamHttpMethod": ["Get"],
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51881
        }
      ],
      "Key": "Laura"
    },
    {
      "DownstreamPathTemplate": "/",
      "UpstreamPathTemplate": "/tom",
      "UpstreamHttpMethod": ["Get"],
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 51882
        }
      ],
      "Key": "Tom"
    }
  ],
  "Aggregates": [
    {
      "ReRouteKeys": ["Tom", "Laura"],
      "UpstreamPathTemplate": "/"
    }
  ]
}
```

当我们请求 / 的时候，会将 /tom 和 /laura 两个结果合并到一个 response 返回：

```json
{ "Tom": {"Age": 19}, "Laura": {"Age": 25} }
```

需要注意的是：

- 聚合服务目前只支持返回 json
- 目前只支持 Get 方式请求下游服务
- 任何下游的 response header 都会被丢弃
- 如果下游服务返回 404，聚合服务只是这个 key 的 value 为空，它不会返回 404

有一些其它的功能会在将来实现：

- 下游服务很慢的处理
- 做一些像 GraphQL 的处理对下游服务返回结果进行处理
- 404 的处理

**限流：**

对请求进行限流可以防止下游服务器因为访问过载而崩溃，这个功能就是我们的张善友张队添加进去的。非常优雅的实现，我们只需要在路由下加一些简单的配置即可以完成。

```json
"RateLimitOptions": {
  "ClientWhitelist": [],
  "EnableRateLimiting": true,
  "Period": "1s",
  "PeriodTimespan": 1,
  "Limit": 1
}
```

- ClientWihteList 白名单
- EnableRateLimiting 是否启用限流
- Period 统计时间段：1s, 5m, 1h, 1d
- PeroidTimeSpan 多少秒之后客户端可以重试
- Limit 在统计时间段内允许的最大请求数量

在 GlobalConfiguration 下我们还可以进行以下配置：

```json
"RateLimitOptions": {
  "DisableRateLimitHeaders": false,
  "QuotaExceededMessage": "Customize Tips!",
  "HttpStatusCode": 999,
  "ClientIdHeader" : "Test"
}
```

- Http头  X-Rate-Limit 和 Retry-After 是否禁用
- QuotaExceedMessage 当请求过载被截断时返回的消息
- HttpStatusCode 当请求过载被截断时返回的http status
- ClientIdHeader 用来识别客户端的请求头，默认是 ClientId

**服务质量与熔断：**

熔断的意思是停止将请求转发到下游服务。当下游服务已经出现故障的时候再请求也是功而返，并且增加下游服务器和 API 网关的负担。这个功能是用的 Pollly 来实现的，我们只需要为路由做一些简单配置即可：

```json
"QoSOptions": {
  "ExceptionsAllowedBeforeBreaking": 3,
  "DurationOfBreak": 5,
  "TimeoutValue": 5000
}
```

- ExceptionsAllowedBeforeBreaking 允许多少个异常请求
- DurationOfBreak 熔断的时间，单位为秒
- TimeoutValue 如果下游请求的处理时间超过多少则自如将请求设置为超时

**缓存：**

Ocelot 可以对下游请求结果进行缓存，目前缓存的功能还不是很强大。它主要是依赖于CacheManager 来实现的，我们只需要在路由下添加以下配置即可

```json
"FileCacheOptions": { "TtlSeconds": 15, "Region": "somename" }
```

Region 是对缓存进行的一个分区，我们可以调用 Ocelot 的 administration API 来移除某个区下面的缓存。

**认证：**

如果我们需要对下游 API 进行认证以及鉴权服务的，则首先 Ocelot 网关这里需要添加认证服务。这和我们给一个单独的 API 或者 ASP.NET Core Mvc 添加认证服务没有什么区别：

```C#
public void ConfigureServices(IServiceCollection services)
{
  var authenticationProviderKey = "TestKey";
  services.AddAuthentication().AddJwtBearer(
    authenticationProviderKey,
    x =>{});
}
```

然后在 ReRoutes 的路由模板中的 AuthenticationOptions 进行配置，只需要我们的 AuthenticationProviderKey 一致即可！

```json
"ReRoutes": [{
  "DownstreamHostAndPorts": [
    {
      "Host": "localhost",
      "Port": 51876,
    }
  ],
  "DownstreamPathTemplate": "/",
  "UpstreamPathTemplate": "/",
  "UpstreamHttpMethod": ["Post"],
  "ReRouteIsCaseSensitive": false,
  "DownstreamScheme": "http",
  "AuthenticationOptions": {
    "AuthenticationProviderKey": "TestKey",
    "AllowedScopes": []
  }
}]
```

**JWT Tokens：**

要让网关支持 JWT 的认证其实和让 API 支持 JWT Token 的认证是一样的：

```C#
public void ConfigureServices(IServiceCollection services)
{
  var authenticationProviderKey = "TestKey";
  services.AddAuthentication().AddJwtBearer(
    authenticationProviderKey,
    x =>
    {
      x.Authority = "test";
      x.Audience = "test";
    }
  );
  services.AddOcelot();
}
```

**Identity Server Bearer Tokens：**

添加 Identity Server 的认证也是一样

```C#
public void ConfigureServices(IServiceCollection services)
{
  var authenticationProviderKey = "TestKey";
  var options = o =>
    {
      o.Authority = "https://whereyouridentityserverlives.com";
      o.ApiName = "api";
      o.SupportedTokens = SupportedTokens.Both;
      o.ApiSecret = "secret";
    };

    services.AddAuthentication()
      .AddIdentityServerAuthentication(
        authenticationProviderKey, options
      );

    services.AddOcelot();
}
```

**Allowed Scopes：**

这里的 Scopes 将从当前 token 中的 claims 中来获取，我们的鉴权服务将依靠于它来实现。当前路由的下游 API 需要某个权限时，我们需要在这里声明。和 OAuth2 中的 scope 意义一致。

**鉴权：**

我们通过认证中的 AllowedScopes 拿到 claims 之后，如果要进行权限的鉴别需要添加以下配置：

```json
"RouteClaimsRequirement": {
  "UserType": "registered"
}
```

当前请求上下文的 token 中所带的 claims 如果没有 name="UserType" 并且 value="registered" 的话将无法访问下游服务。

**请求头转化：**

请求头转发分两种：转化之后传给下游和从下游接收转化之后传给客户端。在 Ocelot 的配置里面叫做 Pre  Downstream Request 和 Post Downstream Request。目前的转化只支持查找和替换。我们用到的配置主要是 UpstreamHeaderTransform 和 DownstreamHeaderTransform。

- Pre Downstream Request

  ```txt
  "Test": "http://www.bbc.co.uk/, http://ocelot.com/"
  ```

  比如我们将客户端传过来的 Header 中的 Test 值改为 `http://ocelot.com/`  之后再传给下游

  ```json
  "UpstreamHeaderTransform": {
    "Test": "http://www.bbc.co.uk/, http://ocelot.com/"
  }
  ```

- Post Downstream Request

  而我们同样可以将下游 Header 中的 Test 再转为 `http://www.bbc.co.uk/` 之后再转给客户端。

  ```json
  "DownstreamHeaderTransform": {
    "Test": "http://www.bbc.co.uk/, http://ocelot.com/"
  }
  ```

- 变量

  在请求头转化这里 Ocelot 为我们提供了两个变量：BaseUrl 和 DownstreamBaseUrl。BaseUrl 就是我们在 GlobalConfiguration 里面配置的BaseUrl，后者是下游服务的 Url。这里用 301 跳转做一个示例如何使用这两个变量。

  默认的 301 跳转，我们会返回一个 Location 的头，于是我们希望将 `http://www.bbc.co.uk` 替换为 `http://ocelot.com`，后者者网关对外的域名。

  ```json
  "DownstreamHeaderTransform": {
    "Location": "http://www.bbc.co.uk/, http://ocelot.com/"
  },
  "HttpHandlerOptions": {
    "AllowAutoRedirect": false,
  }
  ```

  我们通过 DownstreamHeaderTranfrom 将下游返回的请求头中的 Location 替换为了网关的域名，而不是下游服务的域名。所以在这里我们也可以使用 BaseUrl 来做为变量替换。

  ```json
  "DownstreamHeaderTransform": {
    "Location": "http://localhost:6773, {BaseUrl}"
  },
  "HttpHandlerOptions": {
    "AllowAutoRedirect": false,
  }
  ```

  当我们的下游服务有多个的时候，我们就没有办法找到前面的那个 `http://localhost:6773`，因为它可能是多个值。所以这里我们可以使用 DownstreamBaseUrl。

  ```json
  "DownstreamHeaderTransform": {
    "Location": "{DownstreamBaseUrl}, {BaseUrl}"
  },
  "HttpHandlerOptions": {
    "AllowAutoRedirect": false,
  }
  ```

**Claims转化：**

Claims 转化功能可以将 Claims 中的值转化到请求头、Query String、或者下游的 Claims 中，对于 Claims 的转化，比较特殊的一点是它提供了一种对字符串进行解析的方法。

举个例子，比如我们有一个 sub 的 claim。这个 claims 的 name="sub" value="usertypevalue|useridvalue"，实际上我们不会弄这么复杂的value，它是拼接来的，但是我们为了演示这个字符串解析的功能，所以使用了这么一个复杂的value。

Ocelot 为我们提供的功能分为三段，第一段是 Claims[sub]，很好理解 [] 里面是我们的 claim 的名称。第二段是 > 表示对字符串进行拆分，后面跟着拆分完之后我们要取的那个数组里面的某一个元素，用 value[index] 来表示，取第 0 位元素也可以直接用 value。第三段也是以 > 开头后面跟着我们的分隔符，在我们上面的例子分隔符是 `|`。

所以在这里如果我们要取 usertype 这个 claim 就会这样写：Claims[sub] > value[0] > |

Claim 取到之后我们如果要放到请求头、QueryString、以及 Claim 当中对应有以下三个配置：

- Claims to Claims

  ```json
  "AddClaimsToRequest": {
    "UserType": "Claims[sub] > value[0] > |",
    "UserId": "Claims[sub] > value[1] > |"
  }
  ```

- Claims to Headers

  ```json
  "AddHeadersToRequest": {
    "CustomerId": "Claims[sub] > value[1] > |"
  }
  ```

  这里我们还是用的上面那个 sub = usertypevalue|useridvalue 的 claim 来进行处理和转化。

- Claims to Query String

  ```json
  "AddQueriesToRequest": {
    "LocationId": "Claims[LocationId] > value",
  }
  ```

  这里没有进行分隔，所以直接取了value。

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
