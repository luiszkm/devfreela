

namespace DevFreela.Domain.Domain.IntegrationEvents;
public class PaymentApprovedIntegrationEvent
{

    public PaymentApprovedIntegrationEvent(Guid idProject)
    {
        IdProject = idProject;
    }


    public Guid IdProject { get; set; }
}
