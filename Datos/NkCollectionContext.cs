using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos.Modelos;
using ColorModel = Nk_Colletion_New.Datos.Modelos.Color;

namespace Nk_Colletion_New.Datos;

public partial class NkCollectionContext : DbContext
{
    public NkCollectionContext(DbContextOptions<NkCollectionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlertaStock> AlertaStocks { get; set; }

    public virtual DbSet<AperturaCaja> AperturaCajas { get; set; }

    public virtual DbSet<ArqueoCaja> ArqueoCajas { get; set; }

    public virtual DbSet<Caja> Cajas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ColorModel> Colors { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleArqueo> DetalleArqueos { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Egreso> Egresos { get; set; }

    public virtual DbSet<LogSistema> LogSistemas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

    public virtual DbSet<PagoVentum> PagoVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoVariante> ProductoVariantes { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Talla> Tallas { get; set; }

    public virtual DbSet<TipoCambioBcn> TipoCambioBcns { get; set; }

    public virtual DbSet<TipoEgreso> TipoEgresos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertaStock>(entity =>
        {
            entity.HasKey(e => e.IdAlerta).HasName("alerta_stock_pkey");

            entity.HasIndex(e => new { e.IdVariante, e.FechaAlerta }, "idx_alerta_stock_pendiente")
                .IsDescending(false, true)
                .HasFilter("(atendida IS FALSE)");

            entity.Property(e => e.IdAlerta).UseIdentityAlwaysColumn();
            entity.Property(e => e.FechaAlerta).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.AlertaStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_alerta_stock_variante");
        });

        modelBuilder.Entity<AperturaCaja>(entity =>
        {
            entity.HasKey(e => e.IdAperturaCaja).HasName("apertura_caja_pkey");

            entity.HasIndex(e => e.FechaApertura, "idx_apertura_caja_activa")
                .IsDescending()
                .HasFilter("(estado = true)");

            entity.HasIndex(e => e.FechaApertura, "idx_apertura_caja_activa_fecha")
                .IsDescending()
                .HasFilter("(estado IS TRUE)");

            entity.HasIndex(e => e.IdCaja, "ux_apertura_caja_una_activa_por_caja")
                .IsUnique()
                .HasFilter("(estado IS TRUE)");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaApertura).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.MontoApertura).HasDefaultValue(0m);

            entity.HasOne(d => d.IdCajaNavigation).WithMany(p => p.AperturaCajas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_apertura_caja");
        });

        modelBuilder.Entity<ArqueoCaja>(entity =>
        {
            entity.HasKey(e => e.IdArqueo).HasName("arqueo_caja_pkey");

            entity.HasIndex(e => e.IdAperturaCaja, "ux_arqueo_unico_activo_por_apertura")
                .IsUnique()
                .HasFilter("(estado IS TRUE)");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaArqueo).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithMany(p => p.ArqueoCajas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_arqueo_apertura_caja");
        });

        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("caja_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Caja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_caja_usuario");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categoria_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("cliente_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<ColorModel>(entity =>
        {
            entity.HasKey(e => e.IdColor).HasName("color_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("compra_pkey");

            entity.HasIndex(e => e.FechaCompra, "idx_compra_fecha_activa")
                .IsDescending()
                .HasFilter("(estado IS TRUE)");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCompra).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compra_proveedor");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compra_usuario");
        });

        modelBuilder.Entity<DetalleArqueo>(entity =>
        {
            entity.HasKey(e => e.IdDetalleArqueo).HasName("detalle_arqueo_pkey");

            entity.Property(e => e.Moneda).HasDefaultValueSql("'NIO'::character varying");
            entity.Property(e => e.TasaCambio).HasDefaultValue(1m);

            entity.HasOne(d => d.IdArqueoNavigation).WithMany(p => p.DetalleArqueos).HasConstraintName("fk_detalle_arqueo_arqueo");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("detalle_compra_pkey");

            entity.Property(e => e.Subtotal).HasComputedColumnSql("((cantidad)::numeric * precio_unitario)", true);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras).HasConstraintName("fk_detalle_compra_compra");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.DetalleCompras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_compra_variante");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("detalle_venta_pkey");

            entity.Property(e => e.Subtotal).HasComputedColumnSql("((cantidad)::numeric * precio_unitario)", true);

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.DetalleVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_venta_variante");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta).HasConstraintName("fk_detalle_venta_venta");
        });

        modelBuilder.Entity<Egreso>(entity =>
        {
            entity.HasKey(e => e.IdEgreso).HasName("egreso_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaEgreso).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithMany(p => p.Egresos).HasConstraintName("fk_egreso_apertura_caja");

            entity.HasOne(d => d.IdTipoEgresoNavigation).WithMany(p => p.Egresos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_egreso_tipo");
        });

        modelBuilder.Entity<LogSistema>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("log_sistema_pkey");

            entity.Property(e => e.IdLog).UseIdentityAlwaysColumn();
            entity.Property(e => e.Fecha).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Txid).HasDefaultValueSql("txid_current()");
            entity.Property(e => e.UsuarioBd).HasDefaultValueSql("CURRENT_USER");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("marca_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("metodo_pago_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("movimiento_inventario_pkey");

            entity.Property(e => e.IdMovimiento).UseIdentityAlwaysColumn();
            entity.Property(e => e.FechaMovimiento).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Txid).HasDefaultValueSql("txid_current()");
            entity.Property(e => e.UsuarioBd).HasDefaultValueSql("CURRENT_USER");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.MovimientoInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movimiento_variante");
        });

        modelBuilder.Entity<PagoVentum>(entity =>
        {
            entity.HasKey(e => e.IdPagoVenta).HasName("pago_venta_pkey");

            entity.Property(e => e.FechaPago).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.PagoVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_venta_metodo");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.PagoVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_venta_venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("producto_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos).HasConstraintName("fk_producto_categoria");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos).HasConstraintName("fk_producto_marca");
        });

        modelBuilder.Entity<ProductoVariante>(entity =>
        {
            entity.HasKey(e => e.IdVariante).HasName("producto_variante_pkey");

            entity.HasIndex(e => new { e.IdProducto, e.IdTalla, e.IdColor }, "ux_producto_variante_activa_combinacion")
                .IsUnique()
                .HasFilter("(estado IS TRUE)")
                .AreNullsDistinct(false);

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.ProductoVariantes).HasConstraintName("fk_variante_color");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoVariantes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_variante_producto");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.ProductoVariantes).HasConstraintName("fk_variante_talla");
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

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.IdTalla).HasName("talla_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<TipoCambioBcn>(entity =>
        {
            entity.HasKey(e => e.Fecha).HasName("tipo_cambio_bcn_pkey");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Oficial).HasDefaultValue(true);
        });

        modelBuilder.Entity<TipoEgreso>(entity =>
        {
            entity.HasKey(e => e.IdTipoEgreso).HasName("tipo_egreso_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("venta_pkey");

            entity.HasIndex(e => e.FechaVenta, "idx_venta_fecha_activa")
                .IsDescending()
                .HasFilter("(estado = true)");

            entity.HasIndex(e => e.NumeroComprobante, "ux_venta_numero_comprobante")
                .IsUnique()
                .HasFilter("(numero_comprobante IS NOT NULL)");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaVenta).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_venta_apertura_caja");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta).HasConstraintName("fk_venta_cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
