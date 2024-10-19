using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("CarritoCompra")]
    public class CarritoCompraEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarritoCompraId { get; set; }
        public ICollection<CarritoCompraItemEntity> CarritoItems { get; set; }
    }
}
