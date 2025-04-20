using Microsoft.AspNetCore.Mvc;
using UrunAPI.Data;
using UrunAPI.Models;

namespace UrunAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DbHelper _db;

        public ProductController(DbHelper db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_db.GetAllProducts());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _db.GetProductById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _db.AddProduct(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Product product)
        {
            _db.UpdateProduct(id, product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _db.DeleteProduct(id);
            return Ok();
        }
    }
}
