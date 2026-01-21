using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditoriaBbraun.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaBbraun.Infrastructure.Data
{
    public class ContextApplications : DbContext
    {
        public ContextApplications(DbContextOptions<ContextApplications> options): base(options)
        {
        }

        // AGREGAR TODOS LOS DbSet AQUÍ
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Transacciones
            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CodigoBarras).IsRequired().HasMaxLength(500);
                entity.Property(e => e.NumeroSerieDispositivo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FechaHora).IsRequired();
            });

            // Configurar Ordenes
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroOrden).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estado).HasMaxLength(50);
            });

            // Configurar Clientes
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Identificacion).HasMaxLength(50);
                entity.HasIndex(e => e.Identificacion).IsUnique();
            });

            // Configurar Dispositivos
            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroSerie).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.NumeroSerie).IsUnique();
            });
        }
    }
}
