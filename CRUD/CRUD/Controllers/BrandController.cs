using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUD.EF;
using CRUD.EF.Models;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly CrudContext dbcontext;

        public BrandController(CrudContext context)
        {
            dbcontext = context;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
          if (dbcontext.Brands == null)
          {
              return NotFound();
          }
            return await dbcontext.Brands.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
          if (dbcontext.Brands == null)
          {
              return NotFound();
          }
            var brand = await dbcontext.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            dbcontext.Entry(brand).State = EntityState.Modified;

            try
            {
                await dbcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
          if (dbcontext.Brands == null)
          {
              return Problem("Entity set 'CrudContext.Brands'  is null.");
          }
            dbcontext.Brands.Add(brand);
            await dbcontext.SaveChangesAsync();

            return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (dbcontext.Brands == null)
            {
                return NotFound();
            }
            var brand = await dbcontext.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            dbcontext.Brands.Remove(brand);
            await dbcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return (dbcontext.Brands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
