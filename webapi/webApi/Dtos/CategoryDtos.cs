using System.ComponentModel.DataAnnotations;

namespace webApi.Dtos
{
    public class CategoryDtos
    {
        public int CategoriaId { get; set; }
        [Required]
        public string CategoriaNombre { get; set; }
        public string Descripcion { get; set; }
    }
}
