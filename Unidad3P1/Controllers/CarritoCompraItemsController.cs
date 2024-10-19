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
    public class CarritoCompraItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarritoCompraItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarritoCompraItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CarritoCompraItem.Include(c => c.Carrito).Include(c => c.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CarritoCompraItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompraItem = await _context.CarritoCompraItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CarritoCompraItemId == id);
            if (carritoCompraItem == null)
            {
                return NotFound();
            }

            return View(carritoCompraItem);
        }

        // GET: CarritoCompraItems/Create
        public IActionResult Create()
        {
            ViewData["CarritoId"] = new SelectList(_context.CarritoCompra, "CarritoCompraId", "CarritoCompraId");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "DescripcionCorta");
            return View();
        }

        // POST: CarritoCompraItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarritoCompraItemId,Cantidad,ProductoId,CarritoId")] CarritoCompraItemEntity carritoCompraItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carritoCompraItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarritoId"] = new SelectList(_context.CarritoCompra, "CarritoCompraId", "CarritoCompraId", carritoCompraItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "DescripcionCorta", carritoCompraItem.ProductoId);
            return View(carritoCompraItem);
        }

        // GET: CarritoCompraItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompraItem = await _context.CarritoCompraItem.FindAsync(id);
            if (carritoCompraItem == null)
            {
                return NotFound();
            }
            ViewData["CarritoId"] = new SelectList(_context.CarritoCompra, "CarritoCompraId", "CarritoCompraId", carritoCompraItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "DescripcionCorta", carritoCompraItem.ProductoId);
            return View(carritoCompraItem);
        }

        // POST: CarritoCompraItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarritoCompraItemId,Cantidad,ProductoId,CarritoId")] CarritoCompraItemEntity carritoCompraItem)
        {
            if (id != carritoCompraItem.CarritoCompraItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carritoCompraItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarritoCompraItemExists(carritoCompraItem.CarritoCompraItemId))
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
            ViewData["CarritoId"] = new SelectList(_context.CarritoCompra, "CarritoCompraId", "CarritoCompraId", carritoCompraItem.CarritoId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "ProductoId", "DescripcionCorta", carritoCompraItem.ProductoId);
            return View(carritoCompraItem);
        }

        // GET: CarritoCompraItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carritoCompraItem = await _context.CarritoCompraItem
                .Include(c => c.Carrito)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CarritoCompraItemId == id);
            if (carritoCompraItem == null)
            {
                return NotFound();
            }

            return View(carritoCompraItem);
        }

        // POST: CarritoCompraItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carritoCompraItem = await _context.CarritoCompraItem.FindAsync(id);
            if (carritoCompraItem != null)
            {
                _context.CarritoCompraItem.Remove(carritoCompraItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarritoCompraItemExists(int id)
        {
            return _context.CarritoCompraItem.Any(e => e.CarritoCompraItemId == id);
        }
    }
}
