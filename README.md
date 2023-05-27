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

- run *SendMessages* and see the messages show up in the queue
<img src="/pictures/queue6.png" title="queue"  width="900">

- run *PeekMessage* and see the messages peeked from the queue, but not removed
<img src="/pictures/queue7.png" title="queue"  width="900">

- run *ReceiveMessages* and see the messages deleted from the queue
<img src="/pictures/queue8.png" title="queue"  width="900">


## Dead Letter Queue

- enable dead letter queue on the azure portal
<img src="/pictures/dlq.png" title="dead letter queue"  width="900">

- send messages with expiration and wait till they are moved onto the dead letter queue
<img src="/pictures/dlq2.png" title="dead letter queue"  width="900">

- run *ReceiveMessagesFromDLQ* and see the messages deleted in the  dead letter queue
<img src="/pictures/dlq3.png" title="dead letter queue"  width="900">
