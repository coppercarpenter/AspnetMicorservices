using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        #region Private Fields
        private readonly ICatalogContext _db;

        #endregion

        #region Private Methods

        #endregion

        #region Constructor
        public ProductRepository(ICatalogContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        #endregion

        #region Properties

        #endregion

        #region Fields

        #endregion

        #region Methods



        public async Task CreateProduct(Product product)
        {
            await _db.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(e => e.Id, id);

            var res = await _db.Products.DeleteOneAsync(filter);

            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _db.Products.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(e => e.Category, category);
            return await _db.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(e => e.Name, name);
            return await _db.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _db.Products.Find(f => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var res = await _db.Products.ReplaceOneAsync(r => r.Id == product.Id, product);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }
        #endregion
    }
}
