using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Caja;
using Nk_Colletion_New.Negocios.Servicios.Cambio;

namespace Nk_Colletion_New;

public partial class Formapertura : Form
{
    private readonly UsuarioSesion _sesion;
    private readonly Caja_Service? _cajaService;
    private readonly TasaCambio_Service? _tasaService;

    public int IdAperturaCaja { get; private set; }

    public Formapertura() : this(new UsuarioSesion())
    {
    }

    public Formapertura(UsuarioSesion sesion)
    {
        InitializeComponent();
        _sesion = sesion;

        if (AppConfig.DbOptions is not null)
        {
            _cajaService = new Caja_Service(AppConfig.DbOptions);
            _tasaService = new TasaCambio_Service(AppConfig.DbOptions);
        }

        txtvalordolar.ReadOnly = true;
        txtsaldoinicial.TextAlign = HorizontalAlignment.Right;
        StartPosition = FormStartPosition.CenterScreen;
    }

    private async void Formapertura_Load_1(object sender, EventArgs e)
    {
        lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        lblUsuario.Text = string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? $"Usuario #{_sesion.IdUsuario}"
            : _sesion.NombreCompleto;

        IdAperturaCaja = 0;

        if (_cajaService is null || _sesion.IdUsuario <= 0)
        {
            btn_aperturar.Enabled = false;
            guna2HtmlLabel3.Text = "No se pudo identificar la sesión o la conexión a la base de datos.";
            return;
        }

        await CargarTasaAsync();
        await CargarEstadoCajaAsync();
    }

    private async Task CargarTasaAsync()
    {
        if (_tasaService is null)
        {
            txtvalordolar.Text = "No disponible";
            return;
        }

        try
        {
            var tasa = await _tasaService.ObtenerAsync();
            txtvalordolar.Text = tasa.TasaNioPorUsd.ToString("N4");
            label4.Text = tasa.Fecha == DateOnly.FromDateTime(DateTime.Today)
                ? $"Tasa oficial del día ({tasa.Fecha:dd/MM/yyyy})"
                : $"Última tasa oficial ({tasa.Fecha:dd/MM/yyyy})";
        }
        catch (Exception ex)
        {
            txtvalordolar.Text = "No disponible";
            label4.Text = "Tasa de cambio informativa";
            guna2HtmlLabel3.Text = ex.GetBaseException().Message;
        }
    }

    private async Task CargarEstadoCajaAsync()
    {
        if (_cajaService is null)
        {
            return;
        }

        try
        {
            var activa = await _cajaService.ObtenerAperturaActivaAsync(_sesion.IdUsuario);
            if (activa is null)
            {
                IdAperturaCaja = 0;
                txtsaldoinicial.Clear();
                txtsaldoinicial.ReadOnly = false;
                btn_aperturar.Text = "Aperturar caja";
                btn_aperturar.Enabled = true;
                guna2HtmlLabel3.Text = "Ingrese el fondo inicial para comenzar la jornada.";
                txtsaldoinicial.Focus();
                return;
            }

            IdAperturaCaja = activa.IdAperturaCaja;
            txtsaldoinicial.Text = (activa.MontoApertura ?? 0m).ToString("0.00");
            txtsaldoinicial.ReadOnly = true;
            btn_aperturar.Text = "Continuar con caja abierta";
            btn_aperturar.Enabled = true;
            guna2HtmlLabel3.Text =
                $"Caja abierta desde {activa.FechaApertura:dd/MM/yyyy HH:mm}. " +
                "Puede continuar con la jornada actual.";
        }
        catch (Exception ex)
        {
            btn_aperturar.Enabled = false;
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Apertura de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async void guna2Button2_Click(object sender, EventArgs e)
    {
        if (_cajaService is null || _sesion.IdUsuario <= 0)
        {
            MessageBox.Show(
                "No se identificó el usuario de la sesión.",
                "Apertura de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        try
        {
            btn_aperturar.Enabled = false;

            var activa = await _cajaService.ObtenerAperturaActivaAsync(_sesion.IdUsuario);
            if (activa is not null)
            {
                IdAperturaCaja = activa.IdAperturaCaja;
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            if (!decimal.TryParse(txtsaldoinicial.Text, out decimal monto) || monto < 0m)
            {
                MessageBox.Show(
                    "Ingrese un fondo inicial válido, igual o mayor que cero.",
                    "Apertura de caja",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtsaldoinicial.Focus();
                return;
            }

            var apertura = await _cajaService.AbrirCajaAsync(_sesion.IdUsuario, monto);
            IdAperturaCaja = apertura.IdAperturaCaja;

            MessageBox.Show(
                $"Caja abierta correctamente.\n\nFondo inicial: C$ {monto:N2}",
                "Apertura de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "No se pudo abrir la caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            if (!IsDisposed)
            {
                btn_aperturar.Enabled = true;
            }
        }
    }

    private void btn_regresar_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void guna2PictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void guna2PictureBox2_Click(object sender, EventArgs e)
    {
    }

    private void label3_Click(object sender, EventArgs e)
    {
    }

    private void Formapertura_Load(object sender, EventArgs e)
    {
    }

    private void guna2Panel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
    {
    }

    private void guna2Separator1_Click(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void guna2Button1_Click(object sender, EventArgs e)
    {
    }
}
