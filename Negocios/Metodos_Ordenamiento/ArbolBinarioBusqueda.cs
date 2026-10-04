using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nk_Colletion_New.Negocios.Metodos_Ordenamiento
{
    public sealed class ArbolBinarioBusqueda<T>
    {
        private readonly Func<T, string> _selectorClave;
        private readonly StringComparer _comparador = StringComparer.CurrentCultureIgnoreCase;
        private NodoArbolBinario<T>? _raiz;

        private sealed record GrupoClave(string Clave, List<T> Valores);

        public ArbolBinarioBusqueda(Func<T, string> selectorClave)
        {
            _selectorClave = selectorClave ?? throw new ArgumentNullException(nameof(selectorClave));
        }

        public int Cantidad { get; private set; }

        public void Limpiar()
        {
            _raiz = null;
            Cantidad = 0;
        }

        public void Reconstruir(IEnumerable<T> elementos)
        {
            Limpiar();
            var grupos = elementos.GroupBy(x => (_selectorClave(x) ?? string.Empty).Trim(), _comparador)
                .OrderBy(g => g.Key, _comparador)
                .Select(g => new GrupoClave(g.Key, g.ToList()))
                .ToList();
            Cantidad = grupos.Sum(g => g.Valores.Count);
            _raiz = ConstruirBalanceado(grupos, 0, grupos.Count - 1);
        }

        private static NodoArbolBinario<T>? ConstruirBalanceado(IReadOnlyList<GrupoClave> grupos, int inicio, int fin)
        {
            if (inicio > fin)
            {
                return null;
            }

            int medio = inicio + ((fin - inicio) / 2);
            GrupoClave grupo = grupos[medio];
            var nodo = new NodoArbolBinario<T>(grupo.Clave, grupo.Valores[0]);
            for (int i = 1; i < grupo.Valores.Count; i++)
            {
                nodo.Valores.Add(grupo.Valores[i]);
            }

            nodo.Izquierdo = ConstruirBalanceado(grupos, inicio, medio - 1);
            nodo.Derecho = ConstruirBalanceado(grupos, medio + 1, fin);
            return nodo;
        }

        public void Agregar(T elemento)
        {
            string clave = (_selectorClave(elemento) ?? string.Empty).Trim();
            if (_raiz is null)
            {
                _raiz = new NodoArbolBinario<T>(clave, elemento);
                Cantidad++;
                return;
            }

            NodoArbolBinario<T> actual = _raiz;
            while (true)
            {
                int comparacion = _comparador.Compare(clave, actual.Clave);
                if (comparacion == 0)
                {
                    actual.Valores.Add(elemento);
                    Cantidad++;
                    return;
                }

                if (comparacion < 0)
                {
                    if (actual.Izquierdo is null)
                    {
                        actual.Izquierdo = new NodoArbolBinario<T>(clave, elemento);
                        Cantidad++;
                        return;
                    }

                    actual = actual.Izquierdo;
                }
                else
                {
                    if (actual.Derecho is null)
                    {
                        actual.Derecho = new NodoArbolBinario<T>(clave, elemento);
                        Cantidad++;
                        return;
                    }

                    actual = actual.Derecho;
                }
            }
        }

        public List<T> Recorrer(bool ascendente = true)
        {
            var resultado = new List<T>(Cantidad);
            RecorrerNodo(_raiz, resultado, ascendente, null);
            return resultado;
        }

        public List<T> BuscarYFiltrar(
            string? texto,
            Func<T, IEnumerable<string?>> camposBusqueda,
            Func<T, bool>? filtro = null,
            bool ascendente = true
        )
        {
            string termino = Normalizar(texto);
            bool Coincide(T elemento)
            {
                if (filtro is not null && !filtro(elemento))
                {
                    return false;
                }

                if (string.IsNullOrEmpty(termino))
                {
                    return true;
                }

                foreach (string? campo in camposBusqueda(elemento))
                {
                    if (Normalizar(campo).Contains(termino, StringComparison.Ordinal))
                    {
                        return true;
                    }
                }

                return false;
            }

            var resultado = new List<T>();
            RecorrerNodo(_raiz, resultado, ascendente, Coincide);
            return resultado;
        }

        public IReadOnlyList<T> BuscarExacto(string clave)
        {
            NodoArbolBinario<T>? actual = _raiz;
            clave = (clave ?? string.Empty).Trim();
            while (actual is not null)
            {
                int comparacion = _comparador.Compare(clave, actual.Clave);
                if (comparacion == 0)
                {
                    return actual.Valores.ToList();
                }

                actual = comparacion < 0 ? actual.Izquierdo : actual.Derecho;
            }

            return Array.Empty<T>();
        }

        private static void RecorrerNodo(NodoArbolBinario<T>? nodo, List<T> resultado, bool ascendente, Func<T, bool>? filtro)
        {
            if (nodo is null)
            {
                return;
            }

            NodoArbolBinario<T>? primero = ascendente ? nodo.Izquierdo : nodo.Derecho;
            NodoArbolBinario<T>? segundo = ascendente ? nodo.Derecho : nodo.Izquierdo;
            RecorrerNodo(primero, resultado, ascendente, filtro);
            foreach (var valor in nodo.Valores)
            {
                if (filtro is null || filtro(valor))
                {
                    resultado.Add(valor);
                }
            }

            RecorrerNodo(segundo, resultado, ascendente, filtro);
        }

        private static string Normalizar(string? texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return string.Empty;
            }

            string descompuesto = texto.Trim().ToUpperInvariant().Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder(descompuesto.Length);
            foreach (char caracter in descompuesto)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(caracter) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(caracter);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}