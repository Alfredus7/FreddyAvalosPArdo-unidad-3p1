using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Unidad3P1.Data.Entidades;

namespace Unidad3P1.ViewModels
{
    public class ProductoViewModel
    {

        public int ProductoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public string Precio { get; set; }
        public string? ImagenUrl { get; set; }
        public bool Stock { get; set; }
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual CategoriaEntity? Categoria { get; set; }
    }
}
