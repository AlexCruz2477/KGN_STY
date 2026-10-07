using Nk_Colletion_New.Datos;
using Nk_Colletion_New.Negocios.Autenticacion;
using Nk_Colletion_New.Negocios.Servicios.Principal;
using Nk_Colletion_New.Negocios.Servicios.Caja;
using Nk_Colletion_New.Presentacion.Helpers;
using Nk_Colletion_New.Presentacion.Productos;
using System.Globalization;
using System.Text;

namespace Nk_Colletion_New;

public partial class Main : Form
{
    private readonly UsuarioSesion _sesion;
    private int _idAperturaCaja;
    private Caja_Service? _cajaService;
    private Guna.UI2.WinForms.Guna2Button _btnInicio = null!;
    private HashSet<string> _modulosPermitidos = new(StringComparer.OrdinalIgnoreCase);
    private readonly ToolTip _tooltipPermisos = new();

    public Main() : this(new UsuarioSesion(), 0)
    {
    }

    public Main(UsuarioSesion sesion, int idAperturaCaja)
    {
        InitializeComponent();
        _sesion = sesion;
        _idAperturaCaja = idAperturaCaja;

        if (AppConfig.DbOptions is not null)
        {
            _cajaService = new Caja_Service(AppConfig.DbOptions);
        }

        PrepararVentanaPrincipal();
        PrepararMenuLateral();
        CrearBotonInicio();
        AplicarPermisosRol();

        guna2Button1.Click += CerrarSesion_Click;
        btncredito.Click += (_, _) =>
        {
            if (VerificarAcceso("Crédito")) AbrirFormularioEnPanel(new Form_credito());
        };
        btnacercade.Click += (_, _) =>
        {
            if (VerificarAcceso("Acerca de")) AcercaDe_Click(this, EventArgs.Empty);
        };
        Resize += (_, _) => OrganizarMenuLateral();
    }

    private void PrepararVentanaPrincipal()
    {
        FormBorderStyle = FormBorderStyle.Sizable;
        MaximizeBox = true;
        MinimizeBox = true;
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(1000, 650);
        DoubleBuffered = true;
    }

    private void PrepararMenuLateral()
    {
        // Controles antiguos que quedaron en el Designer original, pero que ya no
        // pertenecen al menú vigente. Se ocultan para que no interfieran con el
        // redimensionamiento ni con el tabulado.
        foreach (Control obsoleto in new Control[]
        {
            lbl_Cerrar_sesion,
            label1,
            btn_Acerca_de,
            btn_Mantenimiento,
            btn_Reporte,
            btn_Categoria,
            btn_Inventario,
            btn_Credito,
            btn_Devolucion,
            btn_Ventas,
            btn_Caja,
            btn_Productos,
            btn_Compras,
            btn_Proveedores
        })
        {
            obsoleto.Visible = false;
            obsoleto.Enabled = false;
            obsoleto.TabStop = false;
        }

        Panel_Padre.AutoScroll = false;
        Panel_Padre.MinimumSize = new Size(185, 0);
        OrganizarMenuLateral();
    }

    private void CrearBotonInicio()
    {
        _btnInicio = new Guna.UI2.WinForms.Guna2Button
        {
            Name = "btnInicio",
            Text = "Inicio / Dashboard",
            FillColor = Color.FromArgb(63, 65, 64),
            ForeColor = Color.FromArgb(235, 222, 208),
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
            Image = Properties.Resources.icons8_gráfico_de_barras_24,
            ImageSize = new Size(20, 20),
            TextAlign = HorizontalAlignment.Left,
            Cursor = Cursors.Hand,
            BorderRadius = 6,
            HoverState = { FillColor = Color.FromArgb(184, 149, 85), ForeColor = Color.White }
        };
        _btnInicio.Click += async (_, _) =>
        {
            if (VerificarAcceso("Inicio")) await MostrarDashboardAsync();
        };
        Panel_Padre.Controls.Add(_btnInicio);
        _btnInicio.BringToFront();
    }

    private void AplicarPermisosRol()
    {
        string rol = NormalizarRol(_sesion.Rol);
        bool administrador = rol.Contains("admin") || rol.Contains("administrador");

        if (administrador)
        {
            _modulosPermitidos = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "*" };
        }
        else if (rol.Contains("gerente") || rol.Contains("gerencia") || rol.Contains("supervisor"))
        {
            _modulosPermitidos = new HashSet<string>(new[]
            {
                "Inicio", "Caja", "Ventas", "Compras", "Productos", "Categoría",
                "Clientes", "Proveedores", "Crédito", "Devolución", "Reporte", "Acerca de"
            }, StringComparer.OrdinalIgnoreCase);
        }
        else if (rol.Contains("bodeguero") || rol.Contains("bodega") ||
                 rol.Contains("inventario") || rol.Contains("almacen") || rol.Contains("almacenero"))
        {
            _modulosPermitidos = new HashSet<string>(new[]
            {
                "Inicio", "Compras", "Productos", "Categoría", "Proveedores", "Reporte", "Acerca de"
            }, StringComparer.OrdinalIgnoreCase);
        }
        else if (rol.Contains("cajero") || rol.Contains("caja") || rol.Contains("venta"))
        {
            _modulosPermitidos = new HashSet<string>(new[]
            {
                "Inicio", "Caja", "Ventas", "Productos", "Clientes", "Crédito", "Devolución", "Acerca de"
            }, StringComparer.OrdinalIgnoreCase);
        }
        else if (rol.Contains("compra") || rol.Contains("proveedor"))
        {
            _modulosPermitidos = new HashSet<string>(new[]
            {
                "Inicio", "Compras", "Productos", "Categoría", "Proveedores", "Reporte", "Acerca de"
            }, StringComparer.OrdinalIgnoreCase);
        }
        else
        {
            _modulosPermitidos = new HashSet<string>(new[] { "Inicio", "Acerca de" }, StringComparer.OrdinalIgnoreCase);
        }

        var accesos = new (Control Control, string Modulo)[]
        {
            (_btnInicio, "Inicio"), (btncaja, "Caja"), (btnventas, "Ventas"),
            (btncompras, "Compras"), (btnproductos, "Productos"), (btncategoria, "Categoría"),
            (btn_clientes, "Clientes"), (btn_usuarios, "Usuarios"), (btnproveedores, "Proveedores"),
            (btncredito, "Crédito"), (btndevolucion, "Devolución"), (btnreporte, "Reporte"),
            (btnmantenimiento, "Mantenimiento"), (btnacercade, "Acerca de")
        };

        foreach (var (control, modulo) in accesos)
        {
            bool permitido = PuedeAcceder(modulo);
            control.Enabled = permitido;
            control.TabStop = permitido;
            if (!permitido)
            {
                _tooltipPermisos.SetToolTip(control, $"Tu rol ({_sesion.Rol}) no tiene acceso a {modulo}.");
                control.ForeColor = Color.FromArgb(150, 148, 142);
            }
        }
    }

    private static string NormalizarRol(string rol)
    {
        string normalizado = rol.Normalize(NormalizationForm.FormD);
        var caracteres = normalizado
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray();
        return new string(caracteres).Normalize(NormalizationForm.FormC).ToLowerInvariant();
    }

    private bool PuedeAcceder(string modulo) =>
        _modulosPermitidos.Contains("*") || _modulosPermitidos.Contains(modulo);

    private bool VerificarAcceso(string modulo)
    {
        if (PuedeAcceder(modulo))
        {
            return true;
        }

        MessageBox.Show(
            $"Tu rol ({_sesion.Rol}) no tiene permiso para acceder al módulo {modulo}.",
            "Acceso restringido",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        return false;
    }

    private void OrganizarMenuLateral()
    {
        if (Panel_Padre.IsDisposed || ClientSize.Width <= 0 || ClientSize.Height <= 0)
        {
            return;
        }

        int anchoMenu = Math.Clamp(ClientSize.Width / 7, 190, 251);
        Panel_Padre.Width = anchoMenu;

        guna2Panel1.SetBounds(0, 5, anchoMenu, 93);
        panel1.SetBounds(12, 7, Math.Max(1, anchoMenu - 24), 83);

        if (_btnInicio is not null)
        {
            _btnInicio.SetBounds(0, guna2Panel1.Bottom + 2, anchoMenu, 42);
            _btnInicio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        var botones = new[]
        {
            btn_usuarios,
            btn_clientes,
            btnproveedores,
            btncategoria,
            btncompras,
            btnproductos,
            btncaja,
            btnventas,
            btndevolucion,
            btncredito,
            btnreporte,
            btnmantenimiento,
            btnacercade
        };

        int inicioY = guna2Panel1.Bottom + 46;
        int altoCerrar = 46;
        int margenInferior = 6;
        int cantidadBotonesVisibles = new[]
        {
            btn_usuarios, btn_clientes, btnproveedores, btncategoria, btncompras,
            btnproductos, btncaja, btnventas, btndevolucion, btncredito,
            btnreporte, btnmantenimiento, btnacercade
        }.Count(b => b.Visible);
        int espacioDisponible = Math.Max(1, Panel_Padre.ClientSize.Height - inicioY - altoCerrar - margenInferior);
        int altoBoton = cantidadBotonesVisibles == 0 ? 42 : Math.Clamp(espacioDisponible / cantidadBotonesVisibles, 34, 48);
        bool necesitaScroll = altoBoton * cantidadBotonesVisibles > espacioDisponible;

        Panel_Padre.AutoScroll = necesitaScroll;
        int anchoInterior = Math.Max(1, anchoMenu - (necesitaScroll ? SystemInformation.VerticalScrollBarWidth : 0));
        _btnInicio?.SetBounds(0, guna2Panel1.Bottom + 2, anchoInterior, 42);
        int y = inicioY;

        foreach (var boton in botones)
        {
            if (!boton.Visible)
            {
                continue;
            }

            boton.SetBounds(0, y, anchoInterior, altoBoton);
            boton.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            y += altoBoton;
        }

        if (necesitaScroll)
        {
            int altoContenido = y + 4;
            guna2Button1.SetBounds(0, altoContenido + 4, anchoInterior, altoCerrar);
            Panel_Padre.AutoScrollMinSize = new Size(0, guna2Button1.Bottom + margenInferior);
        }
        else
        {
            guna2Button1.SetBounds(0, Panel_Padre.ClientSize.Height - altoCerrar, anchoInterior, altoCerrar);
            Panel_Padre.AutoScrollMinSize = Size.Empty;
        }

        guna2Button1.Anchor = AnchorStyles.Left | AnchorStyles.Right |
                             (necesitaScroll ? AnchorStyles.Top : AnchorStyles.Bottom);
    }

    public void AbrirFormularioEnPanel(Form formulario)
    {
        foreach (Control control in Panel_Hijo.Controls.Cast<Control>().ToList())
        {
            control.Dispose();
        }

        Panel_Hijo.Controls.Clear();

        // Se captura primero la geometría original del Designer. Después el Dock
        // puede cambiar el tamaño y el adaptador recalcula todos los controles.
        formulario.TopLevel = false;
        formulario.FormBorderStyle = FormBorderStyle.None;
        formulario.Dock = DockStyle.Fill;
        ResponsiveForms.Register(formulario);

        Panel_Hijo.Controls.Add(formulario);
        Panel_Hijo.Tag = formulario;
        formulario.BringToFront();
        formulario.Show();
    }

    private async void Main_Load(object sender, EventArgs e)
    {
        OrganizarMenuLateral();
        if (PuedeAcceder("Caja") || PuedeAcceder("Ventas"))
        {
            await AsegurarCajaAbiertaAsync(mostrarPantallaSiFalta: true);
        }
        await ActualizarTituloAsync();
        await MostrarDashboardAsync();
    }

    private async Task MostrarDashboardAsync()
    {
        foreach (Control control in Panel_Hijo.Controls.Cast<Control>().ToList())
        {
            control.Dispose();
        }

        Panel_Hijo.Controls.Clear();
        var dashboard = new DashboardInicio(
            _sesion,
            _idAperturaCaja,
            AppConfig.DbOptions is null ? null : new Dashboard_Service(AppConfig.DbOptions),
            PuedeAcceder,
            () => { if (VerificarAcceso("Ventas")) btnventas_Click(this, EventArgs.Empty); },
            () => { if (VerificarAcceso("Compras")) btncompras_Click(this, EventArgs.Empty); },
            () => { if (VerificarAcceso("Productos")) btnproductos_Click(this, EventArgs.Empty); },
            () => { if (VerificarAcceso("Clientes")) btn_clientes_Click_1(this, EventArgs.Empty); },
            () => { if (VerificarAcceso("Caja")) btncaja_Click(this, EventArgs.Empty); });
        ResponsiveForms.Register(dashboard);
        Panel_Hijo.Controls.Add(dashboard);
        dashboard.BringToFront();
        await dashboard.CargarAsync();
    }

    private async Task<bool> AsegurarCajaAbiertaAsync(bool mostrarPantallaSiFalta)
    {
        if (_cajaService is null || _sesion.IdUsuario <= 0)
        {
            return _idAperturaCaja > 0;
        }

        try
        {
            var activa = await _cajaService.ObtenerAperturaActivaAsync(_sesion.IdUsuario);
            if (activa is not null)
            {
                _idAperturaCaja = activa.IdAperturaCaja;
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.GetBaseException().Message,
                "Caja",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        _idAperturaCaja = 0;
        if (!mostrarPantallaSiFalta)
        {
            return false;
        }

        using var apertura = new Formapertura(_sesion);
        if (apertura.ShowDialog(this) != DialogResult.OK || apertura.IdAperturaCaja <= 0)
        {
            return false;
        }

        _idAperturaCaja = apertura.IdAperturaCaja;
        await ActualizarTituloAsync();
        return true;
    }

    private async Task ActualizarTituloAsync()
    {
        if (AppConfig.DbOptions is null || _idAperturaCaja <= 0)
        {
            Text = $"NK Style Point | {_sesion.NombreCompleto} | Caja: sin apertura";
            return;
        }

        try
        {
            var dashboard = new Dashboard_Service(AppConfig.DbOptions);
            var resumen = await dashboard.ObtenerAsync(_idAperturaCaja);

            string estadoCaja = resumen.CajaAbierta ? "Abierta" : "Cerrada";
            Text = $"NK Style Point | {_sesion.NombreCompleto} | Caja: {estadoCaja}";
        }
        catch
        {
            Text = $"NK Style Point | {_sesion.NombreCompleto}";
        }
    }

    private async void btncaja_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Caja")) return;
        if (!await AsegurarCajaAbiertaAsync(mostrarPantallaSiFalta: true))
        {
            return;
        }

        var caja = new Caja_premiun(_sesion, _idAperturaCaja);
        caja.CajaCerrada += async (_, _) =>
        {
            _idAperturaCaja = 0;
            await ActualizarTituloAsync();
        };

        AbrirFormularioEnPanel(caja);
    }

    private async void btnventas_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Ventas")) return;
        // Se vuelve a comprobar contra PostgreSQL en cada entrada a Ventas.
        // Si la caja se cerró desde otro formulario/sesión, se muestra la
        // pantalla de apertura en vez de dejar que la venta falle al final.
        if (!await AsegurarCajaAbiertaAsync(mostrarPantallaSiFalta: true))
        {
            MessageBox.Show(
                "Debe realizar una apertura de caja antes de registrar ventas.",
                "Ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        AbrirFormularioEnPanel(new Form_ventas(_sesion));
    }

    private void btncompras_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Compras")) return;
        AbrirFormularioEnPanel(new form_compras(_sesion.IdUsuario));
    }

    private void btnproductos_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Productos")) return;
        AbrirFormularioEnPanel(new Frm_producto());
    }

    private void btncategoria_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Categoría")) return;
        AbrirFormularioEnPanel(new Frm_catalogo_rapido(TipoCatalogoRapido.Categoria));
    }

    private void btn_clientes_Click_1(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Clientes")) return;
        AbrirFormularioEnPanel(new Form_clientes());
    }

    private void btn_usuarios_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Usuarios")) return;
        AbrirFormularioEnPanel(new Form_usuario_principal());
    }

    private void btnproveedores_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Proveedores")) return;
        AbrirFormularioEnPanel(new Form_proveedores_principal(_sesion));
    }

    private void btnmantenimiento_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Mantenimiento")) return;
        AbrirFormularioEnPanel(new Form_mantenimiento());
    }

    private void btnreporte_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Reporte")) return;
        AbrirFormularioEnPanel(new Form_reporte());
    }

    private void btndevolucion_Click(object sender, EventArgs e)
    {
        if (!VerificarAcceso("Devolución")) return;
        AbrirFormularioEnPanel(new Form_devoluciones());
    }

    private void CerrarSesion_Click(object? sender, EventArgs e)
    {
        var respuesta = MessageBox.Show(
            "¿Desea cerrar la sesión actual?",
            "Cerrar sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (respuesta == DialogResult.Yes)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void AcercaDe_Click(object? sender, EventArgs e)
    {
        MessageBox.Show(
            "NK Style Point\n\n" +
            "Sistema de gestión de ventas, compras, productos, clientes y caja.\n" +
            "Conectado a la base de datos NK_COLLECTION.",
            "Acerca de",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void Panel_Padre_Paint(object sender, PaintEventArgs e)
    {
    }

    private void Panel_Hijo_Paint(object sender, PaintEventArgs e)
    {
    }

    // Eventos heredados del Designer original.
    private void button4_Click(object sender, EventArgs e) => btnventas_Click(sender, e);
    private void btn_Clientes_Click(object sender, EventArgs e) => btn_clientes_Click_1(sender, e);
    private void button2_Click(object sender, EventArgs e) => btnproveedores_Click(sender, e);
    private void button5_Click(object sender, EventArgs e) => btncaja_Click(sender, e);
    private void button8_Click(object sender, EventArgs e) => btnmantenimiento_Click(sender, e);
    private void button9_Click(object sender, EventArgs e) => btnreporte_Click(sender, e);
    private void button11_Click(object sender, EventArgs e) => AcercaDe_Click(sender, e);
    private void button12_Click(object sender, EventArgs e)
    {
        if (VerificarAcceso("Crédito")) AbrirFormularioEnPanel(new Form_credito());
    }
    private void button13_Click(object sender, EventArgs e) => btndevolucion_Click(sender, e);
}
