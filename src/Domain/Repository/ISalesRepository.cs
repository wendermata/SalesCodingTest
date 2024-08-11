using Domain.Aggregates;
using Domain.Repository.Shared.SearchableRepository;

namespace Domain.Repository
{
    public interface ISalesRepository : IRepository<SaleAggregate>, ISearchableRepository<SaleAggregate>
    {
    }
}
