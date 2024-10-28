using Unidad3P1.Data.Entidades;

namespace Unidad3P1.ViewModels
{
    public class OrdenViewModel
    {
        public int OrdenId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int Cantidad { get; set; }

        public DateTime FechaOrden { get; set; }


        public virtual ICollection<OrdenDetalleEntity>? Detalles { get; set; }
    }
}
