
namespace DevFreela.Domain.Domain.seddwork;
public interface IGenericRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    public Task Create(TAggregate aggregate, CancellationToken cancellationToken);
    public Task<TAggregate> GetById(Guid id, CancellationToken cancellationToken);
    public Task Update(TAggregate aggregate, CancellationToken cancellationToken);
    public Task Delete(TAggregate aggregate, CancellationToken cancellationToken);

}
