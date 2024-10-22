using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Unidad3P1.Data;
using Unidad3P1.ViewModels;

namespace Unidad3P1.ViewComponents
{
    public class CategoriaViewComponent: ViewComponent
    {
        public ApplicationDbContext Context { get; }
        public IMapper Mapper { get; }
        public CategoriaViewComponent(ApplicationDbContext context,
            IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var entidades = await Context.Categoria.ToListAsync();
            var modelList = Mapper.Map<List<CategoryViewModel>>(entidades);
            return View(modelList);
        }
       
    }
}
