using CRUD.EF;
using CRUD.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly CrudContext dbContext;

        public ProductController(CrudContext context)
        {
            dbContext = context;
        }

      
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>>GetProducts()
        {
            var data = (from d in dbContext.Products
                        join b in dbContext.Brands on d.BrandId equals b.Id
                        join c in dbContext.Categories on d.CategoryId equals c.Id

                        select new Product
                        {
                            Id=d.Id,
                            Name=d.Name,
                            BrandId=d.BrandId,
                            CategoryId=d.CategoryId,
                            BrandName=b.Name,
                            CategoryName=c.Name
                        }


                      ).ToListAsync();

            return await data;
        }
        /*public IActionResult GetProducts()
        {
       var products = dbContext.Products.ToList();
         return Ok(products);
        }*/




        [HttpGet("{id}")]
            
        public IActionResult GetProduct(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [HttpPost]
        public IActionResult CreateProduct(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product
            {
                Name = productDto.Name,
                CategoryId = productDto.CategoryId,
                BrandId = productDto.BrandId
            };

            dbContext.Products.Add(product);
            dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.CategoryId = productDto.CategoryId;
            product.BrandId = productDto.BrandId;

            dbContext.SaveChanges();

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            dbContext.Products.Remove(product);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
