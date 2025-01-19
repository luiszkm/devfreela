

using DevFreela.Domain.Domain.Interfaces.DTO;

namespace DevFreela.Domain.Domain.Interfaces;
public interface IPayment
{
    //void ProcessPayment( PaymentInput paymentInfo);
    void ProcessPayment(string message);
}
