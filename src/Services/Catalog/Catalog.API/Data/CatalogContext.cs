using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.API.Data
{

    internal class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration config)
        {
            var connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
            var databaseName = config.GetValue<string>("DatabaseSettings:DatabaseName");
            var collectionName = config.GetValue<string>("DatabaseSettings:CollectionName");

            // Used to create connection with MongoDb
            var client = new MongoClient(connectionString);

            //Get or create MongoDb 
            var database = client.GetDatabase(databaseName);

            Products = database.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
