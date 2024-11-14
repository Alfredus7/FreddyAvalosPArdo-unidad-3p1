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

namespace Unidad3P1.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductosController(ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize]
        // GET: Productos
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Producto.Include(p => p.Categoria);

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var modelList = webApiClient.GetProductos<List<ProductoViewModel>>();
            return View(modelList.Data);
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var producto = webApiClient.GetProductoById<ProductoViewModel>(id.Value);

            if (producto.Data == null)
            {
                return NotFound();
            }

            return View(producto.Data);
        }











        
        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaNombre");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,DescripcionCorta,DescripcionLarga,Precio,ImagenUrl,InStock,CategoriaId")] ProductoViewModel producto)
        {
            if (ModelState.IsValid)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var Views = webApiClient.PostProducto<ProductoViewModel>(producto);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaNombre", producto.CategoriaId);
            return View(producto);
        }

















        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var producto = webApiClient.GetProductoById<ProductoViewModel>(id.Value);
            if (producto.Data == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaNombre", producto.Data.CategoriaId);
            
            return View(producto.Data);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,DescripcionCorta,DescripcionLarga,Precio,ImagenUrl,InStock,CategoriaId")] ProductoViewModel producto)
        {
            //if (id != producto.ProductoId)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var ProductoEntity = await _context.Producto.FindAsync(id);

            //        _mapper.Map<ProductoViewModel, ProductoEntity>(producto, ProductoEntity);

            //        _context.Update(ProductoEntity);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductoExists(producto.ProductoId))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
             if (ModelState.IsValid)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                webApiClient.PutProducto<ProductoViewModel, ProductoEntity>(id, producto);
                ViewData["CategoriaId"] = new SelectList(_context.Categoria, "CategoriaId", "CategoriaNombre", producto.CategoriaId);
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }









        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var producto = webApiClient.GetProductoById<ProductoViewModel>(id.Value);
            if (producto == null)
            {
                return NotFound();
            }
            if (producto.Data == null)
            {
                return NotFound();
            }

            return View(producto.Data);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var result = webApiClient.DeleteProductoById<ProductoViewModel>(id);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }









        private bool ProductoExists(int id)
        {
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var producto = webApiClient.GetProductoById<ProductoViewModel>(id);
            return producto.Data != null;
        }
        [Authorize]
        public async Task<IActionResult> ProductosDetalles(int? id)
        {

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            var viewModel = _mapper.Map<ProductoViewModel>(producto);

            return View(viewModel);
        }
    }
}
