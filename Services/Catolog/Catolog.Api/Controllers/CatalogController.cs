using Catolog.Api.Entities;
using Catolog.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catolog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Constroctor
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository productRepository, ILogger<CatalogController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        #endregion

        #region Get Product
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts()
        {
            var products = await _productRepository.GetProducts();

            return Ok(products);
        }
        #endregion

        #region Get Product by Id
        [HttpGet("{id}", Name = "GetProductXXXXXXXXX")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> getProducts(string id)
        {
            var products = await _productRepository.GetProduct(id);

            if (products == null)
            {
                _logger.LogError($"Product with id: {id} is not found");
                return NotFound();
            }
            return Ok(products);
        }
        #endregion

        #region Get Product by Category name
        [HttpGet("[[action/{category}]]")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> getProductsBycategory(string cat)
        {
            var products = await _productRepository.GetProductsByCategory(cat);

            if (products == null)
            {
                _logger.LogError($"Product with CategoryId: {cat} is not found");
                return NotFound();
            }
            return Ok(products);
        }
        #endregion

        #region create product
        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> createproduct([FromBody] Product product)
        {
            await _productRepository.CreateProduct(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        #endregion

        #region update product
        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> updateproduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateProduct(product));
        }

        #endregion

        #region delete product
        [HttpDelete("{id}",Name ="DeleteProduct")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> deleteproduct(string id)
        {
            return Ok(await _productRepository.DeleteProduct(id));
        }

        #endregion
    }
}
