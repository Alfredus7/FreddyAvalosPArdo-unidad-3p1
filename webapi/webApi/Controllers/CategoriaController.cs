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
using webApi.Dtos;

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
        public async Task<ActionResult<IEnumerable<CategoryDtos>>> GetCategoria()
        {
            var entidades = await _context.Categoria.ToListAsync();
            var modelList = _mapper.Map<List<CategoryDtos>>(entidades);

            return Ok(modelList);
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDtos>> GetCategoriaEntity(int id)
        {
            var categoriaEntity = await _context.Categoria.FindAsync(id);

            if (categoriaEntity == null)
            {
                return NotFound();
            }

            // Mapear la entidad a DTO
            var categoriaDto = _mapper.Map<CategoryDtos>(categoriaEntity);
            return Ok(categoriaDto);
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEntity(int id, CategoryDtos categoriaDto)
        {
           

            // Mapear el DTO a la entidad
            var categoriaEntity = _mapper.Map<CategoriaEntity>(categoriaDto);
            _context.Entry(categoriaEntity).State = EntityState.Modified;
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
              
                    throw;
            }

            return NoContent();
        }

        // POST: api/Categoria
        [HttpPost]
        public async Task<ActionResult<CategoryDtos>> PostCategoriaEntity(CategoryDtos categoriaDto)
        {
            // Mapear el DTO a la entidad
            var categoriaEntity = _mapper.Map<CategoriaEntity>(categoriaDto);
            _context.Categoria.Add(categoriaEntity);
            await _context.SaveChangesAsync();

            // Mapear de vuelta la entidad creada al DTO para la respuesta
            var createdCategoriaDto = _mapper.Map<CategoryDtos>(categoriaEntity);

            return CreatedAtAction("GetCategoriaEntity", new { id = createdCategoriaDto.CategoriaId }, createdCategoriaDto);
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

    }
}
