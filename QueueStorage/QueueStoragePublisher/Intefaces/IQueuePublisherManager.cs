using Azure.Storage.Queues;

namespace QueueStorage.Intefaces;

internal interface IQueuePublisherManager
{
    Task<bool> CreateQueueIfNotExistsAsync(string queueName);
    Task InsertMessageQueueAsync(string message, string queueName, double? visibilityTimeoutInMinutes = null);
}