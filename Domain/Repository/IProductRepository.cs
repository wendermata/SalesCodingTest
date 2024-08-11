using Domain.Entities;
using Domain.Repository.Shared.SearchableRepository;

namespace Domain.Repository
{
    public interface IProductRepository : IRepository<Product>, ISearchableRepository<Product> { }
}
