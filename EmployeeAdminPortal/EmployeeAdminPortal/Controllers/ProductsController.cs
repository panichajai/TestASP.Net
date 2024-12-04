using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(dbContext.products.ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProductById(int id)
        {
            var product = dbContext.products.Find(id);

            if (product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            var existingProduct = dbContext.products.FirstOrDefault(e => e.Name == addProductDto.Name);

            if (existingProduct != null)
            {
                return Conflict(new { message = "An Product with this name already exists." });
            }

            if (string.IsNullOrWhiteSpace(addProductDto.Name))
            {
                return BadRequest(new { message = "Product name cannot be empty." });
            }

            var productEntity = new Product()
            {
                Name = addProductDto.Name,
                Price = addProductDto.Price,
                Stock = addProductDto.Stock
            };

            dbContext.products.Add(productEntity);
            dbContext.SaveChanges();
            return Ok(productEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateProduct(int id, UpdateProductDto updateProductDto)
        {
            var product = dbContext.products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            product.Name = updateProductDto.Name;
            product.Price = updateProductDto.Price;
            product.Stock = updateProductDto.Stock;

            dbContext.SaveChanges();

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.products.Find(id);

            if (product is null)
            {
                return NotFound();
            }

            dbContext.products.Remove(product);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
