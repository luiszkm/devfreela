

namespace DevFreela.Domain.Domain.Interfaces.DTO;
public class PaymentInput
{


    public Guid Id { get; set; }
    public Guid IdUser { get; set; }
    public Guid IdProject { get; set; }

    public decimal TotalCost { get; set; }

}
