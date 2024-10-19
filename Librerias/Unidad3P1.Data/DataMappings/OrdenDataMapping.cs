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
    public class OrdenDataMapping : IEntityTypeConfiguration<OrdenEntity>
    {
        public void Configure(EntityTypeBuilder<OrdenEntity> builder)
        {
            builder.ToTable("Orden")
                .HasKey(x => x.OrdenId);
            builder.Property(x => x.OrdenId).UseIdentityColumn();

            builder.HasMany(x => x.Detalles)
                .WithOne(x => x.Orden)
                .HasForeignKey(x => x.OrdenId);


        }
    }
}
