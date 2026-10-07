using Guna.UI2.WinForms;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class TemaNk
{
    private static readonly Color Fondo = Color.FromArgb(250, 249, 246);
    private static readonly Color Tarjeta = Color.FromArgb(232, 221, 202);
    private static readonly Color Lateral = Color.FromArgb(74, 14, 24);
    private static readonly Color Texto = Color.FromArgb(41, 41, 41);
    private static readonly Color Secundario = Color.FromArgb(119, 119, 119);
    private static readonly Color Principal = Color.FromArgb(74, 14, 24);
    private static readonly Color Acento = Color.FromArgb(196, 154, 69);
    private static readonly Color Hover = Color.FromArgb(107, 23, 37);
    private static readonly Color Complementario = Color.FromArgb(107, 23, 37);
    private static readonly Color Error = Color.FromArgb(74, 14, 24);
    private static readonly Color Borde = Color.FromArgb(229, 227, 223);
    private static readonly Color Superficie = Color.FromArgb(250, 249, 246);

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
                bool panelLateral = raiz.FindForm()?.Name == "Main" && panel.Name == "guna2Panel1";
                panel.BackColor = panelLateral ? Lateral : Fondo;
                panel.FillColor = panelLateral ? Lateral : MapearColor(panel.FillColor, Tarjeta);

                panel.BorderRadius = Math.Max(panel.BorderRadius, 10);
                panel.ShadowDecoration.Enabled = true;
                panel.ShadowDecoration.Depth = Math.Max(panel.ShadowDecoration.Depth, 5);

                if (panel.BorderThickness == 0)
                {
                    panel.BorderColor = Borde;
                    panel.BorderThickness = 1;
                }
                else
                {
                    panel.BorderColor = Borde;
                }
            }
            else if (control is Guna2ShadowPanel tarjeta)
            {
                bool tarjetaVino = tarjeta.FillColor.ToArgb() == Principal.ToArgb() ||
                    tarjeta.FillColor.ToArgb() == Hover.ToArgb();
                tarjeta.FillColor = tarjetaVino ? Principal : MapearColor(tarjeta.FillColor, Tarjeta);

                tarjeta.ForeColor = tarjetaVino ? Fondo : Texto;
                tarjeta.ShadowColor = Borde;
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
                texto.FillColor = MapearColor(texto.FillColor, Superficie);
                texto.ForeColor = Texto;
                texto.BorderColor = Borde;
                texto.BorderThickness = Math.Max(texto.BorderThickness, 1);
                texto.FocusedState.BorderColor = Principal;
                texto.HoverState.BorderColor = Hover;
                texto.PlaceholderForeColor = Secundario;
            }
            else if (control is Guna2ComboBox combo)
            {
                combo.BorderRadius = Math.Max(combo.BorderRadius, 8);
                combo.FillColor = MapearColor(combo.FillColor, Tarjeta);
                combo.ForeColor = Texto;
                combo.BorderColor = Borde;
                combo.BorderThickness = Math.Max(combo.BorderThickness, 1);
                combo.FocusedColor = Principal;
                combo.FocusedState.BorderColor = Principal;
            }
            else if (control is Guna2DateTimePicker fecha)
            {
                fecha.BorderRadius = Math.Max(fecha.BorderRadius, 8);
                fecha.FillColor = MapearColor(fecha.FillColor, Tarjeta);
                fecha.ForeColor = Texto;
                fecha.BorderColor = Borde;
                fecha.BorderThickness = Math.Max(fecha.BorderThickness, 1);
            }
            else if (control is DataGridView grilla)
            {
                AplicarGrilla(grilla);
            }
            else if (control is TextBox cajaTexto)
            {
                cajaTexto.BackColor = MapearColor(cajaTexto.BackColor, Superficie);
                cajaTexto.ForeColor = Texto;
                cajaTexto.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox lista)
            {
                lista.BackColor = MapearColor(lista.BackColor, Superficie);
                lista.ForeColor = Texto;
                lista.FlatStyle = FlatStyle.Flat;
            }
            else if (control is Button botonNormal)
            {
                botonNormal.FlatStyle = FlatStyle.Flat;
                botonNormal.Cursor = Cursors.Hand;
                bool esVino = botonNormal.BackColor.ToArgb() == Principal.ToArgb() ||
                    botonNormal.BackColor.ToArgb() == Hover.ToArgb() ||
                    botonNormal.BackColor.ToArgb() == Error.ToArgb();
                botonNormal.FlatAppearance.BorderSize = 1;
                botonNormal.FlatAppearance.BorderColor = Borde;
                if (esVino)
                {
                    botonNormal.ForeColor = Fondo;
                    botonNormal.FlatAppearance.MouseOverBackColor = Hover;
                    botonNormal.FlatAppearance.MouseDownBackColor = Complementario;
                }
                else
                {
                    botonNormal.ForeColor = Texto;
                    botonNormal.BackColor = EsColor(botonNormal.BackColor, 74, 14, 24) ||
                        EsColor(botonNormal.BackColor, 107, 23, 37)
                        ? Principal
                        : MapearColor(botonNormal.BackColor, Tarjeta);
                    botonNormal.FlatAppearance.MouseOverBackColor = Borde;
                }
            }
            else if (control is Panel panelNormal)
            {
                bool esMenu = panelNormal.Name == "Panel_Padre" ||
                    (raiz.FindForm()?.Name == "Main" && panelNormal.Name is "panel2" or "panel3" or "panel4" or "panel5");
                panelNormal.BackColor = MapearColor(panelNormal.BackColor, esMenu ? Lateral : Fondo);
            }
            else if (control is Label etiqueta)
            {
                bool eraSecundario = EsColor(etiqueta.ForeColor, 128, 128, 128) ||
                    EsColor(etiqueta.ForeColor, 119, 116, 109) ||
                    EsColor(etiqueta.ForeColor, 205, 205, 205) ||
                    EsColor(etiqueta.ForeColor, 119, 116, 109);
                etiqueta.ForeColor = eraSecundario ? Secundario : Texto;

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
        if (ObtenerAcento(boton.Text, boton.Name) is Color acento)
        {
            boton.FillColor = acento;
            boton.ForeColor = Fondo;
            boton.BorderColor = Borde;
            boton.BorderThickness = Math.Max(boton.BorderThickness, 1);
            boton.HoverState.FillColor = Oscurecer(acento);
            boton.HoverState.ForeColor = Fondo;
            boton.HoverState.BorderColor = Borde;
        }
        else if (esMenuPrincipal)
        {
            boton.FillColor = Lateral;
            boton.ForeColor = Fondo;
            boton.BorderColor = Borde;
            boton.BorderThickness = Math.Max(boton.BorderThickness, 1);
            boton.HoverState.FillColor = Hover;
            boton.HoverState.ForeColor = Fondo;
            boton.HoverState.BorderColor = Borde;
        }
        else
        {
            boton.FillColor = MapearColor(boton.FillColor, Tarjeta);
            boton.ForeColor = Texto;
            boton.BorderColor = Borde;
            boton.BorderThickness = Math.Max(boton.BorderThickness, 1);
            boton.HoverState.FillColor = Hover;
            boton.HoverState.ForeColor = Color.White;
            boton.HoverState.BorderColor = Borde;
        }

        boton.FocusedColor = Principal;
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
        grilla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Fondo;
        grilla.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
        grilla.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        grilla.DefaultCellStyle.BackColor = Superficie;
        grilla.DefaultCellStyle.ForeColor = Texto;
        grilla.DefaultCellStyle.SelectionBackColor = Complementario;
        grilla.DefaultCellStyle.SelectionForeColor = Fondo;
        grilla.AlternatingRowsDefaultCellStyle.BackColor = Fondo;
        grilla.ColumnHeadersHeight = Math.Max(grilla.ColumnHeadersHeight, 38);
        grilla.RowTemplate.Height = Math.Max(grilla.RowTemplate.Height, 34);
        grilla.DefaultCellStyle.Padding = new Padding(7, 3, 7, 3);
        AplicarEstiloGrilla(grilla.ColumnHeadersDefaultCellStyle, esEncabezado: true);
        AplicarEstiloGrilla(grilla.DefaultCellStyle, esEncabezado: false);
        AplicarEstiloGrilla(grilla.AlternatingRowsDefaultCellStyle, esEncabezado: false);

        if (grilla is Guna2DataGridView gunaGrilla)
        {
            gunaGrilla.ThemeStyle.HeaderStyle.BackColor = Lateral;
            gunaGrilla.ThemeStyle.HeaderStyle.ForeColor = Fondo;
            gunaGrilla.ThemeStyle.RowsStyle.ForeColor = Texto;
            gunaGrilla.ThemeStyle.RowsStyle.SelectionBackColor = Complementario;
            gunaGrilla.ThemeStyle.RowsStyle.SelectionForeColor = Fondo;
            gunaGrilla.ThemeStyle.AlternatingRowsStyle.BackColor = Fondo;
            gunaGrilla.ThemeStyle.GridColor = Borde;
        }
    }

    private static void AplicarEstiloGrilla(DataGridViewCellStyle estilo, bool esEncabezado)
    {
        if (esEncabezado)
        {
            estilo.BackColor = Lateral;
            estilo.ForeColor = Fondo;
        }
        else
        {
            estilo.ForeColor = Texto;
            estilo.SelectionBackColor = Complementario;
            estilo.SelectionForeColor = Fondo;
        }
    }

    private static Color? ObtenerAcento(string texto, string nombre)
    {
        string clave = $"{nombre} {texto}".ToLowerInvariant();
        if (clave.Contains("eliminar") || clave.Contains("cancelar") || clave.Contains("cerrar sesión") || clave.Contains("cerrar sesion"))
        {
            return Error;
        }

        if (clave.Contains("guardar") || clave.Contains("registrar") || clave.Contains("ingresar") || clave.Contains("confirmar") || clave.Contains("aceptar"))
        {
            return Acento;
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

    private static Color MapearColor(Color color, Color predeterminado)
    {
        if (EsBlanco(color)) return Fondo;
        if (EsColor(color, 245, 241, 232) || EsColor(color, 253, 251, 247) ||
            EsColor(color, 255, 255, 255)) return Fondo;
        if (EsColor(color, 235, 222, 208) || EsColor(color, 63, 65, 64) ||
            EsColor(color, 41, 42, 40)) return Tarjeta;
        if (EsColor(color, 184, 149, 85) || EsColor(color, 201, 154, 69)) return Acento;
        if (EsColor(color, 140, 106, 56) || EsColor(color, 101, 112, 90) ||
            EsColor(color, 100, 28, 45) || EsColor(color, 139, 38, 61)) return Principal;
        if (EsColor(color, 214, 204, 190) || EsColor(color, 194, 154, 116) ||
            EsColor(color, 70, 70, 70)) return Borde;
        if (EsColor(color, 119, 116, 109)) return Secundario;
        if (EsColor(color, 171, 84, 69) || EsColor(color, 64, 0, 0) ||
            EsColor(color, 70, 18, 31)) return Error;
        return predeterminado;
    }
}
