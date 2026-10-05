using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Caja;

namespace Nk_Colletion_New;

public partial class Form_control_egresos : Form
{
    private readonly UsuarioSesion _sesion;
    private readonly int _idAperturaCaja;
    private readonly Egreso_Service? _egresoService;

    public Form_control_egresos() : this(new UsuarioSesion(), 0)
    {
    }

    public Form_control_egresos(
        UsuarioSesion sesion,
        int idAperturaCaja)
    {
        InitializeComponent();
        _sesion = sesion;
        _idAperturaCaja = idAperturaCaja;

        if (AppConfig.DbOptions is not null)
        {
            _egresoService = new Egreso_Service(AppConfig.DbOptions);
        }

        Load += Form_control_egresos_Load;
        guna2Button1.Click += GuardarEgreso_Click;
        guna2Button2.Click += (_, _) => Close();
    }

    private async void Form_control_egresos_Load(object? sender, EventArgs e)
    {
        label1.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy}";
        label10.Text = string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? $"Usuario #{_sesion.IdUsuario}"
            : _sesion.NombreCompleto;

        guna2TextBox2.ReadOnly = true;
        guna2TextBox3.ReadOnly = true;

        guna2DataGridView1.AllowUserToAddRows = false;
        guna2DataGridView1.AllowUserToDeleteRows = false;
        guna2DataGridView1.ReadOnly = true;
        guna2DataGridView1.MultiSelect = false;
        guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        await RefrescarAsync();
    }

    private async Task RefrescarAsync()
    {
        if (_egresoService is null || _idAperturaCaja <= 0)
        {
            return;
        }

        try
        {
            var lista = await _egresoService.ListarActivosAsync(_idAperturaCaja);

            guna2DataGridView1.Rows.Clear();
            foreach (var egreso in lista)
            {
                guna2DataGridView1.Rows.Add(
                    egreso.FechaEgreso?.ToString("dd/MM/yyyy HH:mm") ?? string.Empty,
                    egreso.IdTipoEgresoNavigation?.Nombre ?? "Sin tipo",
                    egreso.Descripcion ?? string.Empty,
                    $"C$ {egreso.Monto:N2}");
            }

            guna2TextBox3.Text = (await _egresoService
                .ObtenerCantidadEgresosAsync(_idAperturaCaja))
                .ToString();

            guna2TextBox2.Text = $"C$ {await _egresoService.ObtenerTotalEgresadoAsync(_idAperturaCaja):N2}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Egresos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private async void GuardarEgreso_Click(object? sender, EventArgs e)
    {
        if (_egresoService is null || _idAperturaCaja <= 0)
        {
            MessageBox.Show("No se identificó una apertura de caja válida.");
            return;
        }

        string tipo = guna2TextBox6.Text.Trim();
        string responsable = guna2TextBox1.Text.Trim();

        if (string.IsNullOrWhiteSpace(tipo))
        {
            MessageBox.Show("Ingrese el tipo de egreso.");
            guna2TextBox6.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(responsable))
        {
            MessageBox.Show("Ingrese el responsable.");
            guna2TextBox1.Focus();
            return;
        }

        if (!decimal.TryParse(guna2TextBox5.Text, out decimal monto) || monto <= 0)
        {
            MessageBox.Show("Ingrese un monto válido mayor que cero.");
            guna2TextBox5.Focus();
            return;
        }

        try
        {
            guna2Button1.Enabled = false;
            int idTipo = await _egresoService.ObtenerOCrearTipoAsync(tipo);

            await _egresoService.GuardarAsync(
                _idAperturaCaja,
                idTipo,
                monto,
                responsable);

            guna2TextBox6.Clear();
            guna2TextBox1.Clear();
            guna2TextBox5.Clear();

            await RefrescarAsync();

            MessageBox.Show(
                "Egreso registrado correctamente.",
                "Egresos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Egresos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
        finally
        {
            guna2Button1.Enabled = true;
        }
    }

    private void guna2TextBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
    {
    }
}
