using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Datos.Modelos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Caja;

namespace Nk_Colletion_New;

public partial class Form_arqueo : Form
{
    private readonly UsuarioSesion _sesion;
    private readonly int _idAperturaCaja;
    private readonly Arqueo_Service? _service;

    public Form_arqueo() : this(new UsuarioSesion(), 0)
    {
    }

    public Form_arqueo(UsuarioSesion sesion, int idAperturaCaja)
    {
        InitializeComponent();
        _sesion = sesion;
        _idAperturaCaja = idAperturaCaja;

        if (AppConfig.DbOptions is not null)
        {
            _service = new Arqueo_Service(AppConfig.DbOptions);
        }

        Load += Form_arqueo_Load;
        guna2Button1.Click += GenerarArqueo_Click;
        guna2Button2.Click += (_, _) => Close();
        guna2Button1.Text = "Generar arqueo";

        foreach (var campo in new[]
        {
            guna2TextBox1,
            guna2TextBox2,
            guna2TextBox3,
            guna2TextBox5,
            guna2TextBox6
        })
        {
            campo.ReadOnly = true;
        }
    }

    private void Form_arqueo_Load(object? sender, EventArgs e)
    {
        label3.Text = string.IsNullOrWhiteSpace(_sesion.NombreCompleto)
            ? $"Usuario #{_sesion.IdUsuario}"
            : _sesion.NombreCompleto;
        label4.Text = $"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm}";
        label20.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        ActualizarResumenVisual();

        foreach (var control in ControlesDenominacion())
        {
            control.ValueChanged += (_, _) => ActualizarResumenVisual();
        }
    }

    private IEnumerable<NumericUpDown> ControlesDenominacion()
    {
        return new NumericUpDown[]
        {
            numericUpDown1, numericUpDown2, numericUpDown3, numericUpDown4,
            numericUpDown5, numericUpDown6, numericUpDown7, numericUpDown8,
            numericUpDown9, numericUpDown10, numericUpDown11, numericUpDown12,
            numericUpDown13, numericUpDown14, numericUpDown15, numericUpDown16,
            numericUpDown17, numericUpDown18
        };
    }

    private List<DetalleArqueo> CrearDetalles()
    {
        var detalles = new List<DetalleArqueo>();

        static void Agregar(
            ICollection<DetalleArqueo> destino,
            decimal denominacion,
            NumericUpDown control,
            string moneda)
        {
            destino.Add(new DetalleArqueo
            {
                Denominacion = denominacion,
                Cantidad = (int)control.Value,
                Moneda = moneda
            });
        }

        // Billetes en córdobas.
        Agregar(detalles, 1000m, numericUpDown1, "NIO");
        Agregar(detalles, 500m, numericUpDown4, "NIO");
        Agregar(detalles, 200m, numericUpDown3, "NIO");
        Agregar(detalles, 100m, numericUpDown2, "NIO");
        Agregar(detalles, 50m, numericUpDown5, "NIO");
        Agregar(detalles, 20m, numericUpDown6, "NIO");
        Agregar(detalles, 10m, numericUpDown7, "NIO");
        Agregar(detalles, 5m, numericUpDown8, "NIO");

        // Dólares.
        Agregar(detalles, 100m, numericUpDown14, "USD");
        Agregar(detalles, 50m, numericUpDown13, "USD");
        Agregar(detalles, 20m, numericUpDown12, "USD");
        Agregar(detalles, 10m, numericUpDown11, "USD");
        Agregar(detalles, 5m, numericUpDown10, "USD");
        Agregar(detalles, 1m, numericUpDown9, "USD");

        // Monedas en córdobas.
        Agregar(detalles, 5m, numericUpDown18, "NIO");
        Agregar(detalles, 1m, numericUpDown17, "NIO");
        Agregar(detalles, 0.50m, numericUpDown16, "NIO");
        Agregar(detalles, 0.25m, numericUpDown15, "NIO");

        return detalles;
    }

    private void ActualizarResumenVisual()
    {
        var detalles = CrearDetalles();

        decimal billetesCordoba =
            (1000m * numericUpDown1.Value) +
            (500m * numericUpDown4.Value) +
            (200m * numericUpDown3.Value) +
            (100m * numericUpDown2.Value) +
            (50m * numericUpDown5.Value) +
            (20m * numericUpDown6.Value) +
            (10m * numericUpDown7.Value) +
            (5m * numericUpDown8.Value);

        // En la pantalla original hay dos entradas de C$5; una pertenece al
        // bloque de billetes y otra al bloque de monedas. Ambas se contabilizan.
        decimal monedasCordoba =
            (5m * numericUpDown18.Value) +
            (1m * numericUpDown17.Value) +
            (0.50m * numericUpDown16.Value) +
            (0.25m * numericUpDown15.Value);

        decimal totalCordobas = detalles
            .Where(d => d.Moneda == "NIO")
            .Sum(d => d.Denominacion * d.Cantidad);

        decimal totalDolares = detalles
            .Where(d => d.Moneda == "USD")
            .Sum(d => d.Denominacion * d.Cantidad);

        guna2TextBox6.Text = billetesCordoba.ToString("N2");
        guna2TextBox1.Text = monedasCordoba.ToString("N2");
        guna2TextBox3.Text = totalCordobas.ToString("N2");
        guna2TextBox2.Text = totalDolares.ToString("N2");
        guna2TextBox5.Text = $"C$ {totalCordobas:N2} + US$ {totalDolares:N2}";
    }

    private async void GenerarArqueo_Click(object? sender, EventArgs e)
    {
        if (_service is null || _idAperturaCaja <= 0)
        {
            MessageBox.Show("No se identificó una apertura de caja válida.");
            return;
        }

        try
        {
            guna2Button1.Enabled = false;
            var arqueo = await _service.GuardarAsync(
                _idAperturaCaja,
                null,
                CrearDetalles());

            MessageBox.Show(
                $"Arqueo generado correctamente.\n\n" +
                $"Esperado: C$ {arqueo.SaldoEsperado:N2}\n" +
                $"Contado: C$ {arqueo.SaldoContado:N2}\n" +
                $"Diferencia: C$ {arqueo.Diferencia:N2}",
                "Arqueo de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            guna2Button1.Enabled = false;
        }
        catch (Exception ex)
        {
            guna2Button1.Enabled = true;
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Arqueo de caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void label36_Click(object sender, EventArgs e)
    {
    }
}
