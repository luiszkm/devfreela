

using System.Text;
using System.Text.Json;
using DevFreela.Domain.Domain.Interfaces;
using DevFreela.Domain.Domain.Interfaces.DTO;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Payments;
public class Payment : IPayment
{
    private readonly IMessageBusService _messageBusService;
    private const string QUEUE_NAME = "Payments";
    public Payment(IMessageBusService messageBusService)
    {
        _messageBusService = messageBusService;
    }
    // public async void ProcessPayment(PaymentInput paymentInfo)
    // {
    //     var paymentInfoJson = JsonSerializer.Serialize(paymentInfo);
    //     var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
    //     _messageBusService.PublishMessage(QUEUE_NAME, paymentInfoBytes);
    //
    // }

    public void ProcessPayment(string message)
    {
        var paymentInfoJson = JsonSerializer.Serialize(message);
        var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);
        _messageBusService.PublishMessage(QUEUE_NAME, paymentInfoBytes);
    }
}
