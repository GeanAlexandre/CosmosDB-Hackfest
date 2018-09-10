using System.Threading.Tasks;
using Demo.App.Models;
using MongoDB.Driver;

namespace Demo.App.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(ISettings settings)
        {
            _collection = new MongoClient(settings.ConnectionString)
                .GetDatabase("DallasHackfest")
                .GetCollection<Product>(nameof(Product));
        }

        public async Task AddNewAsync(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var filter = Builders<Product>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
