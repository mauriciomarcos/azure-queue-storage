namespace QueueStorageSubscriber.Interfaces;

internal interface IQueueSubscriberManager
{
    Task DequeueMessagesAsync(string queueName);
}