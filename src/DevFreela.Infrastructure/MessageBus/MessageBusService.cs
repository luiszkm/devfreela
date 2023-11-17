
using DevFreela.Domain.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageBus;
public class MessageBusService : IMessageBusService
{
    private readonly ConnectionFactory _connectionFactory;

    public MessageBusService(IConfiguration configuration)
    {
        _connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
        };
    }

    public void PublishMessage(string queue, byte[] message)
    {
        var connection = _connectionFactory.CreateConnection();
        var channel = connection.CreateModel();
        channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
            );

        channel.BasicPublish(
            exchange: "",
            routingKey: queue,
            basicProperties: null,
            body: message
            );

    }
}
