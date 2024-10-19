using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidad3P1.Data.Entidades;

namespace Unidad3P1.Data.DataMappings
{
    internal class CarritoCompraDataMapping : IEntityTypeConfiguration<CarritoCompraEntity>
    {
        public void Configure(EntityTypeBuilder<CarritoCompraEntity> builder)
        {
           builder.ToTable("CarritoCompra")
                .HasKey(x => x.CarritoCompraId);
            builder.Property(x => x.CarritoCompraId).UseIdentityColumn();


        }
    }
}
