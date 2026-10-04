using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Metodos_Ordenamiento
{
    internal sealed class NodoArbolBinario<T>
    {

        public NodoArbolBinario(string clave, T valor)
        {
            Clave = clave;
            Valores.Add(valor);
        }

        public string Clave { get; }
        public List<T> Valores { get; } = new();
        public NodoArbolBinario<T>? Izquierdo { get; set; }
        public NodoArbolBinario<T>? Derecho { get; set; }
    }

}
