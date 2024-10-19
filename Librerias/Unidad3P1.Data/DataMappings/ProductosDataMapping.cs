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
    public class ProductosDataMapping : IEntityTypeConfiguration<ProductoEntity>
    {
        public void Configure(EntityTypeBuilder<ProductoEntity> builder)
        {
            builder.ToTable("Producto")
                .HasKey(x => x.ProductoId);

            builder.Property(x => x.DescripcionCorta).HasMaxLength(100).IsRequired();

            builder.HasMany(x => x.Detalles)
              .WithOne(x => x.Producto)
              .HasForeignKey(x => x.ProductoId);

        }
    }
}
