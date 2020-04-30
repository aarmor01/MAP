using System;

namespace Listas
{
    public class Lista
    {
        //clase privada
        private class Nodo
        {
            public int dato; //información (puede ser de cualquier tipo)
            public Nodo sig; //siguiente elemento de la lista
        }

        Nodo pri; //primer nodo de la lista

        public Lista() //constructor
        {
            pri = null; //incializamos el puntero a null
        }

        public void Inserta(int e) //metodo para insertar elementos en la lista (al final de esta)
        {
            if (pri == null) //lista vacía
            {
                pri = new Nodo(); //creamos el comienzo de la lista
                //le asignamos el dato y el siguiente nodo
                pri.dato = e;
                pri.sig = null;
            }
            else //lista no vacía
            {
                Nodo aux = pri; //nodo auxiliar que empieza en pri
                while (aux.sig != null) //buscamos el último elemento de la lista
                {
                    aux = aux.sig;
                }
                //creamos un nuevo nodo al final con el dato introducido
                aux.sig = new Nodo();
                aux = aux.sig;
                aux.dato = e;
                aux.sig = null;
            }
        }

        public bool BuscaDato(int e) //metodo para buscar un elemento en la lista
        {
            Nodo aux = BuscaNodo(e); //busca el nodo 
            return (aux != null); //devuelve si existe (!=) o no (==)
        }

        private Nodo BuscaNodo(int e) //metodo auxiliar para buscar el nodo
        {
            Nodo aux = pri; //referencia al primero
            while (aux != null && aux.dato != e) //búsqueda de nodo con el elemento 'e'
            {
                aux = aux.sig;
            }

            //termina con aux==null (elemento no encontrado) o bien con aux apuntando al primer nodo con elemento 'e'
            return aux;
        }

        public int CuentaElementos() //metodo que cuenta el numero de elementos en la lista
        {
            Nodo aux = pri; //Nodo auxiliar que empieza en pri
            int cont = 0;
            while (aux != null) //para cada elemento que exista
            {
                cont++; //aumentamos el contador
                aux = aux.sig;
            }

            //devolvemos el numero de elementos
            return cont;
        }

        public int N_Esimo(int n) //metodo que devuelve el n-esimo nodo
        {
            Nodo aux = N_EsimoNodo(n); //buscamos el elemento

            if (aux == null) //en caso de no estar, lanzamos la excepcion
            {
                throw new Exception("No existe ese elemento.");
            }

            //si está, devolvemos el dato
            return aux.dato;
        }

        private Nodo N_EsimoNodo(int n) //metodo de busqueda del n-esimo nodo
        {
            if (n <= 0) //si el valor es menor que 0, devolvemos null automaticamente
            {
                return null;
            }

            //en caso contrario, creamos un nodo auxiliar en pri
            Nodo aux = pri;
            while (aux != null && n > 1) //recorremos el vector hasta llegar al elemento n-esimo (o salirnos de la lista)
            {
                n--;
                aux = aux.sig;
            }

            //devolvemos el elemento
            return aux;
        }

        public bool BorraElemento(int e) //metodo para borrar un elemento de la lista
        {
            if (pri == null) //si no hay elementos en la lista
            {
                return false;
            }
            else if (pri.dato == e) //si el primer elemento tiene el dato
            {
                //movemos pri al siguiente de pri (segundo elemento)
                pri = pri.sig;
                return true;
            }
            else
            {
                Nodo aux = pri;
                //buscamos el elemento previo al dato
                while (aux.sig != null && aux.sig.dato != e)
                {
                    aux = aux.sig;
                }

                if (aux.sig == null) //si no lo encontramos
                {
                    return false;
                }

                //en caso de que esté, hacemos que el siguiente al dato
                //pase a ser el siguiente del anterior al dato
                aux.sig = aux.sig.sig;
                //devolvemos true
                return true;
            }
        }
    }
}