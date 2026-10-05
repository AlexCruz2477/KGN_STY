using System.Runtime.CompilerServices;

namespace Nk_Colletion_New.Presentacion.Estilos;

internal static class InterfazResponsiva
{
    private static readonly ConditionalWeakTable<Form, object> Preparados = new();
    private static bool _activa;

    public static void Activar()
    {
        if (_activa)
        {
            return;
        }

        _activa = true;
        Application.Idle += (_, _) => AplicarPendientes();
    }

    private static void AplicarPendientes()
    {
        foreach (Form formulario in Application.OpenForms.Cast<Form>().ToArray())
        {
            PrepararFormularioYDescendientes(formulario);
        }
    }

    private static void PrepararFormularioYDescendientes(Control raiz)
    {
        if (raiz is Form formulario &&
            !formulario.IsDisposed &&
            !Preparados.TryGetValue(formulario, out _))
        {
            Preparados.Add(formulario, new object());

            if (formulario is not Main)
            {
                TemaNk.Aplicar(formulario);
                AjustesPantallaNk.AplicarInicial(formulario);
                DisenoResponsivoNk.Habilitar(formulario);
            }
        }

        foreach (Control hijo in raiz.Controls)
        {
            PrepararFormularioYDescendientes(hijo);
        }
    }
}
