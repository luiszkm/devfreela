

using System.Text;
using System.Text.Json;
using DevFreela.Domain.Domain.Enums;
using DevFreela.Domain.Domain.IntegrationEvents;
using DevFreela.Domain.Domain.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DevFreela.Infrastructure.Payments;
public class PaymentApprovedConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private const string QUEUE_NAME = "payments";
    private const string PAYMENT_APPROVED_QUEUE_NAME = "payments/approved";

    public PaymentApprovedConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: PAYMENT_APPROVED_QUEUE_NAME,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            var body = eventArgs.Body;
            var paymentApprovedJson = Encoding.UTF8.GetString(body.ToArray());
            var paymentApprovedDto = JsonSerializer.Deserialize<PaymentApprovedIntegrationEvent>(paymentApprovedJson);

            await FinishedProject(paymentApprovedDto.IdProject);

            _channel.BasicAck(eventArgs.DeliveryTag, false);

        };

        _channel.BasicConsume(PAYMENT_APPROVED_QUEUE_NAME, false, consumer);
        return Task.CompletedTask;
    }


    public async Task FinishedProject(Guid id)
    {
        var scope = _serviceProvider.CreateScope();
        var ProjectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

        var project = await ProjectRepository.GetById(id, CancellationToken.None);
        project.ChangeStatus(ProjectStatusEnum.Finished);

    }
}
