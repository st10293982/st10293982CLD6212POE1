using Azure.Storage.Queues;

namespace st10293982CLD6212POE1.Services
{
    public class QueueService
    {
        private readonly QueueClient _queueClient;


        public QueueService(string connectionString, string queueName)
        {
            _queueClient = new QueueClient(connectionString, queueName);
        }

        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
        }
    }
}
