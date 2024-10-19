using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("Producto")]
    public class ProductoEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }
        [StringLength(255)]
        [Required]
        public string Nombre { get; set; }
        public string DescripcionCorta { get; set; }
        public string DescripcionLarga { get; set; }
        public string Precio { get; set; }
        public string? ImagenUrl { get; set; }
        public bool InStock { get; set; }
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual CategoriaEntity Categoria { get; set; }

        public virtual ICollection<OrdenDetalleEntity> Detalles { get; set; }

        public virtual ICollection<CarritoCompraEntity> CarritoItems { get; set; }
    }
}
