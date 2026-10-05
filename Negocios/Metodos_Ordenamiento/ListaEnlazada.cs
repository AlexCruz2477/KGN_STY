using System.Collections;

namespace Nk_Colletion_New.Negocios.Metodos_Ordenamiento
{
    public sealed class ListaEnlazada<T> : IEnumerable<T>
    {
        private Nodo<T>? _primero;
        private Nodo<T>? _ultimo;

        public int Count { get; private set; }

        public void Agregar(T valor)
        {
            var nodo = new Nodo<T>(valor);
            if (_ultimo is null)
            {
                _primero = _ultimo = nodo;
            }
            else
            {
                _ultimo.Siguiente = nodo;
                _ultimo = nodo;
            }

            Count++;
        }

        public bool Eliminar(Predicate<T> coincide)
        {
            Nodo<T>? anterior = null;
            var actual = _primero;
            while (actual is not null)
            {
                if (coincide(actual.Valor))
                {
                    if (anterior is null)
                        _primero = actual.Siguiente;
                    else
                        anterior.Siguiente = actual.Siguiente;

                    if (actual == _ultimo)
                        _ultimo = anterior;

                    Count--;
                    return true;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            return false;
        }

        public void Limpiar()
        {
            _primero = null;
            _ultimo = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var actual = _primero;
            while (actual is not null)
            {
                yield return actual.Valor;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private sealed class Nodo<TValor>(TValor valor)
        {
            public TValor Valor { get; } = valor;
            public Nodo<TValor>? Siguiente { get; set; }
        }
    }
}
