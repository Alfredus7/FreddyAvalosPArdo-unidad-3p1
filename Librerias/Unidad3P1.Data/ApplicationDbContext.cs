using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Unidad3P1.Data.DataMappings;
using Unidad3P1.Data.Entidades;

namespace Unidad3P1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CategoriaEntity>().ToTable("Categoria")
                .HasKey(x => x.CategoriaId);

            builder.Entity<CategoriaEntity>().HasMany (x => x.Items)
                .WithOne(x => x.Categoria).HasForeignKey(x => x.CategoriaId);

            //linea que haga referencia al archivo de data mapping (2do metodo)
            builder.ApplyConfiguration(new ProductosDataMapping());
            builder.ApplyConfiguration(new OrdenDataMapping());



            //seed admin
            string ADMIN_ID = "5d4725d6-6dc4-4d3f-ab81-dda36159300e";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
            string ROLE_ID_VENTAS = "265e39fd-0ed7-4434-9e01-f669da0925de";
            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Ventas",
                NormalizedName = "VENTAS",
                Id = ROLE_ID_VENTAS,
                ConcurrencyStamp = ROLE_ID_VENTAS
            });

            //create user
            var appUser = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "admin@email.com",
                EmailConfirmed = true,
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
            };

            //set user password
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "As12345!");

            //seed user
            builder.Entity<IdentityUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }

        public DbSet<CategoriaEntity> Categoria { get; set; } 
        public DbSet<ProductoEntity> Producto { get; set; }
        public DbSet<OrdenEntity> Ordene { get; set; }
        public DbSet<CarritoCompraEntity> CarritoCompra { get; set; }
        public DbSet<CarritoCompraItemEntity> CarritoCompraItem { get; set; }
    }
}
