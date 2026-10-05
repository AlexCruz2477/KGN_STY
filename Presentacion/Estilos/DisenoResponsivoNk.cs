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
            formulario.AutoScaleMode = AutoScaleMode.None;
        }

        private static Size ObtenerTamanoDiseno(Form formulario)
        {
            var controles = formulario.Controls.Cast<Control>()
                .Where(c => c.Visible &&
                            c.Dock == DockStyle.None &&
                            c.Width > 12 &&
                            c.Height > 12 &&
                            c.Left < formulario.ClientSize.Width + 80 &&
                            c.Top < formulario.ClientSize.Height + 80)
                .ToList();

            if (controles.Count == 0)
            {
                return new Size(
                    Math.Max(1, formulario.ClientSize.Width),
                    Math.Max(1, formulario.ClientSize.Height));
            }

            int margenX = controles.Where(c => c.Left > 0).Select(c => c.Left).DefaultIfEmpty(0).Min();
            int margenY = controles.Where(c => c.Top > 0).Select(c => c.Top).DefaultIfEmpty(0).Min();

            return new Size(
                Math.Max(formulario.ClientSize.Width, controles.Max(c => c.Right) + margenX),
                Math.Max(formulario.ClientSize.Height, controles.Max(c => c.Bottom) + margenY));
        }

        private void Capturar(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                // Los controles heredados que quedaron fuera del lienzo del diseñador
                // no deben afectar el escalado de la interfaz actual.
                if (!control.Visible && control.Dock == DockStyle.None)
                {
                    continue;
                }

                _registros[control] = new RegistroControl(
                    control.Bounds,
                    contenedor == _formulario ? _tamanoOriginal : TamanoSeguro(contenedor.ClientSize),
                    control.Font,
                    control.Dock,
                    control is DataGridView grid ? grid.RowTemplate.Height : 0,
                    control is Guna.UI2.WinForms.Guna2Button boton ? boton.ImageSize : Size.Empty);

                if (control.Dock == DockStyle.None)
                {
                    control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                }

                if (EsContenedor(control))
                {
                    Capturar(control);
                }
            }
        }

        private static Size TamanoSeguro(Size size) =>
            new(Math.Max(1, size.Width), Math.Max(1, size.Height));

        private static bool EsContenedor(Control control) =>
            control is Panel or TabControl or TabPage or GroupBox or
            Guna.UI2.WinForms.Guna2ShadowPanel or Guna.UI2.WinForms.Guna2Panel;

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
                contenedor.ResumeLayout(true);
            }

            foreach (Control control in contenedor.Controls)
            {
                if (EsContenedor(control) && !control.IsDisposed)
                {
                    AjustarContenedor(control);
                }
            }
        }

        private static void AjustarControl(Control control, Control parent, RegistroControl registro)
        {
            if (registro.ParentOriginal.Width <= 0 || registro.ParentOriginal.Height <= 0)
            {
                return;
            }

            if (registro.Dock != DockStyle.None)
            {
                if (control is DataGridView dockedGrid)
                {
                    PrepararGrid(dockedGrid, 1f, registro.AltoFila);
                }
                return;
            }

            float sx = (float)Math.Max(1, parent.ClientSize.Width) / registro.ParentOriginal.Width;
            float sy = (float)Math.Max(1, parent.ClientSize.Height) / registro.ParentOriginal.Height;
            float escalaFuente = Math.Min(sx, sy);

            var bounds = registro.BoundsOriginal;
            control.Bounds = new Rectangle(
                Math.Max(0, (int)Math.Round(bounds.X * sx)),
                Math.Max(0, (int)Math.Round(bounds.Y * sy)),
                Math.Max(1, (int)Math.Round(bounds.Width * sx)),
                Math.Max(1, (int)Math.Round(bounds.Height * sy)));

            AjustarFuente(control, registro, escalaFuente);

            if (control is DataGridView grid)
            {
                PrepararGrid(grid, escalaFuente, registro.AltoFila);
            }

            if (control is Guna.UI2.WinForms.Guna2Button boton)
            {
                AjustarBoton(boton, registro, escalaFuente);
            }
        }

        private static void PrepararGrid(DataGridView grid, float escala, int altoOriginal)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowTemplate.Height = Math.Max(20, (int)Math.Round(Math.Max(22, altoOriginal) * Math.Max(.7f, escala)));
            grid.DefaultCellStyle.Font = grid.Font;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private static void AjustarBoton(
            Guna.UI2.WinForms.Guna2Button boton,
            RegistroControl registro,
            float escala)
        {
            if (!registro.ImagenOriginal.IsEmpty)
            {
                boton.ImageSize = new Size(
                    Math.Max(1, (int)Math.Round(registro.ImagenOriginal.Width * Math.Min(escala, 1.2f))),
                    Math.Max(1, (int)Math.Round(registro.ImagenOriginal.Height * Math.Min(escala, 1.2f))));
            }

            int espacioImagen = boton.Image == null ? 0 : boton.ImageSize.Width + 6;
            using var graphics = boton.CreateGraphics();
            while (boton.Font.Size > 6F &&
                   graphics.MeasureString(boton.Text, boton.Font).Width + espacioImagen + 24 > boton.Width)
            {
                boton.Font = new Font(
                    boton.Font.FontFamily,
                    Math.Max(6F, boton.Font.Size - .25F),
                    boton.Font.Style,
                    boton.Font.Unit);
            }
        }

        private static void AjustarFuente(Control control, RegistroControl registro, float escala)
        {
            float tamano = Math.Max(6F, registro.FuenteOriginal.Size * Math.Clamp(escala, .65F, 1.2F));
            if (Math.Abs(control.Font.Size - tamano) < .1F)
            {
                return;
            }

            control.Font = new Font(
                registro.FuenteOriginal.FontFamily,
                tamano,
                registro.FuenteOriginal.Style,
                registro.FuenteOriginal.Unit);
        }
    }

    private sealed record RegistroControl(
        Rectangle BoundsOriginal,
        Size ParentOriginal,
        Font FuenteOriginal,
        DockStyle Dock,
        int AltoFila,
        Size ImagenOriginal);
}
