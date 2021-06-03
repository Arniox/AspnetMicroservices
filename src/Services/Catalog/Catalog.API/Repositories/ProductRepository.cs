using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        //Context and Constructor
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //GET
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync(); //Get all product
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync(); //Get products by ID
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name); //Create Filter
            return await _context.Products.Find(filter).ToListAsync(); //Get products by Name
        }
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await _context.Products.Find(filter).ToListAsync(); //Get products by Category
        }
        public async Task<IEnumerable<Product>> GetProductBellowPrice(int price)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Lt(p => p.Price, price);
            return await _context.Products.Find(filter).ToListAsync(); //Get products bellow price
        }

        public async Task<IEnumerable<Product>> GetProductAbovePrice(int price)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Gt(p => p.Price, price);
            return await _context.Products.Find(filter).ToListAsync(); //Get products above price
        }

        //CRUD
        //POST
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product); //Insert new Product
        }

        //PUT
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product); //Replace old item with new item

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0; //Return true or false
        }

        //DELETE
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id); //Set up filter
            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter); //Delete Product

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0; //Return true or false
        }

    }
}
