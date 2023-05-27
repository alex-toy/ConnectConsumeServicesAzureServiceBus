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


## Duplicate Message Detection

- create a new queue and enable duplication detection
<img src="/pictures/dmd.png" title="duplicate message detection"  width="400">

- run *SendDuplicateMessage* to try to send the exact same message twice and see that the duplication has been detected
<img src="/pictures/dmd2.png" title="duplicate message detection"  width="900">


## Queue properties

- run *SendWithProperties* and see that the properties have been received
<img src="/pictures/qp.png" title="queue properties"  width="900">

- run *ReceiveWithProperties* and retrieve the order as well as  the properties
<img src="/pictures/qp2.png" title="queue properties"  width="900">


## Azure Service Bus Topic

- create a topic
<img src="/pictures/topic.png" title="topic"  width="500">

- on that topic, create two subscriptions A and B
<img src="/pictures/topic2.png" title="topic"  width="500">

- run *SendTopic* and see that the orders have been received
<img src="/pictures/topic3.png" title="topic"  width="900">

- run *ReceiveFromTopic* and see that the orders have been retrieved
<img src="/pictures/topic4.png" title="topic"  width="900">


## Topic Filters

- add a filter to the subscription based on *SQL*
<img src="/pictures/filter.png" title="topic filter"  width="900">

- run *SendTopic* and see that only the orders meeting the filters requirements have been received
<img src="/pictures/filter2.png" title="topic filter"  width="900">

- add a filter to the subscription based on *Correlation*
<img src="/pictures/filter3.png" title="topic filter"  width="900">

