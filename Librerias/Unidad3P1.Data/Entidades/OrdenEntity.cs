using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("Orden")]
    public class OrdenEntity
    {
        public int OrdenId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        [StringLength(100)]
        public string Direccion { get; set; }
        public int Cantidad { get; set; }

        public DateTime FechaOrden { get; set; }


        public virtual ICollection<OrdenDetalleEntity>? Detalles { get; set; }
    }
}
