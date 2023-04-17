using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueueStorage.Classes;
using QueueStorage.Intefaces;

Console.WriteLine("Iniciando Aplicação Publisher...");

var host = CreateDefaultBuilder().Build();
using IServiceScope serviceScope = host.Services.CreateScope();

IServiceProvider provider = serviceScope.ServiceProvider;
var queueManager = provider.GetRequiredService<IQueuePublisherManager>();

var queueName = "user-timeout-queue";
_ = await queueManager.CreateQueueIfNotExistsAsync(queueName);
await queueManager.InsertMessageQueueAsync("Inserindo Mensagem 01", queueName);
await queueManager.InsertMessageQueueAsync("Inserindo Mensagem 02", queueName, 1);
await queueManager.InsertMessageQueueAsync("Inserindo Mensagem 03", queueName, 1);
await queueManager.InsertMessageQueueAsync("Inserindo Mensagem 04", queueName, 2);

Console.WriteLine("Fim da Aplicação Publisher...");

IHostBuilder CreateDefaultBuilder()
{
    return Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(app =>
        {
            app.AddJsonFile("appsettings.json");
        })
        .ConfigureServices(services =>
        {
            services.AddSingleton<IQueuePublisherManager, QueuePublisherManager>();
        });
}