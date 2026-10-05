using Guna.UI2.WinForms;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class TemaNk
{
    private static readonly Color Fondo = Color.FromArgb(245, 241, 232);
    private static readonly Color Tarjeta = Color.FromArgb(235, 222, 208);
    private static readonly Color Lateral = Color.FromArgb(63, 65, 64);
    private static readonly Color Texto = Color.FromArgb(41, 42, 40);
    private static readonly Color Secundario = Color.FromArgb(119, 116, 109);
    private static readonly Color Principal = Color.FromArgb(184, 149, 85);
    private static readonly Color Hover = Color.FromArgb(140, 106, 56);
    private static readonly Color Complementario = Color.FromArgb(101, 112, 90);
    private static readonly Color Error = Color.FromArgb(171, 84, 69);
    private static readonly Color Borde = Color.FromArgb(214, 204, 190);
    private static readonly Color Superficie = Color.FromArgb(253, 251, 247);

    public static void Aplicar(Form formulario)
    {
        formulario.BackColor = Fondo;
        formulario.ForeColor = Texto;
        formulario.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        AplicarControles(formulario);
    }

    private static void AplicarControles(Control raiz)
    {
        foreach (Control control in raiz.Controls)
        {
            if (control is Guna2Panel panel)
            {
                panel.BackColor = Fondo;
                if (EsBlanco(panel.FillColor))
                {
                    panel.FillColor = Tarjeta;
                }

                panel.BorderRadius = Math.Max(panel.BorderRadius, 10);
                panel.ShadowDecoration.Enabled = true;
                panel.ShadowDecoration.Depth = Math.Max(panel.ShadowDecoration.Depth, 5);

                if (panel.BorderThickness == 0)
                {
                    panel.BorderColor = Borde;
                    panel.BorderThickness = 1;
                }
            }
            else if (control is Guna2ShadowPanel tarjeta)
            {
                if (EsBlanco(tarjeta.FillColor))
                {
                    tarjeta.FillColor = Tarjeta;
                }

                tarjeta.ForeColor = Texto;
                tarjeta.ShadowColor = Secundario;
                tarjeta.ShadowDepth = 16;
                tarjeta.ShadowShift = 2;
            }
            else if (control is Guna2Button boton)
            {
                AplicarBoton(boton, raiz.FindForm()?.Name == "Main");
            }
            else if (control is Guna2TextBox texto)
            {
                texto.BorderRadius = Math.Max(texto.BorderRadius, 8);
                texto.FillColor = Superficie;
                texto.ForeColor = Texto;
                if (EsColor(texto.BorderColor, 216, 216, 216) ||
                    EsColor(texto.BorderColor, 214, 204, 190))
                {
                    texto.BorderColor = Borde;
                }

                if (EsAzul(texto.FocusedState.BorderColor) ||
                    EsColor(texto.FocusedState.BorderColor, 140, 106, 56))
                {
                    texto.FocusedState.BorderColor = Principal;
                }

                if (EsAzul(texto.HoverState.BorderColor) ||
                    EsColor(texto.HoverState.BorderColor, 140, 106, 56))
                {
                    texto.HoverState.BorderColor = Hover;
                }
            }
            else if (control is Guna2ComboBox combo)
            {
                combo.BorderRadius = Math.Max(combo.BorderRadius, 8);
                if (EsColor(combo.ForeColor, 68, 88, 112))
                {
                    combo.ForeColor = Texto;
                }

                if (EsAzul(combo.FocusedColor) || EsColor(combo.FocusedColor, 140, 106, 56))
                {
                    combo.FocusedColor = Principal;
                }

                if (EsAzul(combo.FocusedState.BorderColor) ||
                    EsColor(combo.FocusedState.BorderColor, 140, 106, 56))
                {
                    combo.FocusedState.BorderColor = Principal;
                }
            }
            else if (control is Guna2DateTimePicker fecha)
            {
                fecha.BorderRadius = Math.Max(fecha.BorderRadius, 8);
                fecha.FillColor = Superficie;
                fecha.ForeColor = Texto;
            }
            else if (control is DataGridView grilla)
            {
                AplicarGrilla(grilla);
            }
            else if (control is TextBox cajaTexto)
            {
                cajaTexto.BackColor = Superficie;
                cajaTexto.ForeColor = Texto;
                cajaTexto.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox lista)
            {
                lista.BackColor = Superficie;
                lista.ForeColor = Texto;
                lista.FlatStyle = FlatStyle.Flat;
            }
            else if (control is Button botonNormal)
            {
                botonNormal.FlatStyle = FlatStyle.Flat;
                botonNormal.Cursor = Cursors.Hand;
                botonNormal.FlatAppearance.BorderSize = 0;
                if (EsColor(botonNormal.BackColor, 184, 149, 85) ||
                    EsColor(botonNormal.BackColor, 171, 84, 69))
                {
                    botonNormal.ForeColor = Color.White;
                    botonNormal.FlatAppearance.MouseOverBackColor = Hover;
                    botonNormal.FlatAppearance.MouseDownBackColor = Complementario;
                }
                else
                {
                    botonNormal.FlatAppearance.BorderSize = 1;
                    botonNormal.FlatAppearance.BorderColor = Borde;
                    botonNormal.BackColor = Tarjeta;
                    botonNormal.ForeColor = Texto;
                    botonNormal.FlatAppearance.MouseOverBackColor = Borde;
                }
            }
            else if (control is Panel panelNormal)
            {
                if (EsColor(panelNormal.BackColor, 64, 0, 0))
                {
                    panelNormal.BackColor = Lateral;
                }
                else if (panelNormal.Name == "Panel_Padre")
                {
                    panelNormal.BackColor = Lateral;
                }
            }
            else if (control is Label etiqueta)
            {
                bool eraSecundario = EsColor(etiqueta.ForeColor, 128, 128, 128) ||
                    EsColor(etiqueta.ForeColor, 119, 116, 109) ||
                    EsColor(etiqueta.ForeColor, 205, 205, 205);
                if (eraSecundario)
                {
                    etiqueta.ForeColor = Secundario;
                }
                else if (EsColor(etiqueta.ForeColor, 110, 18, 32) ||
                         EsColor(etiqueta.ForeColor, 64, 0, 0))
                {
                    etiqueta.ForeColor = Texto;
                }

                if (etiqueta.Font.Size >= 18F)
                {
                    etiqueta.Font = new Font("Segoe UI Semibold", etiqueta.Font.Size, FontStyle.Bold);
                }
                else if (etiqueta.Font.Size >= 12F)
                {
                    etiqueta.Font = new Font("Segoe UI Semibold", etiqueta.Font.Size, FontStyle.Regular);
                }
            }
            else if (control is GroupBox grupo)
            {
                grupo.ForeColor = Texto;
            }

            AplicarControles(control);
        }
    }

    private static void AplicarBoton(Guna2Button boton, bool esMenuPrincipal)
    {
        boton.BorderRadius = Math.Max(boton.BorderRadius, 8);
        boton.Cursor = Cursors.Hand;
        boton.Font = new Font("Segoe UI Semibold", Math.Max(9F, boton.Font.Size), FontStyle.Bold);
        if (!esMenuPrincipal && ObtenerAcento(boton.Text, boton.Name) is Color acento)
        {
            boton.FillColor = acento;
            boton.ForeColor = Color.White;
            boton.BorderColor = acento;
            boton.HoverState.FillColor = Oscurecer(acento);
            boton.HoverState.ForeColor = Color.White;
        }
        else if (EsColor(boton.FillColor, 110, 18, 32))
        {
            boton.FillColor = Principal;
            boton.BorderColor = Principal;
            boton.HoverState.FillColor = Hover;
            boton.HoverState.ForeColor = Color.White;
        }
        else if (esMenuPrincipal &&
                 (EsBlanco(boton.FillColor) || EsColor(boton.FillColor, 235, 222, 208)))
        {
            boton.FillColor = Lateral;
            boton.ForeColor = Tarjeta;
            boton.HoverState.FillColor = Complementario;
            boton.HoverState.ForeColor = Color.White;
        }
        else if (EsColor(boton.FillColor, 184, 149, 85))
        {
            boton.HoverState.FillColor = Hover;
            boton.HoverState.ForeColor = Color.White;
        }
        else if (EsColor(boton.FillColor, 63, 65, 64))
        {
            boton.ForeColor = Tarjeta;
            boton.HoverState.FillColor = Principal;
            boton.HoverState.ForeColor = Color.White;
        }
        else if (EsBlanco(boton.FillColor))
        {
            boton.FillColor = Tarjeta;
            boton.ForeColor = Texto;
            boton.HoverState.FillColor = Borde;
            boton.HoverState.ForeColor = Texto;
            if (EsBlanco(boton.BorderColor))
            {
                boton.BorderColor = Borde;
            }
        }

        if (EsColor(boton.FocusedColor, 110, 18, 32))
        {
            boton.FocusedColor = Principal;
        }

        if (EsColor(boton.BorderColor, 194, 154, 116))
        {
            boton.BorderColor = Error;
        }
    }

    private static void AplicarGrilla(DataGridView grilla)
    {
        grilla.EnableHeadersVisualStyles = false;
        grilla.BackgroundColor = Superficie;
        grilla.GridColor = Borde;
        grilla.BorderStyle = BorderStyle.None;
        grilla.RowHeadersVisible = false;
        grilla.ColumnHeadersDefaultCellStyle.BackColor = Lateral;
        grilla.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grilla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Lateral;
        grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
        grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        grilla.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        grilla.DefaultCellStyle.BackColor = Superficie;
        grilla.DefaultCellStyle.ForeColor = Texto;
        grilla.DefaultCellStyle.SelectionBackColor = Complementario;
        grilla.DefaultCellStyle.SelectionForeColor = Color.White;
        grilla.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 241, 232);
        grilla.ColumnHeadersHeight = Math.Max(grilla.ColumnHeadersHeight, 38);
        grilla.RowTemplate.Height = Math.Max(grilla.RowTemplate.Height, 34);
        grilla.DefaultCellStyle.Padding = new Padding(7, 3, 7, 3);
        AplicarEstiloGrilla(grilla.ColumnHeadersDefaultCellStyle, esEncabezado: true);
        AplicarEstiloGrilla(grilla.DefaultCellStyle, esEncabezado: false);
        AplicarEstiloGrilla(grilla.AlternatingRowsDefaultCellStyle, esEncabezado: false);

        if (grilla is Guna2DataGridView gunaGrilla)
        {
            gunaGrilla.ThemeStyle.HeaderStyle.BackColor = Lateral;
            gunaGrilla.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            gunaGrilla.ThemeStyle.RowsStyle.ForeColor = Texto;
            gunaGrilla.ThemeStyle.RowsStyle.SelectionBackColor = Complementario;
            gunaGrilla.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;
            gunaGrilla.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(245, 241, 232);
            gunaGrilla.ThemeStyle.GridColor = Borde;
        }
    }

    private static void AplicarEstiloGrilla(DataGridViewCellStyle estilo, bool esEncabezado)
    {
        if (esEncabezado)
        {
            estilo.BackColor = Lateral;
            estilo.ForeColor = Color.White;
        }
        else
        {
            estilo.ForeColor = Texto;
            estilo.SelectionBackColor = Complementario;
            estilo.SelectionForeColor = Color.White;
        }
    }

    private static bool EsAzul(Color color) =>
        EsColor(color, 94, 148, 255);

    private static Color? ObtenerAcento(string texto, string nombre)
    {
        string clave = $"{nombre} {texto}".ToLowerInvariant();
        if (clave.Contains("eliminar") || clave.Contains("cancelar") || clave.Contains("cerrar sesión") || clave.Contains("cerrar sesion"))
        {
            return Error;
        }

        if (clave.Contains("guardar") || clave.Contains("registrar") || clave.Contains("ingresar") || clave.Contains("confirmar") || clave.Contains("aceptar"))
        {
            return Principal;
        }

        if (clave.Contains("buscar") || clave.Contains("actualizar") || clave.Contains("consultar") || clave.Contains("reintentar"))
        {
            return Complementario;
        }

        return null;
    }

    private static Color Oscurecer(Color color) =>
        Color.FromArgb(Math.Max(0, color.R - 22), Math.Max(0, color.G - 22), Math.Max(0, color.B - 22));

    private static bool EsBlanco(Color color) =>
        color.ToArgb() == Color.White.ToArgb();

    private static bool EsColor(Color color, int rojo, int verde, int azul) =>
        color.ToArgb() == Color.FromArgb(rojo, verde, azul).ToArgb();
}
