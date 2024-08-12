using Domain.Aggregates;
using Domain.Entities;
using Domain.Repository;
using Domain.Repository.Shared.SearchableRepository;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infra.Mongo.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly MongoSettings _settings;
        private readonly IMongoCollection<SaleAggregate> _collection;

        public SalesRepository(IMongoService service, IOptions<MongoSettings> settings)
        {
            _settings = settings.Value;
            _collection = service.Database.GetCollection<SaleAggregate>(_settings.SalesCollectionName);
        }

        public async Task DeleteAsync(SaleAggregate entity, CancellationToken cancellationToken)
        {
            await _collection.DeleteOneAsync(c => c.Id == entity.Id, cancellationToken);
        }

        public async Task<SaleAggregate> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var cursor = await _collection.FindAsync(c => c.Id == id, cancellationToken: cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task InsertAsync(SaleAggregate entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public async Task<SearchOutput<SaleAggregate>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var toSkip = (input.Page - 1) * input.PageSize;

            var sortBuilder = Builders<SaleAggregate>.Sort;
            var sort = (input.OrderBy.ToLower(), input.Order) switch
            {
                ("zipcode", SearchOrder.Asc) => sortBuilder.Ascending(x => x.ZipCode),
                ("zipcode", SearchOrder.Desc) => sortBuilder.Descending(x => x.ZipCode),
                ("createdat", SearchOrder.Asc) => sortBuilder.Ascending(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => sortBuilder.Descending(x => x.CreatedAt),
                _ => sortBuilder.Ascending(x => x.CreatedAt).Ascending(x => x.Id),
            };

            var query = _collection.Find(Builders<SaleAggregate>.Filter.Empty).Sort(sort).Skip(toSkip).Limit(input.PageSize);

            var list = await query.ToListAsync(cancellationToken);
            var total = await _collection.CountDocumentsAsync(Builders<SaleAggregate>.Filter.Empty, cancellationToken: cancellationToken);

            return new SearchOutput<SaleAggregate>(input.Page, input.PageSize, (int)total, list);
        }

        public async Task UpdateAsync(SaleAggregate entity, CancellationToken cancellationToken)
        {
            var filter = Builders<SaleAggregate>.Filter.Eq(c => c.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity, cancellationToken: cancellationToken);
        }

    }
}
