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
    public class CarritoComprasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoComprasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarritoCompras
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarritoCompra.ToListAsync());
        }

        // GET: CarritoCompras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra
                .FirstOrDefaultAsync(m => m.CarritoCompraId == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // GET: CarritoCompras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarritoCompras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoCompraId")] CarritoCompraEntity carritoCompra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoCompra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carritoCompra);
        }

        // GET: CarritoCompras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra.FindAsync(id);
            if (carritoCompra == null)
            {
                return NotFound();
            }
            return View(carritoCompra);
        }

        // POST: CarritoCompras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarritoCompraId")] CarritoCompraEntity carritoCompra)
        {
            if (id != carritoCompra.CarritoCompraId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoCompra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoCompraExists(carritoCompra.CarritoCompraId))
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
            return View(carritoCompra);
        }

        // GET: CarritoCompras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompra = await _context.CarritoCompra
                .FirstOrDefaultAsync(m => m.CarritoCompraId == id);
            if (carritoCompra == null)
            {
                return NotFound();
            }

            return View(carritoCompra);
        }

        // POST: CarritoCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoCompra = await _context.CarritoCompra.FindAsync(id);
            if (carritoCompra != null)
            {
                _context.CarritoCompra.Remove(carritoCompra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoCompraExists(int id)
        {
            return _context.CarritoCompra.Any(e => e.CarritoCompraId == id);
        }
    }
}
