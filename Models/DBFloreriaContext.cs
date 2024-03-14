using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FloreriaMvc.Models;

public partial class DBFloreriaContext : DbContext
{
    public DBFloreriaContext()
    {
    }

    public DBFloreriaContext(DbContextOptions<DBFloreriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesVentum> DetallesVenta { get; set; }

    public virtual DbSet<Insumo> Insumos { get; set; }

    public virtual DbSet<InsumoProducto> InsumoProductos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<ProductosFinale> ProductosFinales { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=floreriadb.c342oe8m4j5s.us-east-2.rds.amazonaws.com;Database=db_floreria;  Uid=admin; Pwd=rootFloreria0000; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.NombreCategoria).HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__C2FF24BD8D573901");

            entity.Property(e => e.ClienteId).HasColumnName("clienteID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(75)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetallesVentum>(entity =>
        {
            entity.HasKey(e => e.DetallesVentaId).HasName("PK__detalles__524D0C93059ED640");

            entity.ToTable("detallesVenta");

            entity.Property(e => e.DetallesVentaId).HasColumnName("detallesVentaID");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.ProductoFinalId).HasColumnName("productoFinalID");
            entity.Property(e => e.VentaId).HasColumnName("ventaID");

            entity.HasOne(d => d.ProductoFinal).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.ProductoFinalId)
                .HasConstraintName("FK_dv_productoID");

            entity.HasOne(d => d.Venta).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.VentaId)
                .HasConstraintName("FK_dv_ventaID");
        });

        modelBuilder.Entity<Insumo>(entity =>
        {
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<InsumoProducto>(entity =>
        {
            entity.Property(e => e.CantidadNecesaria).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Insumo).WithMany(p => p.InsumoProductos).HasForeignKey(d => d.InsumoId);

            entity.HasOne(d => d.ProductoFinal).WithMany(p => p.InsumoProductos).HasForeignKey(d => d.ProductoFinalId);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.Property(e => e.PedidoId).HasColumnName("pedidoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Detalles)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DiaEntrega).HasColumnName("diaEntrega");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.HoraEntrega)
                .HasColumnType("datetime")
                .HasColumnName("horaEntrega");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_pedidos_clienteid");
        });

        modelBuilder.Entity<ProductosFinale>(entity =>
        {
            entity.HasKey(e => e.ProductoFinalId);

            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Factor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precioVenta");

            entity.HasOne(d => d.Categoria).WithMany(p => p.ProductosFinales)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.VentaId).HasName("PK__Venta__40B8EB348785115C");

            entity.Property(e => e.VentaId).HasColumnName("ventaID");
            entity.Property(e => e.ClienteId).HasColumnName("clienteID");
            entity.Property(e => e.FechaVenta).HasColumnName("fechaVenta");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("totalVenta");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("fk_clienteID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
