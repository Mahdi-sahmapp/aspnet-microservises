

using Catolog.Api.Data;
using Catolog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catolog.Api.Repositories
{
    public class ProducyRepository : IProductRepository
    {
        #region Constructor
        private readonly ICatologContext _context;

        public ProducyRepository(ICatologContext catologcontext)
        {
            _context = catologcontext;
        }
        #endregion

        public async Task CreateProduct(Product product)
        {
            // mongoD syntax
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string Id)
        {
            // mongoD syntax
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(a => a.Id, Id);
            DeleteResult deleteresualt = await _context.Products.DeleteOneAsync(filter);

            return deleteresualt.IsAcknowledged && deleteresualt.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string Id)
        {
            return await _context.Products.Find(a => a.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.Find(a => a.Name == name).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var t = await _context.Products.Find(a => true).ToListAsync();
            return await _context.Products.Find(a => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string cat)
        {
            // mongoD syntax
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, cat);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            // mongoD syntax
            var UpdateResualt = await _context.Products
                .ReplaceOneAsync(filter: a => a.Id == product.Id, replacement: product);

            return UpdateResualt.IsAcknowledged && UpdateResualt.ModifiedCount > 0;
        }
    }
}
