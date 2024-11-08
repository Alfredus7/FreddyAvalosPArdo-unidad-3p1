using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data;
using Unidad3P1.Data.Entidades;

namespace webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoriaController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEntity>>> GetCategoria()
        {
            return await _context.Categoria.ToListAsync();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEntity>> GetCategoriaEntity(int id)
        {
            var categoriaEntity = await _context.Categoria.FindAsync(id);

            if (categoriaEntity == null)
            {
                return NotFound();
            }

            return categoriaEntity;
        }

        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEntity(int id, CategoriaEntity categoriaEntity)
        {
            if (id != categoriaEntity.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoriaEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaEntityExists(id))
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

        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaEntity>> PostCategoriaEntity(CategoriaEntity categoriaEntity)
        {
            _context.Categoria.Add(categoriaEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaEntity", new { id = categoriaEntity.CategoriaId }, categoriaEntity);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaEntity(int id)
        {
            var categoriaEntity = await _context.Categoria.FindAsync(id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoriaEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaEntityExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaId == id);
        }
    }
}
