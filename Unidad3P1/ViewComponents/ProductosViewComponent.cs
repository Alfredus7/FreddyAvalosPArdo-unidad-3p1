using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Unidad3P1.Data;
using Unidad3P1.ViewModels;

namespace Unidad3P1.ViewComponents
{
    public class ProductosViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductosViewComponent(ApplicationDbContext dbContext,
            IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            var entidades = await _context.Producto
                .Where(x => x.CategoriaId == id)
                .ToListAsync();
            var modelsList = _mapper.Map<List<ProductoViewModel>>(entidades);
            return View("MenuProductos", modelsList);
        }
    }
}
