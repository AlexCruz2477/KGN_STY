using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nk_Colletion_New.Models;

public partial class NkCollectionContext : DbContext
{
    public NkCollectionContext()
    {
    }

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

    public virtual DbSet<Color> Colors { get; set; }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=NK_STYLE_POINT;Username=postgres;Password=131007");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlertaStock>(entity =>
        {
            entity.HasKey(e => e.IdAlerta).HasName("alerta_stock_pkey");

            entity.ToTable("alerta_stock");

            entity.HasIndex(e => new { e.IdVariante, e.FechaAlerta }, "idx_alerta_stock_pendiente")
                .IsDescending(false, true)
                .HasFilter("(atendida IS FALSE)");

            entity.Property(e => e.IdAlerta)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_alerta");
            entity.Property(e => e.Atendida)
                .HasDefaultValue(false)
                .HasColumnName("atendida");
            entity.Property(e => e.FechaAlerta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_alerta");
            entity.Property(e => e.FechaAtendida)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_atendida");
            entity.Property(e => e.IdVariante).HasColumnName("id_variante");
            entity.Property(e => e.StockActual).HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo).HasColumnName("stock_minimo");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.AlertaStocks)
                .HasForeignKey(d => d.IdVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_alerta_stock_variante");
        });

        modelBuilder.Entity<AperturaCaja>(entity =>
        {
            entity.HasKey(e => e.IdAperturaCaja).HasName("apertura_caja_pkey");

            entity.ToTable("apertura_caja");

            entity.HasIndex(e => e.FechaApertura, "idx_apertura_caja_activa_fecha")
                .IsDescending()
                .HasFilter("(estado IS TRUE)");

            entity.HasIndex(e => e.IdCaja, "ux_apertura_caja_una_activa_por_caja")
                .IsUnique()
                .HasFilter("(estado IS TRUE)");

            entity.Property(e => e.IdAperturaCaja).HasColumnName("id_apertura_caja");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaApertura)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_apertura");
            entity.Property(e => e.FechaCierre)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cierre");
            entity.Property(e => e.IdCaja).HasColumnName("id_caja");
            entity.Property(e => e.MontoApertura)
                .HasPrecision(12, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("monto_apertura");

            entity.HasOne(d => d.IdCajaNavigation).WithOne(p => p.AperturaCaja)
                .HasForeignKey<AperturaCaja>(d => d.IdCaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_apertura_caja");
        });

        modelBuilder.Entity<ArqueoCaja>(entity =>
        {
            entity.HasKey(e => e.IdArqueo).HasName("arqueo_caja_pkey");

            entity.ToTable("arqueo_caja");

            entity.HasIndex(e => e.IdAperturaCaja, "idx_arqueo_id_apertura_caja");

            entity.HasIndex(e => e.IdAperturaCaja, "ux_arqueo_unico_activo_por_apertura")
                .IsUnique()
                .HasFilter("(estado IS TRUE)");

            entity.Property(e => e.IdArqueo).HasColumnName("id_arqueo");
            entity.Property(e => e.Diferencia)
                .HasPrecision(12, 2)
                .HasColumnName("diferencia");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaArqueo)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_arqueo");
            entity.Property(e => e.IdAperturaCaja).HasColumnName("id_apertura_caja");
            entity.Property(e => e.Observacion)
                .HasMaxLength(250)
                .HasColumnName("observacion");
            entity.Property(e => e.SaldoContado)
                .HasPrecision(12, 2)
                .HasColumnName("saldo_contado");
            entity.Property(e => e.SaldoEsperado)
                .HasPrecision(12, 2)
                .HasColumnName("saldo_esperado");
            entity.Property(e => e.TotalEgresos)
                .HasPrecision(12, 2)
                .HasColumnName("total_egresos");
            entity.Property(e => e.TotalVentas)
                .HasPrecision(12, 2)
                .HasColumnName("total_ventas");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithOne(p => p.ArqueoCaja)
                .HasForeignKey<ArqueoCaja>(d => d.IdAperturaCaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_arqueo_apertura_caja");
        });

        modelBuilder.Entity<Caja>(entity =>
        {
            entity.HasKey(e => e.IdCaja).HasName("caja_pkey");

            entity.ToTable("caja");

            entity.HasIndex(e => e.IdUsuario, "caja_id_usuario_key").IsUnique();

            entity.Property(e => e.IdCaja).HasColumnName("id_caja");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.NumeroCaja)
                .HasMaxLength(50)
                .HasColumnName("numero_caja");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Caja)
                .HasForeignKey<Caja>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_caja_usuario");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.HasIndex(e => e.NombreCategoria, "categoria_nombre_categoria_key").IsUnique();

            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .HasColumnName("nombre_categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("cliente_pkey");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Cedula, "cliente_cedula_key").IsUnique();

            entity.HasIndex(e => new { e.Nombre, e.Apellido }, "idx_cliente_nombre_apellido");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.IdColor).HasName("color_pkey");

            entity.ToTable("color");

            entity.Property(e => e.IdColor).HasColumnName("id_color");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.NombreColor)
                .HasMaxLength(50)
                .HasColumnName("nombre_color");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("compra_pkey");

            entity.ToTable("compra");

            entity.HasIndex(e => e.FechaCompra, "idx_compra_fecha_activa")
                .IsDescending()
                .HasFilter("(estado IS TRUE)");

            entity.HasIndex(e => new { e.IdProveedor, e.FechaCompra }, "idx_compra_proveedor_fecha").IsDescending(false, true);

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCompra)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_compra");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Impuesto)
                .HasPrecision(12, 2)
                .HasColumnName("impuesto");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(50)
                .HasColumnName("numero_factura");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compra_proveedor");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compra_usuario");
        });

        modelBuilder.Entity<DetalleArqueo>(entity =>
        {
            entity.HasKey(e => e.IdDetalleArqueo).HasName("detalle_arqueo_pkey");

            entity.ToTable("detalle_arqueo");

            entity.HasIndex(e => e.IdArqueo, "idx_detalle_arqueo_id_arqueo");

            entity.Property(e => e.IdDetalleArqueo).HasColumnName("id_detalle_arqueo");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Denominacion)
                .HasPrecision(12, 2)
                .HasColumnName("denominacion");
            entity.Property(e => e.IdArqueo).HasColumnName("id_arqueo");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'NIO'::character varying")
                .HasColumnName("moneda");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.TasaCambio)
                .HasPrecision(12, 4)
                .HasDefaultValueSql("1")
                .HasColumnName("tasa_cambio");

            entity.HasOne(d => d.IdArqueoNavigation).WithMany(p => p.DetalleArqueos)
                .HasForeignKey(d => d.IdArqueo)
                .HasConstraintName("fk_detalle_arqueo_arqueo");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("detalle_compra_pkey");

            entity.ToTable("detalle_compra");

            entity.HasIndex(e => e.IdCompra, "idx_detalle_compra_id_compra");

            entity.HasIndex(e => e.IdVariante, "idx_detalle_compra_id_variante");

            entity.Property(e => e.IdDetalleCompra).HasColumnName("id_detalle_compra");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdVariante).HasColumnName("id_variante");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(12, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((cantidad)::numeric * precio_unitario)", true)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("fk_detalle_compra_compra");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_compra_variante");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("detalle_venta_pkey");

            entity.ToTable("detalle_venta");

            entity.HasIndex(e => e.IdVariante, "idx_detalle_venta_id_variante");

            entity.HasIndex(e => e.IdVenta, "idx_detalle_venta_id_venta");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("id_detalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdVariante).HasColumnName("id_variante");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(12, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasComputedColumnSql("((cantidad)::numeric * precio_unitario)", true)
                .HasColumnName("subtotal");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_detalle_venta_variante");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_detalle_venta_venta");
        });

        modelBuilder.Entity<Egreso>(entity =>
        {
            entity.HasKey(e => e.IdEgreso).HasName("egreso_pkey");

            entity.ToTable("egreso");

            entity.HasIndex(e => new { e.IdAperturaCaja, e.Estado }, "idx_egreso_apertura_estado");

            entity.HasIndex(e => e.FechaEgreso, "idx_egreso_fecha").IsDescending();

            entity.Property(e => e.IdEgreso).HasColumnName("id_egreso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEgreso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_egreso");
            entity.Property(e => e.IdAperturaCaja).HasColumnName("id_apertura_caja");
            entity.Property(e => e.IdTipoEgreso).HasColumnName("id_tipo_egreso");
            entity.Property(e => e.Monto)
                .HasPrecision(12, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithMany(p => p.Egresos)
                .HasForeignKey(d => d.IdAperturaCaja)
                .HasConstraintName("fk_egreso_apertura_caja");

            entity.HasOne(d => d.IdTipoEgresoNavigation).WithMany(p => p.Egresos)
                .HasForeignKey(d => d.IdTipoEgreso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_egreso_tipo");
        });

        modelBuilder.Entity<LogSistema>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("log_sistema_pkey");

            entity.ToTable("log_sistema");

            entity.HasIndex(e => e.Fecha, "idx_log_sistema_fecha").IsDescending();

            entity.HasIndex(e => new { e.Modulo, e.Fecha }, "idx_log_sistema_modulo_fecha").IsDescending(false, true);

            entity.HasIndex(e => new { e.TablaAfectada, e.IdRegistro }, "idx_log_sistema_tabla_registro");

            entity.Property(e => e.IdLog)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_log");
            entity.Property(e => e.Accion)
                .HasMaxLength(20)
                .HasColumnName("accion");
            entity.Property(e => e.DatosAnteriores)
                .HasColumnType("jsonb")
                .HasColumnName("datos_anteriores");
            entity.Property(e => e.DatosNuevos)
                .HasColumnType("jsonb")
                .HasColumnName("datos_nuevos");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.IdRegistro).HasColumnName("id_registro");
            entity.Property(e => e.Modulo)
                .HasMaxLength(30)
                .HasColumnName("modulo");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(80)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.Txid)
                .HasDefaultValueSql("txid_current()")
                .HasColumnName("txid");
            entity.Property(e => e.UsuarioApp).HasColumnName("usuario_app");
            entity.Property(e => e.UsuarioBd)
                .HasDefaultValueSql("CURRENT_USER")
                .HasColumnName("usuario_bd");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("marca_pkey");

            entity.ToTable("marca");

            entity.HasIndex(e => e.Nombre, "marca_nombre_key").IsUnique();

            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("metodo_pago_pkey");

            entity.ToTable("metodo_pago");

            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("movimiento_inventario_pkey");

            entity.ToTable("movimiento_inventario");

            entity.HasIndex(e => new { e.ReferenciaTabla, e.ReferenciaId }, "idx_movimiento_inventario_referencia");

            entity.HasIndex(e => new { e.IdVariante, e.FechaMovimiento }, "idx_movimiento_inventario_variante_fecha").IsDescending(false, true);

            entity.Property(e => e.IdMovimiento)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.IdVariante).HasColumnName("id_variante");
            entity.Property(e => e.ReferenciaId).HasColumnName("referencia_id");
            entity.Property(e => e.ReferenciaTabla)
                .HasMaxLength(50)
                .HasColumnName("referencia_tabla");
            entity.Property(e => e.StockAnterior).HasColumnName("stock_anterior");
            entity.Property(e => e.StockNuevo).HasColumnName("stock_nuevo");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(50)
                .HasColumnName("tipo_movimiento");
            entity.Property(e => e.Txid)
                .HasDefaultValueSql("txid_current()")
                .HasColumnName("txid");
            entity.Property(e => e.UsuarioBd)
                .HasDefaultValueSql("CURRENT_USER")
                .HasColumnName("usuario_bd");

            entity.HasOne(d => d.IdVarianteNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdVariante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movimiento_variante");
        });

        modelBuilder.Entity<PagoVentum>(entity =>
        {
            entity.HasKey(e => e.IdPagoVenta).HasName("pago_venta_pkey");

            entity.ToTable("pago_venta");

            entity.HasIndex(e => e.IdVenta, "idx_pago_venta_id_venta");

            entity.Property(e => e.IdPagoVenta).HasColumnName("id_pago_venta");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Monto)
                .HasPrecision(12, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.PagoVenta)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_venta_metodo");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.PagoVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_venta_venta");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(150)
                .HasColumnName("nombre_producto");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fk_producto_categoria");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("fk_producto_marca");
        });

        modelBuilder.Entity<ProductoVariante>(entity =>
        {
            entity.HasKey(e => e.IdVariante).HasName("producto_variante_pkey");

            entity.ToTable("producto_variante");

            entity.HasIndex(e => new { e.IdProducto, e.IdVariante }, "idx_producto_variante_producto_variante");

            entity.HasIndex(e => e.Codigo, "producto_variante_codigo_key").IsUnique();

            entity.HasIndex(e => new { e.IdProducto, e.IdTalla, e.IdColor }, "ux_producto_variante_activa_combinacion")
                .IsUnique()
                .HasFilter("(estado IS TRUE)")
                .AreNullsDistinct(false);

            entity.Property(e => e.IdVariante).HasColumnName("id_variante");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("codigo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.IdColor).HasColumnName("id_color");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdTalla).HasColumnName("id_talla");
            entity.Property(e => e.PrecioCompra)
                .HasPrecision(12, 2)
                .HasColumnName("precio_compra");
            entity.Property(e => e.PrecioVenta)
                .HasPrecision(12, 2)
                .HasColumnName("precio_venta");
            entity.Property(e => e.StockActual)
                .HasDefaultValue(0)
                .HasColumnName("stock_actual");
            entity.Property(e => e.StockMinimo)
                .HasDefaultValue(0)
                .HasColumnName("stock_minimo");

            entity.HasOne(d => d.IdColorNavigation).WithMany(p => p.ProductoVariantes)
                .HasForeignKey(d => d.IdColor)
                .HasConstraintName("fk_variante_color");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoVariantes)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_variante_producto");

            entity.HasOne(d => d.IdTallaNavigation).WithMany(p => p.ProductoVariantes)
                .HasForeignKey(d => d.IdTalla)
                .HasConstraintName("fk_variante_talla");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("proveedor_pkey");

            entity.ToTable("proveedor");

            entity.HasIndex(e => e.Ruc, "proveedor_ruc_key").IsUnique();

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.Ruc)
                .HasMaxLength(50)
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("rol_pkey");

            entity.ToTable("rol");

            entity.HasIndex(e => e.Nombre, "rol_nombre_key").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Talla>(entity =>
        {
            entity.HasKey(e => e.IdTalla).HasName("talla_pkey");

            entity.ToTable("talla");

            entity.Property(e => e.IdTalla).HasColumnName("id_talla");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.NombreTalla)
                .HasMaxLength(50)
                .HasColumnName("nombre_talla");
        });

        modelBuilder.Entity<TipoCambioBcn>(entity =>
        {
            entity.HasKey(e => e.Fecha).HasName("tipo_cambio_bcn_pkey");

            entity.ToTable("tipo_cambio_bcn");

            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Fuente)
                .HasMaxLength(120)
                .HasDefaultValueSql("'Banco Central de Nicaragua (BCN)'::character varying")
                .HasColumnName("fuente");
            entity.Property(e => e.Oficial)
                .HasDefaultValue(true)
                .HasColumnName("oficial");
            entity.Property(e => e.Referencia).HasColumnName("referencia");
            entity.Property(e => e.TasaNioPorUsd)
                .HasPrecision(12, 4)
                .HasColumnName("tasa_nio_por_usd");
        });

        modelBuilder.Entity<TipoEgreso>(entity =>
        {
            entity.HasKey(e => e.IdTipoEgreso).HasName("tipo_egreso_pkey");

            entity.ToTable("tipo_egreso");

            entity.Property(e => e.IdTipoEgreso).HasColumnName("id_tipo_egreso");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Nombre, "idx_usuario_nombre");

            entity.HasIndex(e => e.Cedula, "usuario_cedula_key").IsUnique();

            entity.HasIndex(e => e.Correo, "usuario_correo_key").IsUnique();

            entity.HasIndex(e => e.Usuario1, "usuario_usuario_key").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Cedula)
                .HasMaxLength(20)
                .HasColumnName("cedula");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHoraRecuperacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_hora_recuperacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.TokenRecuperacion)
                .HasMaxLength(255)
                .HasColumnName("token_recuperacion");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_rol");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("venta_pkey");

            entity.ToTable("venta");

            entity.HasIndex(e => new { e.IdAperturaCaja, e.Estado }, "idx_venta_apertura_estado");

            entity.HasIndex(e => e.FechaVenta, "idx_venta_fecha").IsDescending();

            entity.HasIndex(e => e.FechaVenta, "idx_venta_fecha_activa")
                .IsDescending()
                .HasFilter("(estado IS TRUE)");

            entity.HasIndex(e => e.NumeroComprobante, "ux_venta_numero_comprobante")
                .IsUnique()
                .HasFilter("(numero_comprobante IS NOT NULL)");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Descuento)
                .HasPrecision(12, 2)
                .HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdAperturaCaja).HasColumnName("id_apertura_caja");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Iva)
                .HasPrecision(12, 2)
                .HasColumnName("iva");
            entity.Property(e => e.NumeroComprobante)
                .HasMaxLength(50)
                .HasColumnName("numero_comprobante");
            entity.Property(e => e.Subtotal)
                .HasPrecision(12, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.TotalVenta)
                .HasPrecision(12, 2)
                .HasColumnName("total_venta");

            entity.HasOne(d => d.IdAperturaCajaNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdAperturaCaja)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_venta_apertura_caja");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("fk_venta_cliente");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
