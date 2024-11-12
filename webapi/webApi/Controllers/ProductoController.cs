using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data;
using Unidad3P1.Data.Entidades;
using webApi.Dtos;

namespace ApiWeb.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicAuthenticationHandler")]
    public class ProductoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductoController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Producto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDtos>>> GetProducto()
        {
            var entidades = await _context.Producto.ToListAsync();
            var modelList = _mapper.Map<List<ProductoDtos>>(entidades);

            return Ok(modelList);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDtos>> GetProductoEntity(int id)
        {
            var ProductoEntity = await _context.Producto.FindAsync(id);

            if (ProductoEntity == null)
            {
                return NotFound();
            }
            var ProductoDtos = _mapper.Map<ProductoDtos>(ProductoEntity);
            return Ok(ProductoDtos);
        }

        // PUT: api/Producto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoEntity(int id, ProductoDtos productoDto)
        {
            if (id != productoDto.ProductoId)
            {
                return BadRequest();
            }

             var productoEntity = _mapper.Map<ProductoEntity>(productoDto);
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
        public async Task<ActionResult<ProductoDtos>> PostProductoEntity(ProductoDtos productoDto)
        {
            var productoEntity = _mapper.Map<ProductoEntity>(productoDto);

            _context.Producto.Add(productoEntity);
            await _context.SaveChangesAsync();

            var productoDtos = _mapper.Map<ProductoDtos>(productoEntity);

            return CreatedAtAction("GetProductoEntity", new { id = productoEntity.ProductoId }, productoDtos);
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

             var productoDtos = _mapper.Map<ProductoDtos>(productoEntity);

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
