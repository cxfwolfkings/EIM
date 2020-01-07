# Consul

## 目录

1. 安装
   - [端口表](#端口表)
   - [引导数据中心](#引导数据中心)
   - [服务器性能](#服务器性能)
2. [Glossary（词汇表）](#Glossary)

## 端口表

在运行Consul之前，您应确保可以访问以下绑定端口：

![x](./Resource/Consul端口.PNG)

- DNS接口用于解析DNS查询。
- HTTP API客户端用于与HTTP API进行对话。
- HTTPS API（可选）默认情况下处于关闭状态，但端口8501是多种工具默认使用的约定。
- gRPC API（可选）。当前，gRPC仅用于将xDS API公开给Envoy代理。默认情况下它是关闭的，但是端口8502是各种工具默认使用的约定。在-dev模式下默认为8502 。
- Serf LAN这用于处理LAN中的八卦。所有代理商都需要。
  
  服务器广域网服务器使用它来通过广域网传播到其他服务器。从Consul 0.8开始，WAN连接泛洪功能要求Serf WAN端口（TCP / UDP）在WAN和LAN接口上进行侦听。另请参阅： 领事0.8.0 CHANGELOG和GH-3058
  
  服务器RPC服务器使用它来处理来自其他代理的传入请求。

>注意，可以在**代理配置**中更改默认端口。

## 引导数据中心

代理可以在客户端或服务器模式下运行。服务器节点负责运行 [共识协议](https://www.consul.io/docs/internals/consensus.html) 并存储集群状态。客户端节点大多是无状态的，并且严重依赖于服务器节点。

在Consul群集可以开始为请求提供服务之前，必须将服务器节点选为领导者。引导是将这些初始服务器节点加入群集的过程。阅读 [架构文档](https://www.consul.io/docs/internals/architecture.html) 以了解有关Consul内部的更多信息。

建议每个数据中心总共有三到五个服务器。单台服务器部署非常气馁，因为数据丢失是在出现故障的情况在所难免。请参阅 [部署表](https://www.consul.io/docs/internals/consensus.html#deployment-table) 以获取更多详细信息。

**引导服务器：**

引导服务器的推荐方法是使用 [-bootstrap-expect](https://www.consul.io/docs/agent/options.html#_bootstrap_expect) 配置选项。此选项通知Consul服务器节点的预期数量，并在有那么多服务器可用时自动引导。为避免出现不一致和脑裂（多个服务器将其视为领导者的集群）情况，您应该为-bootstrap-expect 所有服务器指定相同的值 或完全不指定任何值。只有指定值的服务器才会尝试引导群集。

假设我们正在启动一个三服务器集群。我们可以启动 Node A，Node B 以及 Node C 各自提供的-bootstrap-expect 3标志。节点启动后，您应该在服务输出中看到警告消息。

```sh
[WARN] raft: EnableSingleNode disabled, and no known peers. Aborting election.
```

该警告表明节点期望有2个对等节点，但尚不知道。在下面，您将学习如何连接服务器，以便可以当选领导者。

**创建集群：**

您可以通过将服务器连接在一起来创建组来触发领导者选举。您可以将节点配置为自动或手动加入。

1、自动加入服务器

有多种加入服务器的选项。选择最适合您的环境和特定用例的方法。

- 使用 [-join](https://www.consul.io/docs/agent/options.html#_join) 和 [start_join](https://www.consul.io/docs/agent/options.html#start_join) 选项指定服务器列表 。
- 使用 [-retry-join](https://www.consul.io/docs/agent/options.html#_retry_join) 选项指定服务器列表。
- 通过 -retry-join 选项对支持的云环境使用按标签自动加入。

可以在代理配置文件或命令行标志中设置所有这三种方法。

2、手动加入服务器

要手动创建集群，您应该连接到其中一台服务器并运行 `consul join` 命令。

```sh
$ consul join <Node A Address> <Node B Address> <Node C Address>
Successfully joined cluster by contacting 3 nodes.
```

由于联接操作是对称的，因此哪个节点启动它都无关紧要。连接成功后，其中一个节点将输出以下内容：

```sh
[INFO] consul: adding server foo (Addr: 127.0.0.2:8300) (DC: dc1)
[INFO] consul: adding server bar (Addr: 127.0.0.1:8300) (DC: dc1)
[INFO] consul: Attempting bootstrap with nodes: [127.0.0.3:8300 127.0.0.2:8300 127.0.0.1:8300]
    ...
[INFO] consul: cluster leadership acquired
```

3、验证集群并连接客户端

作为健全性检查，该 [consul info](https://www.consul.io/docs/commands/info.html) 命令是有用的工具。它可用于验证 `raft.num_peers` 和查看最新的日志索引 `raft.last_log_index`。在 consul info 跟随者上运行时，`raft.last_log_index` 一旦领导者开始复制，您应该会看到收敛到相同的值。该值表示已存储在磁盘上的最后一个日志条目。

现在，所有服务器都已启动并相互复制，您可以使用与服务器相同的联接方法来联接客户机。客户端可以轻松加入任何现有节点，因此更加容易。所有节点都参与八卦协议以执行基本发现，因此一旦加入集群的任何成员，新客户端将自动找到服务器并进行注册。

>注意：严格来说，不必在客户端之前启动服务器节点。但是，大多数操作将在服务器可用之前失败。

## 服务器性能

由于Consul服务器运行 [共识协议](https://www.consul.io/docs/internals/consensus.html) 来处理所有写入操作，并且几乎所有读取操作都与之联系，因此服务器性能对于Consul群集的总体吞吐量和运行状况至关重要。服务器通常受写操作的 I/O 约束，因为每次添加条目时，底层的Raft日志存储都会执行与磁盘的同步。服务器通常受CPU限制以进行读取，因为读取是从针对并行访问进行了优化的完全内存数据存储进行的。

## Glossary

Consul 和 Consul Enterprise 文档中使用的一些技术术语的简要定义，以及整个 Consul 社区中经常出现的一些术语。

**Agent（代理）：**

代理是Consul群集中每个成员上运行时间较长的守护程序。它通过运行启动`consul agent`。该代理能够以 `客户端` 或 `服务器` 模式运行。由于所有节点都必须运行代理，因此将节点称为客户端或服务器更为简单，但是存在代理的其他实例。所有代理都可以运行DNS或HTTP接口，并负责运行检查并保持服务同步。

**Client（客户）：**

客户端是将所有RPC转发到服务器的代理。客户端是相对无状态的。客户端执行的唯一后台活动是参与LAN闲话池。这具有最小的资源开销，并且仅消耗少量的网络带宽。

**Server（服务器）：**

服务器是具有扩展职责集的代理，包括参与Raft仲裁，维护集群状态，响应RPC查询，与其他数据中心交换WAN闲话以及将查询转发给领导者或远程数据中心。

**Datacenter（数据中心）：**

我们将数据中心定义为私有，低延迟和高带宽的网络环境。这不包括穿越公共互联网的通信，但出于我们的目的，单个EC2区域内的多个可用区将被视为单个数据中心的一部分。

**Consensus（共识）：**

当在我们的文档中使用时，我们使用共识来表示对当选领导人的同意以及对交易顺序的同意。由于这些事务适用于 [有限状态机](https://en.wikipedia.org/wiki/Finite-state_machine)，因此我们对一致性的定义意味着复制状态机的一致性。共识在[Wikipedia](https://en.wikipedia.org/wiki/Consensus_(computer_science))上有更详细的描述，我们的实现在[这里](https://www.consul.io/docs/internals/consensus.html)描述。

**Gossip（八卦）：**

Consul建立在[Serf](https://www.serf.io/)之上，该Serf提供了完整的[八卦协议](https://en.wikipedia.org/wiki/Gossip_protocol)，可用于多种目的。Serf提供成员资格，故障检测和事件广播。我们在[八卦文档](https://www.consul.io/docs/internals/gossip.html)中对这些用法的详细说明，足以知道八卦涉及随机的节点到节点通信，主要是通过UDP。

**LAN Gossip（局域网八卦）：**

指的是局域网八卦池，其中包含的节点都位于同一局域网或数据中心上。

**WAN Gossip（广域网八卦）：**

指仅包含服务器的WAN闲话池。这些服务器主要位于不同的数据中心，通常通过Internet或广域网进行通信。

**RPC：**

远程过程调用。这是一种请求/响应机制，允许客户端向服务器发出请求。
