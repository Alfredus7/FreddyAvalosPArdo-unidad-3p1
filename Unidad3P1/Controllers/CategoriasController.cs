using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data;
using Unidad3P1.Data.Entidades;
using Unidad3P1.ViewModels;
using webApi.Dtos;


namespace Unidad3P1.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            var entidades = await _context.Categoria.ToListAsync();
            var modelList = _mapper.Map<List<CategoryDtos>>(entidades);

            return View(modelList);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoriaEntity = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoriaEntity == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CategoryViewModel>(categoriaEntity);

            return View(viewModel);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,CategoriaNombre,Descripcion")] CategoryViewModel categoria)
        {
            if (ModelState.IsValid)
            {
                var categoriaEntity = _mapper.Map<CategoryViewModel, CategoriaEntity>(categoria);

                _context.Add(categoriaEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CategoryViewModel>(categoria);

            return View(viewModel);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,CategoriaNombre,Descripcion")] CategoryViewModel categoriaModel)
        {
            if (id != categoriaModel.CategoriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var categoriaEntity = await _context.Categoria.FindAsync(id);

                    _mapper.Map<CategoryViewModel, CategoriaEntity>(categoriaModel, categoriaEntity);

                    _context.Update(categoriaEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoriaModel.CategoriaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaModel);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .FirstOrDefaultAsync(m => m.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<CategoryViewModel>(categoria);

            return View(viewModel);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria != null)
            {
                _context.Categoria.Remove(categoria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaId == id);
        }
        public async Task<IActionResult> ProductosCategoria(int? id)
        {
            return View(id);
        }
    }
}
