using MongoDB.Driver;
using Adapi.Models;

namespace Adapi.Domain
{
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

        public void Insert(Sale sale)
        {
            _salesCollection.InsertOne(sale);
        }

    }
}
