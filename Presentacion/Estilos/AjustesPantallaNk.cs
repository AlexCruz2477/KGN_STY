using Guna.UI2.WinForms;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class AjustesPantallaNk
{

    public static void AplicarInicial(Form formulario)
    {
        formulario.AutoScroll = false;
        formulario.Padding = Padding.Empty;
        PrepararEstructuraComun(formulario);
        if (formulario.Name == "Frm_cierre_caja" && Buscar(formulario, "label8")is Label separador)
        {
            var campos = new[]
            {
                "txtAbonos",
                "txtPagos",
                "label18",
                "label17"
            }.Select(nombre => Buscar(formulario, nombre)).OfType<Control>();
            separador.Top = campos.Max(control => control.Bottom) + 8;
        }

        if (formulario.Name == "Frm_venta")
        {
            PrepararVentas(formulario);
        }
    }

    private static void PrepararEstructuraComun(Form formulario)
    {
        //ajustar encabezados
        foreach (Guna2Panel panel in formulario.Controls.OfType<Guna2Panel>())
        {
            if (panel.Top <= 40 && panel.Width >= formulario.ClientSize.Width * 0.70)
            {
                panel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        //ajustar tarjeta principal
        var tarjetas = formulario.Controls.OfType<Guna2ShadowPanel>().ToList();
        if (tarjetas.Count == 1)
        {
            var tarjeta = tarjetas[0];
            if (tarjeta.Width >= formulario.ClientSize.Width * 0.65 &&
                tarjeta.Height >= formulario.ClientSize.Height * 0.45)
            {
                tarjeta.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        //ajustar tablas y pestañas
        foreach (var tabs in BuscarControles<TabControl>(formulario))
        {
            if (tabs.Parent != null && tabs.Width >= tabs.Parent.ClientSize.Width * 0.60)
            {
                tabs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        foreach (var grid in BuscarControles<DataGridView>(formulario))
        {
            if (grid.Parent != null && grid.Width >= grid.Parent.ClientSize.Width * 0.55)
            {
                grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }
        }
    }

    private static void PrepararVentas(Form formulario)
    {
        //ocultar radios duplicados
        Ocultar(formulario, "rdb_Tarjeta");
        Ocultar(formulario, "rdb_Credito");
        Ocultar(formulario, "rdb_Efectivo");
        var panelCabecera = Buscar(formulario, "guna2Panel1");
        var panelDatos = Buscar(formulario, "guna2ShadowPanel1");
        var panelPago = Buscar(formulario, "guna2ShadowPanel2");
        var panelDetalle = Buscar(formulario, "guna2ShadowPanel3");
        if (panelCabecera != null)
        {
            panelCabecera.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }

        if (panelDatos != null)
        {
            panelDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        }

        if (panelPago != null)
        {
            panelPago.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        }

        if (panelDetalle != null)
        {
            panelDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        var grid = Buscar(formulario, "dgw_ventas") as DataGridView;
        if (grid != null)
        {
            grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        // Los totales ya quedan ubicados en el Designer.
        // En ejecución solo reforzamos texto, anclaje y estilo para no moverlos
        // a una posición distinta de la que se ve al diseñar.
        PrepararEtiquetaTotal(formulario, "lbl_subtotal", "Subtotal:");
        PrepararEtiquetaTotal(formulario, "lbl_descuento", "Descuento:");
        PrepararEtiquetaTotal(formulario, "lbl_total", "Total:");
        PrepararEtiquetaTotal(formulario, "lbl_cambio", "Cambio:");
        foreach (string nombre in new[]
        {
            "lbl_subtotalF",
            "lbl_descuentoF",
            "lbl_totalF",
            "lbl_cambioF"
        }

        )
        {
            if (Buscar(formulario, nombre)is Label valor)
            {
                valor.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                valor.Font = new Font(valor.Font.FontFamily, 10F, FontStyle.Bold, valor.Font.Unit);
            }
        }

        //ordenar métodos de pago
        foreach (string nombre in new[]
        {
            "rdbtn_tarjeta",
            "rdbtn_credito",
            "rdbtn_efectivo"
        }

        )
        {
            if (Buscar(formulario, nombre)is RadioButton radio)
            {
                radio.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }
    }

    private static void PrepararEtiquetaTotal(Form formulario, string nombre, string texto)
    {
        if (Buscar(formulario, nombre)is not Label label)
        {
            return;
        }

        label.Text = texto;
        label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        label.Font = new Font(label.Font.FontFamily, 9.5F, FontStyle.Bold, label.Font.Unit);
        label.ForeColor = SystemColors.ControlText;
    }

    private static void Ocultar(Form formulario, string nombre)
    {
        var control = Buscar(formulario, nombre);
        if (control != null)
        {
            control.Visible = false;
            control.Enabled = false;
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        }
    }

    internal static Control? Buscar(Control raiz, string nombre)
    {
        if (raiz.Name == nombre)
        {
            return raiz;
        }

        foreach (Control hijo in raiz.Controls)
        {
            var encontrado = Buscar(hijo, nombre);
            if (encontrado != null)
            {
                return encontrado;
            }
        }

        return null;
    }

    private static IEnumerable<T> BuscarControles<T>(Control raiz)
        where T : Control
    {
        foreach (Control hijo in raiz.Controls)
        {
            if (hijo is T tipo)
            {
                yield return tipo;
            }

            foreach (var nieto in BuscarControles<T>(hijo))
            {
                yield return nieto;
            }
        }
    }
}
