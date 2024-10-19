using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("OrdenDetalle")]
    public class OrdenDetalleEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdenDetalleId { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public virtual ProductoEntity Producto { get; set; }
        public virtual OrdenEntity Orden { get; set; }
    }
}
