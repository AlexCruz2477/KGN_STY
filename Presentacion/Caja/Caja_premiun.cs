using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Caja;

namespace Nk_Colletion_New;

public partial class Caja_premiun : Form
{
    public event EventHandler? CajaCerrada;

    private readonly UsuarioSesion _sesion;
    private readonly int _idAperturaCaja;
    private readonly CajaResumen_Service? _resumenService;

    public Caja_premiun() : this(new UsuarioSesion(), 0)
    {
    }

    public Caja_premiun(UsuarioSesion sesion, int idAperturaCaja)
    {
        InitializeComponent();
        _sesion = sesion;
        _idAperturaCaja = idAperturaCaja;

        if (AppConfig.DbOptions is not null)
        {
            _resumenService = new CajaResumen_Service(AppConfig.DbOptions);
        }

        Load += Caja_premiun_Load;
        guna2Button2.Click += AbrirArqueo_Click;
        guna2Button3.Click += AbrirCierre_Click;
        guna2Button4.Click += AbrirEgresos_Click;
        guna2Button1.Click += async (_, _) => await CargarResumenAsync();
        guna2Button1.Text = "Actualizar resumen";

        foreach (var campo in new[]
        {
            guna2TextBox1,
            guna2TextBox2,
            guna2TextBox3,
            guna2TextBox4,
            guna2TextBox5,
            guna2TextBox6
        })
        {
            campo.ReadOnly = true;
        }
    }

    private async void Caja_premiun_Load(object? sender, EventArgs e)
    {
        label9.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
        label11.Text = string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? $"Usuario #{_sesion.IdUsuario}"
            : _sesion.NombreCompleto;

        await CargarResumenAsync();
    }

    private async Task CargarResumenAsync()
    {
        if (_resumenService is null || _idAperturaCaja <= 0)
        {
            return;
        }

        try
        {
            var resumen = await _resumenService.ObtenerAsync(_idAperturaCaja);

            guna2TextBox1.Text = $"C$ {resumen.SaldoInicial:N2}";
            guna2TextBox2.Text = $"C$ {resumen.TotalVentas:N2}";
            guna2TextBox3.Text = $"C$ {resumen.TotalEgresos:N2}";
            guna2TextBox4.Text = $"C$ {resumen.SaldoEfectivoEsperado:N2}";
            guna2TextBox5.Text = $"{resumen.CantidadVentas} ventas";
            guna2TextBox6.Text = $"{resumen.CantidadEgresos} egresos";

            guna2DataGridView1.Rows.Clear();
            foreach (var movimiento in await _resumenService.ObtenerMovimientosAsync(_idAperturaCaja))
            {
                string tipo = $"{movimiento.Fecha:dd/MM HH:mm} · {movimiento.Tipo}";
                string monto = movimiento.Monto < 0
                    ? $"- C$ {Math.Abs(movimiento.Monto):N2}"
                    : $"C$ {movimiento.Monto:N2}";

                guna2DataGridView1.Rows.Add(tipo, monto);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async void AbrirEgresos_Click(object? sender, EventArgs e)
    {
        using var formulario = new Form_control_egresos(
            _sesion,
            _idAperturaCaja);
        formulario.ShowDialog(this);
        await CargarResumenAsync();
    }

    private async void AbrirArqueo_Click(object? sender, EventArgs e)
    {
        using var formulario = new Form_arqueo(
            _sesion,
            _idAperturaCaja);
        formulario.ShowDialog(this);
        await CargarResumenAsync();
    }

    private async void AbrirCierre_Click(object? sender, EventArgs e)
    {
        using var formulario = new Form_cierre_caja(_idAperturaCaja);
        if (formulario.ShowDialog(this) == DialogResult.OK)
        {
            guna2Button2.Enabled = false;
            guna2Button3.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button1.Enabled = false;

            MessageBox.Show(
                "La caja quedó cerrada. Cuando vuelva a entrar a Caja o Ventas, el sistema solicitará una nueva apertura.",
                "Caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            CajaCerrada?.Invoke(this, EventArgs.Empty);
        }

        await CargarResumenAsync();
    }

    private void guna2Panel1_Paint(object sender, PaintEventArgs e)
    {
    }

    private void guna2ShadowPanel3_Paint(object sender, PaintEventArgs e)
    {
    }

    private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
    {
    }

    private void Caja_premiun_Load_1(object sender, EventArgs e)
    {

    }
}
