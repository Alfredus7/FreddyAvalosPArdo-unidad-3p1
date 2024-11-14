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
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();

            var modelList = webApiClient.GetCategorias<List<CategoryViewModel>>();

            return View(modelList.Data);
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var Views = webApiClient.GetCategoriaById<CategoryViewModel>(id.Value);


            if (Views.Data == null)
            {
                return NotFound();
            }
   

            return View(Views.Data);
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
                WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
                var Views = webApiClient.PostCategoria<CategoryViewModel>(categoria);
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


            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var Views = webApiClient.GetCategoriaById<CategoryViewModel>(id.Value);

            if (Views.Data == null)
            {
                return NotFound();
            }

            return View(Views.Data);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaId,CategoriaNombre,Descripcion")] CategoryViewModel categoriaModel)
        {
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var Views = webApiClient.PutCategoria<CategoryViewModel,ProductoEntity>(id,categoriaModel);
            return View(categoriaModel);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var Views = webApiClient.GetCategoriaById<CategoryViewModel>(id.Value);

            if (Views.Data == null)
            {
                return NotFound();
            }
            return View(Views.Data);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            WebApiClients.WebApiClient webApiClient = new WebApiClients.WebApiClient();
            var Views = webApiClient.DeleteCategoriaById<CategoryViewModel>(id);
            if (Views.Data==false)
            {
                BadRequest();
            }
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
