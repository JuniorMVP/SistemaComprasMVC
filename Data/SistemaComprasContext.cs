using Microsoft.EntityFrameworkCore;
using SistemaComprasMVC.Models;

namespace SistemaComprasMVC.Data
{
    public class SistemaComprasContext : DbContext
    {
        public SistemaComprasContext(DbContextOptions<SistemaComprasContext> options)
            : base(options)
        {
        }

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<UnidadDeMedida> UnidadesDeMedida { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Articulo> Articulos { get; set; }

        // Añadir el DbSet para la tabla OrdenDeCompra
        public DbSet<OrdenDeCompra> OrdenesDeCompra { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Articulo y UnidadDeMedida
            modelBuilder.Entity<Articulo>()
                .HasOne(a => a.UnidadDeMedida)
                .WithMany()
                .HasForeignKey(a => a.UnidadDeMedidaId)
                .OnDelete(DeleteBehavior.Restrict); // Cambiar a Restrict para evitar ciclos

            // Configuración de la relación entre OrdenDeCompra y Articulo
            modelBuilder.Entity<OrdenDeCompra>()
                .HasOne(o => o.Articulo)
                .WithMany() // Si no es necesario que un Artículo tenga muchas órdenes de compra, se puede omitir el WithMany()
                .HasForeignKey(o => o.ArticuloId)
                .OnDelete(DeleteBehavior.Restrict); // Cambia a Restrict si no quieres que se borren las ordenes cuando se borra el Artículo

            // Configuración de la relación entre OrdenDeCompra y UnidadDeMedida
            modelBuilder.Entity<OrdenDeCompra>()
                .HasOne(o => o.UnidadDeMedida)
                .WithMany() // Si no es necesario que una Unidad de Medida tenga muchas órdenes de compra, se puede omitir el WithMany()
                .HasForeignKey(o => o.UnidadDeMedidaId)
                .OnDelete(DeleteBehavior.Restrict); // Cambia a Restrict si no quieres que se borren las ordenes cuando se borra la UnidadDeMedida

            // Añadir la precisión para la propiedad decimal CostoUnitario
            modelBuilder.Entity<OrdenDeCompra>()
                .Property(o => o.CostoUnitario)
                .HasPrecision(18, 2); // 18 dígitos con 2 decimales

            base.OnModelCreating(modelBuilder);
        }
    }
}
