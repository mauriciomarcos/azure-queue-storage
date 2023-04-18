using Microsoft.Extensions.Hosting;
using QueueStorageSubscriber.Interfaces;

namespace QueueStorageSubscriberListener.Classes
{
    public sealed class StorageSubscriberListenerBackgroundService : IHostedService, IDisposable
    {        
        private readonly IQueueSubscriberManager _queueListener;
        private readonly System.Timers.Timer _timer;

        public StorageSubscriberListenerBackgroundService(IQueueSubscriberManager queueListener,
                                                          System.Timers.Timer timer) => (_queueListener, _timer) = (queueListener, timer);      

        public async Task StartAsync(CancellationToken cancellationToken)
        {
             _timer.Elapsed += async (sender, e) => await _queueListener.DequeueMessagesAsync("user-timeout-queue", cancellationToken);
             await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

        public void Dispose() => _timer?.Dispose();
    }
}