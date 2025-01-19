using DevFreela.Domain.Domain.Interfaces;
using MediatR;

namespace DevFreela.Application.UseCases.Project.FinishedProject;
public class FinishedProject : IRequestHandler<FinishedProjectInput, bool>
{
    private IPayment _paymentRepository;


    public FinishedProject(IPayment paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public Task<bool> Handle(FinishedProjectInput request, CancellationToken cancellationToken)
    {
        _paymentRepository.ProcessPayment("buceta");

        return Task.FromResult(true);
    }
}
