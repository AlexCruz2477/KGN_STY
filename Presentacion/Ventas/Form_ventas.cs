using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nk_Colletion_New
{
    public partial class Form_ventas : Form
    {
        private readonly Venta_Service? _servicio;
        private readonly int _idUsuario;
        private readonly ListaEnlazada<DetalleVentaTemporal> _detalles = new();
        private readonly ComboBox _clientesCombo = new();
        private List<ProductoVariante> _variantes = new();
        private bool _cargando;

        public Form_ventas() : this(0)
        {
        }

        public Form_ventas(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            var opciones = AppConfig.DbOptions;
            if (opciones is not null) _servicio = new Venta_Service(opciones);
            _clientesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            _clientesCombo.Location = guna2TextBox1.Location;
            _clientesCombo.Size = guna2TextBox1.Size;
            _clientesCombo.Font = guna2TextBox1.Font;
            _clientesCombo.Anchor = guna2TextBox1.Anchor;
            guna2TextBox1.Visible = false;
            guna2ComboBox3.SelectedIndexChanged += MetodoPagoSeleccionado;
            guna2ShadowPanel1.Controls.Add(_clientesCombo);
            _clientesCombo.BringToFront();
            guna2Button2.Text = "Eliminar";
            guna2NumericUpDown1.Minimum = 1;
            guna2NumericUpDown1.Value = 1;
            label21.Text = "Pago:";
            label14.Text = "Pago adicional:";
            guna2TextBox13.PlaceholderText = "Opcional";
            guna2TextBox12.PlaceholderText = "Monto";
            guna2TextBox1.PlaceholderText = "Nombre cliente (informativo)";
            guna2TextBox7.PlaceholderText = "Teléfono cliente";
            guna2TextBox4.PlaceholderText = "Dirección cliente";
            guna2TextBox3.ReadOnly = true;
            guna2TextBox2.ReadOnly = true;
            guna2TextBox8.ReadOnly = true;
            guna2ComboBox2.Enabled = false;
            guna2ComboBox4.Enabled = false;
            guna2ComboBox5.Enabled = false;
            guna2Button3.Visible = false;
            guna2Button1.Click += AgregarDetalle_Click;
            guna2Button2.Click += EliminarDetalle_Click;
            guna2Button4.Click += GuardarVenta_Click;
            guna2DataGridView1.CellDoubleClick += (_, e) => EliminarDetallePorFila(e.RowIndex);
            guna2ComboBox1.SelectedIndexChanged += VarianteSeleccionada;
            guna2NumericUpDown1.ValueChanged += (_, _) => ActualizarTotales();
            guna2TextBox10.TextChanged += (_, _) => ActualizarTotales();
            guna2TextBox12.TextChanged += (_, _) => ActualizarTotales();
            guna2TextBox13.TextChanged += (_, _) => ActualizarTotales();
        }


        private async void Form_ventas_Load(object sender, EventArgs e)
        {
            if (_servicio is null) { MessageBox.Show("No está configurada la conexión a la base de datos."); return; }
            try
            {
                _cargando = true;
                guna2DataGridView1.Columns.Clear();
                guna2DataGridView1.Columns.Add("VarianteId", "VarianteId");
                guna2DataGridView1.Columns[0].Visible = false;
                guna2DataGridView1.Columns.Add("Producto", "Producto");
                guna2DataGridView1.Columns.Add("Código", "Código");
                guna2DataGridView1.Columns.Add("Cantidad", "Cantidad");
                guna2DataGridView1.Columns.Add("Precio", "Precio unitario");
                guna2DataGridView1.Columns.Add("Subtotal", "Subtotal");
                var clientes = await _servicio.ListarClientesAsync();
                _clientesCombo.DataSource = clientes;
                _clientesCombo.DisplayMember = nameof(Cliente.Nombre);
                _clientesCombo.ValueMember = nameof(Cliente.IdCliente);
                _clientesCombo.SelectedIndex = -1;
                _clientesCombo.Format += (_, args) =>
                {
                    if (args.ListItem is Cliente cliente) args.Value = $"{cliente.Nombre} {cliente.Apellido}";
                };
                _variantes = await _servicio.BuscarVariantesAsync(string.Empty);
                guna2ComboBox1.DataSource = _variantes;
                guna2ComboBox1.DisplayMember = nameof(ProductoVariante.Codigo);
                guna2ComboBox1.ValueMember = nameof(ProductoVariante.IdVariante);
                var metodos = await _servicio.ListarMetodosPagoAsync();
                guna2ComboBox3.DataSource = metodos;
                guna2ComboBox3.DisplayMember = nameof(MetodoPago.Nombre);
                guna2ComboBox3.ValueMember = nameof(MetodoPago.IdMetodoPago);
                guna2ComboBox3.SelectedIndex = metodos.Count > 0 ? 0 : -1;
                label11.Text = $"Usuario #{_idUsuario}";
                if (_idUsuario <= 0) label11.Text = "Usuario no identificado";
                ActualizarDatosVariante();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            finally { _cargando = false; }
        }

        private void VarianteSeleccionada(object? sender, EventArgs e) => ActualizarDatosVariante();

        private void MetodoPagoSeleccionado(object? sender, EventArgs e)
        {
            string? nombre = (guna2ComboBox3.SelectedItem as MetodoPago)?.Nombre;
            label21.Text = nombre is null ? "Pago:" : $"{nombre}:";
            label14.Text = "Pago adicional:";
        }

        private void ActualizarDatosVariante()
        {
            if (guna2ComboBox1.SelectedItem is not ProductoVariante variante) return;
            guna2TextBox2.Text = variante.Codigo;
            guna2TextBox8.Text = variante.PrecioVenta.ToString("0.00");
            guna2ComboBox2.Text = variante.IdTallaNavigation?.NombreTalla ?? string.Empty;
            guna2ComboBox4.Text = variante.IdProductoNavigation?.IdMarcaNavigation?.Nombre ?? string.Empty;
            guna2ComboBox5.Text = variante.IdColorNavigation?.NombreColor ?? string.Empty;
            guna2TextBox1.Text = variante.IdProductoNavigation?.NombreProducto ?? string.Empty;
            guna2TextBox3.Text = variante.StockActual.ToString();
        }

        private void AgregarDetalle_Click(object? sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem is not ProductoVariante variante) { MessageBox.Show("Seleccione una variante."); return; }
            int cantidad = (int)guna2NumericUpDown1.Value;
            if (cantidad <= 0) { MessageBox.Show("La cantidad debe ser mayor que cero."); return; }
            if (!decimal.TryParse(guna2TextBox10.Text, out var descuento) && !string.IsNullOrWhiteSpace(guna2TextBox10.Text)) { MessageBox.Show("Ingrese un descuento válido."); return; }
            if (descuento < 0) { MessageBox.Show("El descuento no puede ser negativo."); return; }
            var actual = _detalles.FirstOrDefault(d => d.IdVariante == variante.IdVariante);
            if (actual is not null && actual.Cantidad + cantidad > variante.StockActual) { MessageBox.Show("La cantidad supera el stock disponible."); return; }
            if (actual is not null) _detalles.Eliminar(d => d.IdVariante == variante.IdVariante);
            _detalles.Agregar(new DetalleVentaTemporal(variante.IdVariante, variante.IdProductoNavigation.NombreProducto, variante.Codigo,
                cantidad + (actual?.Cantidad ?? 0), variante.PrecioVenta, variante.StockActual,
                variante.IdTallaNavigation?.NombreTalla, variante.IdColorNavigation?.NombreColor));
            ActualizarGrilla();
        }

        private void EliminarDetalle_Click(object? sender, EventArgs e)
        {
            if (guna2DataGridView1.CurrentRow is null || guna2DataGridView1.CurrentRow.IsNewRow) return;
            EliminarDetallePorFila(guna2DataGridView1.CurrentRow.Index);
        }

        private void EliminarDetallePorFila(int indice)
        {
            if (indice < 0 || indice >= _detalles.Count) return;
            int id = Convert.ToInt32(guna2DataGridView1.Rows[indice].Cells[0].Value);
            _detalles.Eliminar(d => d.IdVariante == id);
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (var item in _detalles) guna2DataGridView1.Rows.Add(item.IdVariante, item.Producto, item.Codigo, item.Cantidad, item.PrecioUnitario.ToString("0.00"), item.Subtotal.ToString("0.00"));
            ActualizarTotales();
        }

        private void ActualizarTotales()
        {
            if (_cargando) return;
            decimal subtotal = _detalles.Sum(d => d.Subtotal);
            decimal descuento = decimal.TryParse(guna2TextBox10.Text, out decimal valorDescuento) ? valorDescuento : 0;
            decimal iva = 0;
            decimal total = Math.Max(0, subtotal + iva - descuento);
            lbl_subtotalF.Text = subtotal.ToString("0.00");
            lbl_descuentoF.Text = descuento.ToString("0.00");
            lbl_totalF.Text = total.ToString("0.00");
            decimal pago = decimal.TryParse(guna2TextBox12.Text, out var pagoCordoba) ? pagoCordoba : 0;
            pago += decimal.TryParse(guna2TextBox13.Text, out var pagoAdicional) ? pagoAdicional : 0;
            lbl_cambioF.Text = Math.Max(0, pago - total).ToString("0.00");
            label1.Text = iva.ToString("0.00");
        }

        private async void GuardarVenta_Click(object? sender, EventArgs e)
        {
            if (_servicio is null || _idUsuario <= 0) { MessageBox.Show("No se identificó el usuario de la sesión."); return; }
            try
            {
                int? idCliente = _clientesCombo.SelectedValue is int id && id > 0 ? id : null;
                int idMetodo = guna2ComboBox3.SelectedValue is int metodo ? metodo : 0;
                decimal total = decimal.Parse(lbl_totalF.Text);
                decimal efectivo = decimal.TryParse(guna2TextBox12.Text, out var montoEfectivo) ? montoEfectivo : 0;
                decimal efectivoDolar = decimal.TryParse(guna2TextBox13.Text, out var montoDolar) ? montoDolar : 0;
                if (efectivo < 0 || efectivoDolar < 0) { MessageBox.Show("Los montos de pago no pueden ser negativos."); return; }
                if (efectivo > 0 && efectivoDolar > 0) { MessageBox.Show("Registre un solo monto de pago. Para pagos combinados use un método por operación."); return; }
                decimal pagado = efectivo + efectivoDolar;
                if (pagado < total) { MessageBox.Show("El pago no cubre el total."); return; }
                var pagos = new List<DatosPagoVenta>();
                if (pagado > 0) pagos.Add(new DatosPagoVenta(idMetodo, pagado));
                int venta = await _servicio.GuardarAsync(_idUsuario, idCliente, decimal.TryParse(guna2TextBox10.Text, out var desc) ? desc : 0, 0, _detalles, pagos);
                MessageBox.Show($"Venta {venta} registrada correctamente.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _detalles.Limpiar(); ActualizarGrilla(); guna2TextBox12.Clear(); guna2TextBox13.Clear(); guna2TextBox10.Clear();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "No se pudo registrar la venta", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void lbl_subtotal_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
