using Guna.UI2.WinForms;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class EstiloControlesDesigner
{
    private static readonly Color RojoPrincipal = Color.FromArgb(100, 28, 45);

    public static void AplicarBordes(Control contenedor)
    {
        foreach (Control control in contenedor.Controls)
        {
            AplicarBordes(control);
            switch (control)
            {
                case Guna2Button boton:
                    boton.BorderColor = Color.Black;
                    boton.BorderThickness = 1;
                    boton.FillColor = RojoPrincipal;
                    break;
                case Guna2CircleButton boton:
                    boton.BorderColor = Color.Black;
                    boton.BorderThickness = 1;
                    boton.FillColor = RojoPrincipal;
                    break;
                case Guna2TextBox texto:
                    texto.BorderColor = Color.Black;
                    texto.BorderThickness = 1;
                    break;
                case Guna2ComboBox combo:
                    combo.BorderColor = Color.Black;
                    combo.BorderThickness = 1;
                    break;
                case Guna2NumericUpDown numerico:
                    numerico.BorderColor = Color.Black;
                    numerico.BorderThickness = 1;
                    break;
                case Guna2DateTimePicker fecha:
                    fecha.BorderColor = Color.Black;
                    fecha.BorderThickness = 1;
                    break;
                case TextBox texto:
                    texto.BorderStyle = BorderStyle.FixedSingle;
                    break;
            }
        }
    }
}
