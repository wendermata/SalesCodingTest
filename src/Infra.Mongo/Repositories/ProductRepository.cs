using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoSettings _settings;
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoService service, IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<Product>(_settings.ProductCollectionName);
        }

        public async Task DeleteAsync(Product entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(Product entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task<SearchOutput<Product>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PageSize;
            var filterBuilder = Builders<Product>.Filter;
            var filter = filterBuilder.Eq(x => x.IsActive, true);

            if (!string.IsNullOrWhiteSpace(input.Search))
            {
                var searchFilter = filterBuilder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(input.Search, "i"));
                filter = filterBuilder.And(filter, searchFilter);
            }

            var sortBuilder = Builders<Product>.Sort;
            var sort = (input.OrderBy.ToLower(), input.Order) switch
            {
                ("name", SearchOrder.Asc) => sortBuilder.Ascending(x => x.Name),
                ("name", SearchOrder.Desc) => sortBuilder.Descending(x => x.Name),
                ("createdat", SearchOrder.Asc) => sortBuilder.Ascending(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => sortBuilder.Descending(x => x.CreatedAt),
                _ => sortBuilder.Ascending(x => x.CreatedAt).Ascending(x => x.Id),
            };

            var query = _collection.Find(filter).Sort(sort).Skip(toSkip).Limit(input.PageSize);

            var list = await query.ToListAsync(cancellationToken);
            var total = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

            return new SearchOutput<Product>(input.Page, input.PageSize, (int)total, list);
        }

        public async Task UpdateAsync(Product entity, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }
}
