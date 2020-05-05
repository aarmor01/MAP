using System;

namespace Listas
{
    //clase publica para TESTS DE UNIDAD
    public class Lista
    {
        //clase privada
        class Nodo
        {
            public int dato;
            public Nodo sig; //enlace al siguiente nodo
            //constructoras
            public Nodo(int e) { dato = e; sig = null; }
            public Nodo(int e, Nodo n) { dato = e; sig = n; }
        }
        //atributos de la lista enlazada: referencia al primero y al ultimo
        Nodo pri, ult;
        public int nElems; //publico para TESTS DE UNIDAD

        public Lista() //constructora de la clase
        {
            pri = ult = null; //iniciamos comienzo y final
            nElems = 0; //iniciamos numero de elementos
        }

        #region ContructorTestsUnidad
        public Lista (int limite, int repeticiones) //constructora lista no vacía (TESTS DE UNIDAD)
        {
            pri = ult = null; //iniciamos comienzo y final
            nElems = 0; //iniciamos numero de elementos
            for(int i = 0; i < repeticiones; i++) //para cada repeticion
            {
                for(int j = 1; j <= limite; j++) //añadimos los valores
                {
                    //si es el primero, iniciamos "pri" y "ult" a este
                    if (pri == null) pri = ult = new Nodo(j);
                    else //en caso contrario
                    {
                        //asignamos un nuevo nodo al final, y avanzamos "ult" un nodo
                        ult.sig = new Nodo(j);
                        ult = ult.sig;
                    }
                    nElems++;
                }
            }
        }
        #endregion

        public void Inserta(int e) //metodo para insertar elementos al final de la lista
        {
            if (ult == null) pri = ult = new Nodo(e); //si esta vacía, la inicamos con ese elemento
            else //en caso contrario
            {
                //reasignamos el último elemento a este
                ult.sig = new Nodo(e);
                ult = ult.sig;
            }
            nElems++; //aumentamos el numero de elementos
        }

        public bool BuscaDato(int e) //metodo para buscar un dato en la lista
        {
            Nodo aux = BuscaNodo(e); //busca el nodo 
            return (aux != null); //devolvemos si existe o no
        }

        private Nodo BuscaNodo(int e) //metodo de busqueda de un nodo con un dato en especifico
        {
            Nodo aux = pri; //referencia al primero
            while (aux != null && aux.dato != e) //búsqueda de nodo con el elemento 'e'
            {
                aux = aux.sig;
            }

            //termina con aux==null (elto no encontrado) o bien con aux apuntando al primer nodo con elemento e
            return aux;
        }

        public int CuentaElementos() //metodo que cuenta el numero de elementos en la lista
        {
            return nElems; //devolvemos el numero de elementos
        }

        public int N_Esimo(int n) //metodo que devuelve el n-esimo nodo
        {
            Nodo aux = N_EsimoNodo(n); //buscamos el elemento

            if (aux == null) //en caso de no estar, lanzamos la excepcion
            {
                throw new Exception("The element doesn't exists.");
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
                nElems--; //descontamos el numero de elementos
                if (pri == null) ult = null; //si es una lista de 1, y lo quitamos, actualizamos ult
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

                if (aux.sig == ult) ult = aux; //si el elemento es el ultimo, actualizamos su referencia
                //en caso de que esté, hacemos que el siguiente al dato
                //pase a ser el siguiente del anterior al dato
                aux.sig = aux.sig.sig;
                nElems--; //descontamos el numero de elementos
                //devolvemos true
                return true;
            }
        }

        #region MetodosTestsUnidad
        public string VerLista() //metodo que devuelve la lista en string para los tests de unidad
        {
            string lista = "";
            Nodo aux = pri;
            while (aux != null)
            {
                lista += aux.dato + "_";
                aux = aux.sig;
            }
            return lista;
        }
        #endregion
    }
}