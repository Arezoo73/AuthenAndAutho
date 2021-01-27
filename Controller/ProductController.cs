using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Inferastructure.IRepository;
using Shop.Inferastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<Product>> Get()
        {
            return productRepository.GetAllProduct();
        }
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            return productRepository.GetProductById(id);
        }
        [HttpPost("Insert")]
        public ActionResult AddProduct(Product product)
        {
            productRepository.AddProduct(product);
            return Ok(product);
        }
    }
}
