using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data;
using Unidad3P1.Data.Entidades;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoEntity>>> GetProducto()
        {
            return await _context.Producto.ToListAsync();
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoEntity>> GetProductoEntity(int id)
        {
            var productoEntity = await _context.Producto.FindAsync(id);

            if (productoEntity == null)
            {
                return NotFound();
            }

            return productoEntity;
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoEntity(int id, ProductoEntity productoEntity)
        {
            if (id != productoEntity.ProductoId)
            {
                return BadRequest();
            }

            _context.Entry(productoEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoEntityExists(id))
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

        // POST: api/Producto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoEntity>> PostProductoEntity(ProductoEntity productoEntity)
        {
            _context.Producto.Add(productoEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoEntity", new { id = productoEntity.ProductoId }, productoEntity);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoEntity(int id)
        {
            var productoEntity = await _context.Producto.FindAsync(id);
            if (productoEntity == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(productoEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoEntityExists(int id)
        {
            return _context.Producto.Any(e => e.ProductoId == id);
        }
    }
}
