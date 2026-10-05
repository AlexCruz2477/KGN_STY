namespace Nk_Colletion_New.Presentacion.Productos
{
    internal static class NavegacionPanel
    {

        public static void Abrir(Form actual, Form siguiente)
        {
            var contenedor = actual.Parent;
if (contenedor == null)
            {
                siguiente.Show();
                return;
            }

            actual.Hide();
            siguiente.TopLevel = false;
            siguiente.FormBorderStyle = FormBorderStyle.None;
            siguiente.Dock = DockStyle.Fill;
            contenedor.Controls.Add(siguiente);
            contenedor.Tag = siguiente;
            siguiente.BringToFront();
            siguiente.Show();
        }

        public static void Volver(Form actual, Form anterior)
        {
            var contenedor = actual.Parent;
            if (contenedor == null)
            {
                actual.Close();
                anterior.Show();
                return;
            }

            contenedor.Controls.Remove(actual);
            actual.Dispose();
            if (!contenedor.Controls.Contains(anterior))
            {
                contenedor.Controls.Add(anterior);
            }
anterior.Dock = DockStyle.Fill;
            anterior.Show();
            anterior.BringToFront();
            contenedor.Tag = anterior;
        }
    }
}
