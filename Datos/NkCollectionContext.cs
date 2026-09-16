using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos.Modelos;

namespace Nk_Colletion_New.Datos;

public partial class NkCollectionContext : DbContext
{
    public NkCollectionContext(DbContextOptions<NkCollectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Credito> Creditos { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleDevolucionVentum> DetalleDevolucionVenta { get; set; }

    public virtual DbSet<DetallePagoCredito> DetallePagoCreditos { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<DevolucionVentum> DevolucionVenta { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("caja_pkey");

            entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Cajas).HasConstraintName("caja_id_compra_fkey");

            entity.HasOne(d => d.IdDetallePagoCreditoNavigation).WithMany(p => p.Cajas).HasConstraintName("caja_id_detalle_pago_credito_fkey");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Cajas).HasConstraintName("caja_id_venta_fkey");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categoria_pkey");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("cliente_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("compra_pkey");

            entity.Property(e => e.FechaCompra).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Impuesto).HasDefaultValueSql("0");
            entity.Property(e => e.Subtotal).HasDefaultValueSql("0");
            entity.Property(e => e.Total).HasDefaultValueSql("0");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras).HasConstraintName("compra_id_proveedor_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras).HasConstraintName("compra_id_usuario_fkey");
        });

        modelBuilder.Entity<Credito>(entity =>
        {
            entity.HasKey(e => e.IdCredito).HasName("credito_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaSolicitud).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.FechaVencimiento).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Creditos).HasConstraintName("credito_id_venta_fkey");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("detalle_compra_pkey");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras).HasConstraintName("detalle_compra_id_compra_fkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras).HasConstraintName("detalle_compra_id_producto_fkey");
        });

        modelBuilder.Entity<DetalleDevolucionVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDevVenta).HasName("detalle_devolucion_venta_pkey");

            entity.HasOne(d => d.IdDevolucionVentaNavigation).WithMany(p => p.DetalleDevolucionVenta).HasConstraintName("detalle_devolucion_venta_id_devolucion_venta_fkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleDevolucionVenta).HasConstraintName("detalle_devolucion_venta_id_producto_fkey");
        });

        modelBuilder.Entity<DetallePagoCredito>(entity =>
        {
            entity.HasKey(e => e.IdDetallePagoCredito).HasName("detalle_pago_credito_pkey");

            entity.Property(e => e.FechaPago).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdCreditoNavigation).WithMany(p => p.DetallePagoCreditos).HasConstraintName("detalle_pago_credito_id_credito_fkey");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("detalle_venta_pkey");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("detalle_venta_id_producto_fkey");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("detalle_venta_id_venta_fkey");
        });

        modelBuilder.Entity<DevolucionVentum>(entity =>
        {
            entity.HasKey(e => e.IdDevolucionVenta).HasName("devolucion_venta_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.DevolucionVenta).HasConstraintName("devolucion_venta_id_cliente_fkey");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DevolucionVenta).HasConstraintName("devolucion_venta_id_venta_fkey");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("inventario_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios).HasConstraintName("inventario_id_producto_fkey");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("marca_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("producto_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_DATE");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos).HasConstraintName("producto_id_categoria_fkey");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos).HasConstraintName("producto_id_marca_fkey");

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos).HasConstraintName("producto_id_tipo_producto_fkey");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("proveedor_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("rol_pkey");
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("tipo_producto_pkey");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.TipoProductos).HasConstraintName("tipo_producto_id_categoria_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios).HasConstraintName("usuario_id_rol_fkey");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("venta_pkey");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta).HasConstraintName("venta_id_cliente_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta).HasConstraintName("venta_id_usuario_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
