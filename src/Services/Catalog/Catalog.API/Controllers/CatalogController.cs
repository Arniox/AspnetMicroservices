using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            //Get Products
            var products = await _repository.GetProducts();

            //If List is empty
            if (products.Count() == 0)
                return NoContent();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            //Get Product
            var product = await _repository.GetProduct(id);

            //If No product was found
            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }
            return Ok(product);
        }

        [Route("[action]/{name}", Name = "GetProductByName")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            //Get Products
            var products = await _repository.GetProductByName(name);

            //If List is empty
            if (products.Count() == 0)
                return NoContent();
            return Ok(products);
        }

        [Route("[action]/{category}", Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            //Get Products
            var products = await _repository.GetProductByCategory(category);

            //If List is empty
            if (products.Count() == 0)
                return NoContent();
            return Ok(products);
        }

        [Route("[action]/{price}", Name = "GetProductBellowPrice")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductBellowPrice(int price)
        {
            //Get Products
            var products = await _repository.GetProductBellowPrice(price);

            //If List is empty
            if (products.Count() == 0)
                return NoContent();
            return Ok(products);
        }

        [Route("[action]/{price}", Name = "GetProductAbovePrice")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductAbovePrice(int price)
        {
            //Get Products
            var products = await _repository.GetProductAbovePrice(price);

            //If List is empty
            if (products.Count() == 0)
                return NoContent();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            //Update Product
            var success = await _repository.UpdateProduct(product);

            //Return Bad Request if success is false
            if (!success)
                return StatusCode(304, success);
            return Ok(success);
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotModified)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            //Delete Product
            var success = await _repository.DeleteProduct(id);

            //Return Bad Request is success is false
            if (!success)
                return StatusCode(304, success);
            return Ok(success);
        }

    }
}
