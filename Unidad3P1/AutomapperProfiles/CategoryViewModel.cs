using System.ComponentModel.DataAnnotations;

namespace Unidad3P1.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoriaId { get; set; }
        [Required]
        public string CategoriaNombre { get; set; }
        public string Descripcion { get; set; }
    }
}
