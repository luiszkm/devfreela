
namespace DevFreela.Domain.Domain.seddwork.SearchbleRepository.cs;
public interface ISearchRepository<TAggregate>
where TAggregate : AggregateRoot
{
    Task<SearchOutput<TAggregate>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}
