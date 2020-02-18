using MongoDB.Driver;
using Adapi.Models;

namespace Adapi.Domain
{
    // TODO: Interface for mocking in Unit Tests
    public class SalesRepository
    {
        private readonly IMongoClient _mongoClient;
        private IMongoCollection<Sale> _salesCollection;

        public SalesRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            var database = _mongoClient.GetDatabase("AdapiDB");

            _salesCollection = database.GetCollection<Sale>("Sales");
        }

        public async void Insert(Sale sale)
        {
            await _salesCollection.InsertOneAsync(sale);
        }

    }
}
