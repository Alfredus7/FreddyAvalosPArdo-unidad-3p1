using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidad3P1.Data.Entidades
{
    [Table("Categoria")]
    public class CategoriaEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public string Descripcion { get; set; }   
        public virtual ICollection<ProductoEntity> Items { get; set; }
    }
}
