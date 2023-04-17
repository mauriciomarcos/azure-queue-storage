
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueStorageSubscriber.Classes;
using QueueStorageSubscriber.Interfaces;

Console.WriteLine("Iniciando Aplicação Subscriber...");

var host = CreateDefaultBuilder().Build();
using IServiceScope serviceScope = host.Services.CreateScope();

IServiceProvider provider = serviceScope.ServiceProvider;
var queueSubscriberManager = provider.GetRequiredService<IQueueSubscriberManager>();

var queueName = "user-timeout-queue";
var repeat = true;

while (repeat)
{
    await queueSubscriberManager.DequeueMessagesAsync(queueName);
}

Console.WriteLine("Fim da Aplicação Subscriber...");

IHostBuilder CreateDefaultBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices(services =>
        {
            services.AddSingleton<IQueueSubscriberManager, QueueSubscriberManager>();
        });
}