using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ImagenGa.Models;

public partial class DbpruebaContext : DbContext
{
    public DbpruebaContext()
    {
    }

    public DbpruebaContext(DbContextOptions<DbpruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Imagen> Imagenes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }
    public virtual DbSet<Rol> Roles { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagen__09889210231F000B");

            entity.ToTable("Imagen");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaE).HasColumnType("datetime");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__09889210231F000B");

            entity.ToTable("Rol");

        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PK__Registro__09889210231F000B");

            entity.ToTable("Registro");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdAccion)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdImagen)
                .HasMaxLength(50)
                .IsUnicode(false);

            // Configuración de la llave foránea con Usuario
            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Registro)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Usuario");

            // Configuración de la llave foránea con Imagen
            entity.HasOne(d => d.Imagen)
                .WithMany(p => p.Registro)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_Imagen");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF9756AE1415");

            entity.ToTable("Usuario");

            entity.Property(e => e.Clave)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Usuario");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
