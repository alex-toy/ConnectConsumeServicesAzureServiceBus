# Connect and Consume Services - Azure Service Bus

## Creating and working with the queues

### Create namespace

<img src="/pictures/namespace.png" title="namespace"  width="500">

### Create Queues

<img src="/pictures/queue.png" title="queue"  width="900">

- on *Service Bus Explorer*, you can send messages
<img src="/pictures/queue2.png" title="queue"  width="900">

- and peek messages
<img src="/pictures/queue3.png" title="queue"  width="900">


## Create .NET project

- install packages
```
Azure.Messaging.ServiceBus
```

- add a send access policy on the queue
<img src="/pictures/queue4.png" title="queue"  width="900">

- grab a connection string on it
<img src="/pictures/queue5.png" title="queue"  width="900">

- run the program and see the messages show up in the queue
<img src="/pictures/queue6.png" title="queue"  width="900">

- run the program and see the messages peeked from the queue, but not removed
<img src="/pictures/queue7.png" title="queue"  width="900">

