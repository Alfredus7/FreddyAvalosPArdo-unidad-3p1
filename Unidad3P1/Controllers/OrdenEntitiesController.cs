using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data;
using Unidad3P1.Data.Entidades;

namespace Unidad3P1.Controllers
{
    public class OrdenEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdenEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrdenEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ordene.ToListAsync());
        }

        // GET: OrdenEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenEntity = await _context.Ordene
                .FirstOrDefaultAsync(m => m.OrdenId == id);
            if (ordenEntity == null)
            {
                return NotFound();
            }

            return View(ordenEntity);
        }

        // GET: OrdenEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrdenEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrdenId,Nombre,Apellido,Direccion,Cantidad,FechaOrden")] OrdenEntity ordenEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordenEntity);
        }

        // GET: OrdenEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenEntity = await _context.Ordene.FindAsync(id);
            if (ordenEntity == null)
            {
                return NotFound();
            }
            return View(ordenEntity);
        }

        // POST: OrdenEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrdenId,Nombre,Apellido,Direccion,Cantidad,FechaOrden")] OrdenEntity ordenEntity)
        {
            if (id != ordenEntity.OrdenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenEntityExists(ordenEntity.OrdenId))
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
            return View(ordenEntity);
        }

        // GET: OrdenEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordenEntity = await _context.Ordene
                .FirstOrDefaultAsync(m => m.OrdenId == id);
            if (ordenEntity == null)
            {
                return NotFound();
            }

            return View(ordenEntity);
        }

        // POST: OrdenEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordenEntity = await _context.Ordene.FindAsync(id);
            if (ordenEntity != null)
            {
                _context.Ordene.Remove(ordenEntity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenEntityExists(int id)
        {
            return _context.Ordene.Any(e => e.OrdenId == id);
        }







        public IActionResult OrdenesProductos(int? id)
        {
            return View();
        }

        // POST: OrdenEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdenesProductos([Bind("OrdenId,Nombre,Apellido,Direccion,Cantidad,FechaOrden")] OrdenEntity ordenEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ordenEntity);
        }
    }
}
