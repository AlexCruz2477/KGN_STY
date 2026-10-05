using System.Runtime.CompilerServices;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class DisenoResponsivoNk
{

    private static readonly ConditionalWeakTable<Form, EstadoFormulario> Estados = new();

    public static void Habilitar(Form formulario)
    {
        if (Estados.TryGetValue(formulario, out _))
        {
            return;
        }

        var estado = new EstadoFormulario(formulario);
        Estados.Add(formulario, estado);
        formulario.Resize += (_, _) => estado.Aplicar();
        formulario.Shown += (_, _) => estado.Aplicar();
        formulario.DpiChanged += (_, _) => estado.Aplicar();
        estado.Aplicar();
    }

    private sealed class EstadoFormulario
    {
        private readonly Form _formulario;

        private readonly Dictionary<Control, RegistroControl> _registros = new();
        private readonly Size _tamanoOriginal;
        private bool _aplicando;

        public EstadoFormulario(Form formulario)
        {
            _formulario = formulario;
            _tamanoOriginal = ObtenerTamanoDiseno(formulario);
            Capturar(formulario);
            // El escalado se calcula desde la geometría original en cada cambio.
            // No se combina con otro escalado por fuente o con los anclajes nativos.
            formulario.AutoScaleMode = AutoScaleMode.None;
        }

        private static Size ObtenerTamanoDiseno(Form formulario)
        {
            var controles = formulario.Controls.Cast<Control>()
                .Where(c => c.Dock == DockStyle.None &&
                c.Width > 12 &&
                c.Height > 12)
                .ToList();
            if (controles.Count == 0)
            {
                return formulario.ClientSize;
            }

            // Windows puede limitar el tamaño inicial de un Form al monitor,
            // aunque el Designer haya colocado controles fuera de esa superficie.
            int margenX = controles.Where(c => c.Left > 0).Select(c => c.Left).DefaultIfEmpty(0).Min();
            int margenY = controles.Where(c => c.Top > 0).Select(c => c.Top).DefaultIfEmpty(0).Min();
            return new Size(
                Math.Max(formulario.ClientSize.Width, controles.Max(c => c.Right) + margenX),
                Math.Max(formulario.ClientSize.Height, controles.Max(c => c.Bottom) + margenY)
            );
        }

        private void Capturar(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                _registros[control] = new RegistroControl(
                    control.Bounds,
                    contenedor == _formulario ? _tamanoOriginal : contenedor.ClientSize,
                    control.Font,
                    control.Dock,
                    control is DataGridView grid ? grid.RowTemplate.Height : 0,
                    control is Guna.UI2.WinForms.Guna2Button boton ? boton.ImageSize : Size.Empty
                );
                if (control.Dock == DockStyle.None)
                {
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }

                // Los controles de entrada y las tablas administran sus propios
                // controles internos; solo adaptamos los contenedores del diseño.
                if (EsContenedor(control))
                {
                    Capturar(control);
                }
            }
        }

        private static bool EsContenedor(Control control)
        {
            return control is Panel or TabControl or TabPage or GroupBox or Guna.UI2.WinForms.Guna2ShadowPanel;
        }

        public void Aplicar()
        {
            if (_aplicando ||
                _formulario.IsDisposed ||
                _formulario.ClientSize.Width <= 0 ||
                _formulario.ClientSize.Height <= 0)
            {
                return;
            }

            _aplicando = true;
            try
            {
                AjustarContenedor(_formulario);
            }
            finally
            {
                _aplicando = false;
            }
        }

        private void AjustarContenedor(Control contenedor)
        {
            contenedor.SuspendLayout();
            try
            {
                foreach (Control control in contenedor.Controls)
                {
                    if (!_registros.TryGetValue(control, out var registro) || control.IsDisposed)
                    {
                        continue;
                    }

                    AjustarControl(control, contenedor, registro);
                }
            }
            finally
            {
                // Primero terminamos el layout del padre para que TabPage y
                // los controles con Dock tengan su tamaño final antes de sus hijos.
                contenedor.ResumeLayout(true);
            }

            foreach (Control control in contenedor.Controls)
            {
                if (EsContenedor(control) && !control.IsDisposed)
                {
                    AjustarContenedor(control);
                }
            }

            if (_formulario.Name == "Frm_venta" && contenedor.Name == "guna2ShadowPanel1")
            {
                AjustarBotonesVenta(contenedor);
            }
        }

        private void AjustarBotonesVenta(Control panel)
        {
            var agregar = panel.Controls.Find("btn_agregar", false).FirstOrDefault();
            var cancelar = panel.Controls.Find("btn_cancelar", false).FirstOrDefault();
            if (agregar == null || cancelar == null)
            {
                return;
            }

            int margen = Math.Max(10, panel.Width / 20);
            int separacion = 8;
            int ancho = Math.Max(1, (panel.Width - margen * 2 - separacion) / 2);
            agregar.SetBounds(margen, agregar.Top, ancho, agregar.Height);
            cancelar.SetBounds(margen + ancho + separacion, cancelar.Top, ancho, cancelar.Height);
            foreach (var boton in new[]
            {
                agregar,
                cancelar
            }

            )
            {
                var original = _registros[boton];
                float escala = Math.Min(
                    (float)panel.Width / original.ParentOriginal.Width,
                    (float)panel.Height / original.ParentOriginal.Height
                );
                AjustarFuente(boton, original, escala);
            }
        }

        private static void AjustarControl(Control control, Control parent, RegistroControl registro)
        {
            if (registro.ParentOriginal.Width <= 0 || registro.ParentOriginal.Height <= 0)
            {
                return;
            }

            float sx = (float)parent.ClientSize.Width / registro.ParentOriginal.Width;
            float sy = (float)parent.ClientSize.Height / registro.ParentOriginal.Height;
            float escalaFuente = Math.Min(sx, sy);
            if (registro.Dock != DockStyle.None)
            {
                return;
            }

            AjustarFuente(control, registro, escalaFuente);
            var bounds = registro.BoundsOriginal;
            control.Bounds = new Rectangle(
                Math.Max(0, (int)Math.Round(bounds.X * sx)),
                Math.Max(0, (int)Math.Round(bounds.Y * sy)),
                Math.Max(1, (int)Math.Round(bounds.Width * sx)),
                Math.Max(1, (int)Math.Round(bounds.Height * sy))
            );
            if (control is DataGridView grid)
            {
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grid.RowTemplate.Height = Math.Max(20, (int)Math.Round(registro.AltoFila * escalaFuente));
                grid.DefaultCellStyle.Font = grid.Font;
                grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
                grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            }

            if (control is Guna.UI2.WinForms.Guna2Button boton)
            {
                boton.ImageSize = new Size(
                    Math.Max(1, (int)Math.Round(registro.ImagenOriginal.Width * escalaFuente)),
                    Math.Max(1, (int)Math.Round(registro.ImagenOriginal.Height * escalaFuente))
                );
                int espacioImagen = boton.Image == null ? 0 : boton.ImageSize.Width + 6;
                using var graphics = boton.CreateGraphics();
                while (boton.Font.Size > 6F &&
                    graphics.MeasureString(boton.Text, boton.Font)
                    .Width + espacioImagen + 26 > boton.Width)
                {
                    boton.Font = new Font(boton.Font.FontFamily, Math.Max(6F, boton.Font.Size - 0.25F), boton.Font.Style);
                }
            }
        }

        private static void AjustarFuente(Control control, RegistroControl registro, float escala)
        {
            float tamano = Math.Max(6F, registro.FuenteOriginal.Size * Math.Min(escala, 1.2F));
            if (Math.Abs(control.Font.Size - tamano) < 0.1F)
            {
                return;
            }

            control.Font = new Font(
                registro.FuenteOriginal.FontFamily,
                tamano,
                registro.FuenteOriginal.Style,
                registro.FuenteOriginal.Unit
            );
        }
    }

    private sealed record RegistroControl(
        Rectangle BoundsOriginal,
        Size ParentOriginal,
        Font FuenteOriginal,
        DockStyle Dock,
        int AltoFila,
        Size ImagenOriginal
    );
}
