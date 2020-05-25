using System;
using NUnit.Framework;
using Listas;

namespace Tests
{
    [TestFixture]
    public class TestsLista
    {
        Lista listaVacia, listaNoVacia, lista1Elem, listaConRepeticion;
        int elem;

        [SetUp]
        public void InitVariables()
        {
            listaVacia = new Lista();
            listaNoVacia = new Lista(3, 1); //1_2_3_
            lista1Elem = new Lista(1, 1); //1_
            listaConRepeticion = new Lista(4, 2); //1_2_3_4_1_2_3_4_
            elem = -1;
        }

        #region Tests_CuentaElementos()
        //comprueba que CuentaElementos() funciona para una lista vacía
        [Test]
        public void CuentaElementosListaVacia() 
        {
            //Arrange en SetUp
            //Act
            int elementos = listaVacia.CuentaElementos();
            //Assert
            Assert.That(elementos, Is.EqualTo(0), "Error: No se ha creado bien la lista, debería tener 0 elementos");
        }

        //comprueba que CuentaElementos() funciona para una lista no vacía
        [Test]
        public void CuentaElementosListaNoVacia() 
        {
            //Arrange en SetUp
            //Act
            int elementos = listaNoVacia.CuentaElementos();
            //Assert
            Assert.That(elementos, Is.EqualTo(3), "Error: No se ha creado bien la lista, debería tener 3 elementos");
        }
        #endregion

        #region Tests_Inserta()
        //comprueba que Inserta() funciona para una lista vacía
        [Test]
        public void InsertaListaVacia()
        {
            //Arrange en SetUp
            //Act
            listaVacia.Inserta(1);
            int elementos = listaVacia.nElems;
            string lista = listaVacia.VerLista();
            //Assert
            Assert.That(elementos, Is.EqualTo(1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(lista, Is.EqualTo("1_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }

        //comprueba que Inserta() funciona para una lista no vacía
        [Test]
        public void InsertaListaNoVacia() 
        {
            //Arrange de 'listaNoVacia' en SetUp
            int longitudPrevia = listaNoVacia.nElems;
            string listaPrevia = listaNoVacia.VerLista();
            //Act
            listaNoVacia.Inserta(6);
            int elementos = listaNoVacia.nElems;
            string lista = listaNoVacia.VerLista();
            //Assert
            Assert.That(elementos, Is.EqualTo(longitudPrevia + 1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(lista, Is.EqualTo(listaPrevia + "6_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }
        #endregion

        #region Tests_BorraElemento()
        //comprueba que BorraElemento() funciona para una lista vacía
        [Test]
        public void BorraElementoListaVacia() 
        {
            //Arrange en SetUp
            //Act
            bool borraElemento = listaVacia.BorraElemento(8);
            int elementos = listaVacia.nElems;
            //Assert
            Assert.That(borraElemento, Is.False, "Error: Se ha devuelto 'true' en una lista vacía, cuando debería devolver 'false'");
            Assert.That(elementos, Is.EqualTo(0), "Error: Se ha reducido elemento sin haber borrado ninguno");
        }

        //comprueba que BorraElemento() funciona para una lista de 1 elemento cuando se borra el elemento en cuestion
        [Test]
        public void BorraElementoLista1ElementoEsta()
        {
            //Arrange en SetUp
            //Act
            bool borraElemento = lista1Elem.BorraElemento(1);
            int elementos = lista1Elem.nElems;
            string lista = lista1Elem.VerLista();
            //Assert
            Assert.That(borraElemento, Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(elementos, Is.EqualTo(0), "Error: No debería haber ningun elemento en la lista");
            Assert.That(lista, Is.EqualTo(""), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }

        //comprueba que BorraElemento() funciona para una lista de 1 elemento cuando se intenta borra un elemento que no está
        [Test]
        public void BorraElementoLista1ElementoNoEsta()
        {
            //Arrange  de 'lista1Elem' en SetUp
            int nElemsPrevio = lista1Elem.nElems;
            string listaPrevia = lista1Elem.VerLista();
            //Act
            bool borraElemento = lista1Elem.BorraElemento(8);
            int elementos = lista1Elem.nElems;
            string lista = lista1Elem.VerLista();
            //Assert
            Assert.That(borraElemento, Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio), "Error: Debería mantenerse el numero de elementos");
            Assert.That(lista, Is.EqualTo(listaPrevia), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        //comprueba que BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el primer elemento
        [Test]
        public void BorraElementoPrimeroListaNoVacia()
        {
            //Arrange  de 'listaNoVacia' en SetUp
            int nElemsPrevio = listaNoVacia.nElems;
            //Act
            bool borraElemento = listaNoVacia.BorraElemento(1);
            int elementos = listaNoVacia.nElems;
            string lista = listaNoVacia.VerLista();
            //Assert
            Assert.That(borraElemento, Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(lista, Is.EqualTo("2_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        //comprueba que BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el ultimo elemento
        [Test]
        public void BorraElementoUltimoListaNoVacia()
        {
            //Arrange  de 'listaNoVacia' en SetUp
            int nElemsPrevio = listaNoVacia.nElems;
            //Act
            bool borraElemento = listaNoVacia.BorraElemento(3);
            int elementos = listaNoVacia.nElems;
            string lista = listaNoVacia.VerLista();
            //Assert
            Assert.That(borraElemento, Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(lista, Is.EqualTo("1_2_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        //comprueba que BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento medio
        [Test]
        public void BorraElementoMedioListaNoVacia()
        {
            //Arrange  de 'listaNoVacia' en SetUp
            int nElemsPrevio = listaNoVacia.nElems;
            //Act
            bool borraElemento = listaNoVacia.BorraElemento(2);
            int elementos = listaNoVacia.nElems;
            string lista = listaNoVacia.VerLista();
            //Assert
            Assert.That(borraElemento, Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(lista, Is.EqualTo("1_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        //comprueba que BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento que no existe
        [Test]
        public void BorraElementoNoExisteListaNoVacia()
        {
            //Arrange  de 'listaNoVacia' en SetUp
            int nElemsPrevio = listaNoVacia.nElems;
            string listaPrevia = listaNoVacia.VerLista();
            //Act
            bool borraElemento = listaNoVacia.BorraElemento(8);
            int elementos = listaNoVacia.nElems;
            string lista = listaNoVacia.VerLista();
            //Assert
            Assert.That(borraElemento, Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio), "Error: Debería mantenerse el numero de elementos");
            Assert.That(lista, Is.EqualTo(listaPrevia), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        //comprueba que BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento medio repetido posteriormente
        [Test]
        public void BorraElementoListaNoVaciaConRepeticion()
        {
            //Arrange  de 'listaConRepeticion' en SetUp
            int nElemsPrevio = listaConRepeticion.nElems;
            //Act
            bool borraElemento = listaConRepeticion.BorraElemento(3);
            int elementos = listaConRepeticion.nElems;
            string lista = listaConRepeticion.VerLista();
            //Assert
            Assert.That(borraElemento, Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(elementos, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(lista, Is.EqualTo("1_2_4_1_2_3_4_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        #endregion

        #region Tests_N_Esimo()
        //comprueba que N_Esimo() funciona correctamente para una lista vacía
        [Test]
        public void N_EsimoListaVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaVacia.N_Esimo(7); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        //comprueba que N_Esimo() funciona correctamente para una lista no vacía al no existir la posicion por la izquierda
        [Test]
        public void N_EsimoNoExistePosicionExtremoIzquierdoListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaNoVacia.N_Esimo(0); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        //comprueba que N_Esimo() funciona correctamente para una lista no vacía al no existir la posicion por la derecha
        [Test]
        public void N_EsimoNoExistePosicionExtremoDerechoListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaNoVacia.N_Esimo(listaNoVacia.nElems + 1); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        //comprueba que N_Esimo() funciona correctamente para una lista no vacía al existir la posicion
        [Test]
        public void N_EsimoExistePosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidad, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(2), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        //metodo auxiliar de ejecucion del aserto en N_EsimoExistePosicionListaNoVacia()
        private void EjecucionN_EsimoTestsDeUnidad() 
        {
            elem = listaNoVacia.N_Esimo(2);
        }

        //comprueba que N_Esimo() funciona correctamente para una lista no vacía en su extremo izquierdo
        [Test]
        public void N_EsimoExistePrimeraPosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidadPrimeraPosicion, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(1), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        //metodo auxiliar de ejecucion del aserto en N_EsimoExistePrimeraPosicionListaNoVacia()
        private void EjecucionN_EsimoTestsDeUnidadPrimeraPosicion() 
        {
            elem = listaNoVacia.N_Esimo(1);
        }

        //comprueba que N_Esimo() funciona correctamente para una lista no vacía en su extremo derecho
        [Test]
        public void N_EsimoExisteUltimaPosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidadUltimaPosicion, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(3), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        //metodo auxiliar de ejecucion del aserto en N_EsimoExisteUltimaPosicionListaNoVacia()
        private void EjecucionN_EsimoTestsDeUnidadUltimaPosicion() 
        {
            elem = listaNoVacia.N_Esimo(listaNoVacia.nElems);
        }
        #endregion
    }
}