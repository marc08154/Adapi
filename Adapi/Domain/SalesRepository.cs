using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Adapi.Models;

namespace Adapi.Domain
{
    // TODO: Interface for mocking in Unit Tests
    public class SalesRepository
    {
        private readonly IMongoClient _mongoClient;
        private IMongoCollection<Sale> salesCollection;

        public SalesRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            var database = _mongoClient.GetDatabase("AdapiDB");

            salesCollection = database.GetCollection<Sale>("Sales");
        }

        public async void Insert(Sale sale)
        {
            await salesCollection.InsertOneAsync(sale);
        }

    }
}
