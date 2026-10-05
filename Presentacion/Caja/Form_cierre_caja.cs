using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Servicios.Caja;

namespace Nk_Colletion_New;

public partial class Form_cierre_caja : Form
{
    private readonly int _idAperturaCaja;
    private readonly CierreCaja_Service? _service;

    public Form_cierre_caja() : this(0)
    {
    }

    public Form_cierre_caja(int idAperturaCaja)
    {
        InitializeComponent();
        _idAperturaCaja = idAperturaCaja;

        if (AppConfig.DbOptions is not null)
        {
            _service = new CierreCaja_Service(AppConfig.DbOptions);
        }

        guna2Button1.Text = "Cerrar caja";
        guna2Button1.Click += CerrarCaja_Click;
        guna2Button2.Click += (_, _) => Close();

        foreach (var campo in new[]
        {
            txtEfectivodolar,
            txtEfectivocordoba,
            txtMontotarjeta,
            txtSaldosobrante,
            txtSaldofaltante,
            txtSaldofinal,
            txtArqueodecaja,
            txtSaldoinicial,
            txtPagos,
            txtAbonos,
            txtCompras,
            txtventas
        })
        {
            campo.ReadOnly = true;
        }
    }

    private async void Form_cierre_caja_Load(object sender, EventArgs e)
    {
        if (_service is null || _idAperturaCaja <= 0)
        {
            guna2Button1.Enabled = false;
            return;
        }

        try
        {
            var (apertura, arqueo) = await _service.ObtenerAsync(_idAperturaCaja);

            label10.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
            label11.Text = apertura.IdCajaNavigation.IdUsuarioNavigation.Nombre +
                           " " +
                           apertura.IdCajaNavigation.IdUsuarioNavigation.Apellido;

            txtSaldoinicial.Text = (apertura.MontoApertura ?? 0m).ToString("N2");
            txtCompras.Text = "N/A";
            txtAbonos.Text = "N/A";
            txtMontotarjeta.Text = "N/A";

            guna2Button1.Enabled = apertura.Estado == true && arqueo is not null;

            if (arqueo is null)
            {
                txtArqueodecaja.Text = "Pendiente";
                MessageBox.Show(
                    "Debe generar un arqueo antes de cerrar la caja.",
                    "Cierre de caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            txtventas.Text = arqueo.TotalVentas.ToString("N2");
            txtPagos.Text = arqueo.TotalEgresos.ToString("N2");
            txtArqueodecaja.Text = arqueo.SaldoContado.ToString("N2");
            txtSaldofinal.Text = arqueo.SaldoEsperado.ToString("N2");
            txtSaldosobrante.Text = Math.Max(0m, arqueo.Diferencia).ToString("N2");
            txtSaldofaltante.Text = Math.Max(0m, -arqueo.Diferencia).ToString("N2");

            txtEfectivocordoba.Text = arqueo.DetalleArqueos
                .Where(d => d.Moneda == "NIO")
                .Sum(d => d.Denominacion * d.Cantidad)
                .ToString("N2");

            txtEfectivodolar.Text = arqueo.DetalleArqueos
                .Where(d => d.Moneda == "USD")
                .Sum(d => d.Denominacion * d.Cantidad)
                .ToString("N2");
        }
        catch (Exception ex)
        {
            guna2Button1.Enabled = false;
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Cierre de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async void CerrarCaja_Click(object? sender, EventArgs e)
    {
        if (_service is null)
        {
            return;
        }

        try
        {
            guna2Button1.Enabled = false;
            await _service.CerrarAsync(_idAperturaCaja);

            MessageBox.Show(
                "Caja cerrada correctamente.",
                "Cierre de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            guna2Button1.Enabled = true;
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Cierre de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void label27_Click(object sender, EventArgs e)
    {
    }

    private void txtSaldofaltante_TextChanged(object sender, EventArgs e)
    {
    }

    private void label8_Click(object sender, EventArgs e)
    {
    }
}
