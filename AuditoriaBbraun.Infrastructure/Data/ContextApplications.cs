using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditoriaBbraun.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace AuditoriaBbraun.Infrastructure.Data
{
    public class ContextApplications : DbContext
    {
        public ContextApplications(DbContextOptions<ContextApplications> options) : base(options)
        {
        }

        // AGREGAR TODOS LOS DbSet AQUÍ
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Dispositivo> Dispositivos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }
        public DbSet<Auditoria> Auditoria { get; set; }
        public DbSet<EstadoLed> EstadoLEDs { get; set; }

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
                entity.Property(e => e.Estado).HasMaxLength(50);
            });

            // Configurar Clientes
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);

            });

            // Configurar Dispositivos
            modelBuilder.Entity<Dispositivo>(entity =>
            {
                entity.HasKey(d => d.Id);

                entity.Property(d => d.NumeroSerie)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(d => d.NumeroSerie)
                    .IsUnique();

                entity.Property(d => d.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.Tipo)
                    .HasMaxLength(20)
                    .HasConversion<string>();

                entity.Property(d => d.Estado)
                    .HasMaxLength(20)
                    .HasDefaultValue("Activo")
                    .HasConversion<string>();

                entity.Property(d => d.Ubicacion)
                    .HasMaxLength(200);

                entity.Property(d => d.Configuracion)
                    .HasColumnType("nvarchar(max)");

                // Relaciones
                entity.HasOne(d => d.Usuario)
                    .WithMany()
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(d => d.CreadoPor)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ModificadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(d => d.ModificadoPor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.CodigoBarras)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(t => t.Peso)
                    .HasColumnType("decimal(10,3)")
                    .IsRequired();

                entity.Property(t => t.Largo)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(t => t.Ancho)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(t => t.Alto)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(t => t.Volumen)
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(t => t.DireccionRodillo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(t => t.NumeroSerieDispositivo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(t => t.Estado)
                    .HasMaxLength(20)
                    .HasDefaultValue("Procesado")
                    .HasConversion<string>();

                // Relaciones
                entity.HasOne(t => t.Dispositivo)
                    .WithMany(d => d.Transacciones)
                    .HasForeignKey(t => t.DispositivoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Orden)
                    .WithMany(o => o.Transacciones)
                    .HasForeignKey(t => t.OrdenId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(t => t.CreadoPor)
                    .OnDelete(DeleteBehavior.Restrict);

                // Índices
                entity.HasIndex(t => t.CodigoBarras);
                entity.HasIndex(t => t.FechaHora);
                entity.HasIndex(t => t.DispositivoId);
                entity.HasIndex(t => t.OrdenId);
                entity.HasIndex(t => t.Estado);
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.NumeroOrden)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(o => o.NumeroOrden)
                    .IsUnique();

                entity.Property(o => o.Estado)
                    .HasMaxLength(20)
                    .HasDefaultValue("Pendiente")
                    .HasConversion<string>();

                entity.Property(o => o.TotalPeso)
                    .HasColumnType("decimal(10,3)");

                entity.Property(o => o.TotalVolumen)
                    .HasColumnType("decimal(10,2)");

                // Relaciones
                entity.HasOne(o => o.Cliente)
                    .WithMany(c => c.Ordenes)
                    .HasForeignKey(o => o.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(o => o.CreadoPorUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.AsignadoAUsuario)
                    .WithMany()
                    .HasForeignKey(o => o.AsignadoA)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Codigo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(c => c.Codigo)
                    .IsUnique();

                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);


                entity.HasOne(c => c.CreadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(c => c.CreadoPorUsuario)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.Tipo)
                    .HasMaxLength(20)
                    .HasConversion<string>();

                entity.Property(i => i.RutaArchivo)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(i => i.NombreArchivo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(i => i.Formato)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(i => i.Metadata)
                    .HasColumnType("nvarchar(max)");

                // Relación
                entity.HasOne(i => i.Transaccion)
                    .WithMany(t => t.Imagenes)
                    .HasForeignKey(i => i.TransaccionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Clave)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(c => c.Clave)
                    .IsUnique();

                entity.Property(c => c.Valor)
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");



                // Relación
                entity.HasOne(c => c.ModificadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(c => c.ModificadoPor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Auditoria>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Accion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(a => a.Entidad)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(a => a.EntidadId)
                    .HasMaxLength(100);

                entity.Property(a => a.Detalles)
                    .HasColumnType("nvarchar(max)");

                // Relación
                entity.HasOne(a => a.Usuario)
                    .WithMany()
                    .HasForeignKey(a => a.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Índices
                entity.HasIndex(a => a.Fecha);
                entity.HasIndex(a => a.UsuarioId);
                entity.HasIndex(a => a.Accion);
            });

            modelBuilder.Entity<EstadoLed>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .HasConversion<string>();

                // Restricción única por dispositivo y slot
                entity.HasIndex(e => new { e.DispositivoId, e.NumeroSlot })
                    .IsUnique();

                // Relaciones
                entity.HasOne(e => e.Dispositivo)
                    .WithMany(d => d.EstadoLEDs)
                    .HasForeignKey(e => e.DispositivoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.ActualizadoPorUsuario)
                    .WithMany()
                    .HasForeignKey(e => e.ActualizadoPor)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Procesar entidades antes de guardar
            ProcesarEntidadesAntesDeGuardar();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ProcesarEntidadesAntesDeGuardar()
        {
            var entidades = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entidad in entidades)
            {
                // Auditoría automática
                if (entidad.Entity is Auditoria)
                    continue;

                // Si la entidad tiene fechas de creación/modificación
                if (entidad.Entity is BaseEntity baseEntity)
                {
                    if (entidad.State == EntityState.Added)
                    {
                        baseEntity.FechaCreacion = DateTime.UtcNow;
                    }
                    else if (entidad.State == EntityState.Modified)
                    {
                        baseEntity.FechaModificacion = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
