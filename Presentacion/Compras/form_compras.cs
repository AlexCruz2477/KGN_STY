using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Metodos_Ordenamiento;
using Nk_Colletion_New.Negocios.Servicios.Compras;

namespace Nk_Colletion_New
{
    public partial class form_compras : Form
    {
        private readonly Compra_Service? _servicio;
        private readonly int _idUsuario;
        private readonly ListaEnlazada<DetalleCompraTemporal> _detalles = new();
        private List<ProductoVariante> _variantes = new();

        public form_compras() : this(0)
        {
        }

        public form_compras(int idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            if (AppConfig.DbOptions is not null) _servicio = new Compra_Service(AppConfig.DbOptions);
            guna2Button3.Click += AgregarDetalle_Click;
            guna2Button1.Click += GuardarCompra_Click;
            guna2Button2.Click += (_, _) => LimpiarCompra();
            guna2ComboBox1.SelectedIndexChanged += VarianteSeleccionada;
            guna2DataGridView1.AllowUserToDeleteRows = false;
            guna2DataGridView1.Columns.Clear();
            guna2DataGridView1.Columns.Add("IdVariante", "IdVariante");
            guna2DataGridView1.Columns[0].Visible = false;
            guna2DataGridView1.Columns.Add("Producto", "Producto");
            guna2DataGridView1.Columns.Add("Cantidad", "Cantidad");
            guna2DataGridView1.Columns.Add("PrecioCompra", "Precio compra");
            guna2DataGridView1.Columns.Add("PrecioVenta", "Precio venta");
            guna2DataGridView1.Columns.Add("Subtotal", "Subtotal");
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            await GuardarCompraAsync();
        }

        private async void form_compras_Load(object sender, EventArgs e)
        {
            if (_servicio is null) { MessageBox.Show("No está configurada la conexión a la base de datos."); return; }
            try
            {
                var proveedores = await _servicio.ListarProveedoresAsync();
                guna2ComboBox2.DataSource = proveedores;
                guna2ComboBox2.DisplayMember = nameof(Proveedor.Nombre);
                guna2ComboBox2.ValueMember = nameof(Proveedor.IdProveedor);
                guna2ComboBox2.SelectedIndex = -1;
                _variantes = await _servicio.ListarVariantesAsync();
                guna2ComboBox1.DataSource = _variantes;
                guna2ComboBox1.DisplayMember = nameof(ProductoVariante.Codigo);
                guna2ComboBox1.ValueMember = nameof(ProductoVariante.IdVariante);
                guna2ComboBox1.SelectedIndex = -1;
                guna2DateTimePicker1.Value = DateTime.Today;
                guna2TextBox4.PlaceholderText = "Cantidad";
                guna2TextBox4.Text = "1";
                guna2TextBox5.PlaceholderText = "Precio de compra";
                guna2TextBox5.ReadOnly = true;
                guna2TextBox2.PlaceholderText = "Precio de venta";
                label5.Text = "Producto / variante:";
                label13.Text = $"Usuario: {_idUsuario}";
                VarianteSeleccionada(this, EventArgs.Empty);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Compras", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VarianteSeleccionada(object? sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem is not ProductoVariante variante) return;
            guna2TextBox5.Text = variante.PrecioCompra.ToString("0.00");
            guna2TextBox2.Text = variante.PrecioVenta.ToString("0.00");
        }

        private void AgregarDetalle_Click(object? sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem is not ProductoVariante variante) { MessageBox.Show("Seleccione una variante."); return; }
            if (!int.TryParse(guna2TextBox4.Text, out int cantidad) || cantidad <= 0) { MessageBox.Show("Ingrese una cantidad mayor que cero."); return; }
            if (!decimal.TryParse(guna2TextBox5.Text, out var precioCompra) || precioCompra < 0 || !decimal.TryParse(guna2TextBox2.Text, out var precioVenta) || precioVenta < 0)
            { MessageBox.Show("Ingrese precios válidos."); return; }
            var existente = _detalles.FirstOrDefault(d => d.IdVariante == variante.IdVariante);
            if (existente is not null) _detalles.Eliminar(d => d.IdVariante == variante.IdVariante);
            _detalles.Agregar(new DetalleCompraTemporal(variante.IdVariante, variante.IdProductoNavigation.NombreProducto, variante.Codigo,
                cantidad + (existente?.Cantidad ?? 0), precioCompra, precioVenta));
            ActualizarGrilla();
        }

        private void ActualizarGrilla()
        {
            guna2DataGridView1.Rows.Clear();
            foreach (var detalle in _detalles)
                guna2DataGridView1.Rows.Add(detalle.IdVariante, $"{detalle.Producto} ({detalle.Codigo})", detalle.Cantidad,
                    detalle.PrecioCompra.ToString("0.00"), detalle.PrecioVenta.ToString("0.00"), detalle.Subtotal.ToString("0.00"));
            label14.Text = $"Total: {_detalles.Sum(d => d.Subtotal):0.00}";
        }

        private void EliminarDetalle(int index)
        {
            if (index < 0 || index >= _detalles.Count) return;
            int id = _detalles.ElementAt(index).IdVariante;
            _detalles.Eliminar(d => d.IdVariante == id);
            ActualizarGrilla();
        }

        private async void GuardarCompra_Click(object? sender, EventArgs e) => await GuardarCompraAsync();

        private async Task GuardarCompraAsync()
        {
            if (_servicio is null || _idUsuario <= 0) { MessageBox.Show("No se identificó el usuario."); return; }
            try
            {
                if (guna2ComboBox2.SelectedValue is not int idProveedor) { MessageBox.Show("Seleccione un proveedor."); return; }
                int idCompra = await _servicio.GuardarAsync(idProveedor, _idUsuario, guna2TextBox3.Text, guna2DateTimePicker1.Value,
                    0, _detalles);
                MessageBox.Show($"Compra {idCompra} registrada correctamente.", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCompra();
                _variantes = await _servicio.ListarVariantesAsync();
                guna2ComboBox1.DataSource = _variantes;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "No se pudo registrar la compra", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void LimpiarCompra()
        {
            _detalles.Limpiar();
            ActualizarGrilla();
            guna2TextBox3.Clear();
                guna2TextBox4.Text = "1";
        }
    }
}
