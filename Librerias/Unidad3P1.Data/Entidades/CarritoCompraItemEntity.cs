using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("CarritoCompraItem")]
    public class CarritoCompraItemEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarritoCompraItemId { get; set; }
        public int Cantidad { get; set; }

       
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public virtual ProductoEntity Producto { get; set; }
        
        public int CarritoId { get; set; }
        [ForeignKey("CarritoId")]
        public virtual CarritoCompraEntity Carrito { get; set; }
       
    }
}
