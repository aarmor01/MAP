using System;
using NUnit.Framework;
using Listas;

namespace Tests
{
    [TestFixture]
    public class TestsLista
    {
        Lista listaVacia, listaNoVacia, listaConRepeticion;
        int elem;

        [SetUp]
        public void InitVariables()
        {
            listaVacia = new Lista();
            listaNoVacia = new Lista(3, 1); //1_2_3_
            listaConRepeticion = new Lista(4, 2); //1_2_3_4_1_2_3_4_
            elem = -1;
        }

        #region Tests_CuentaElementos()
        [Test]
        public void CuentaElementosListaVacia() //comprueba que l.CuentaElementos() funciona para una lista vacía
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(listaVacia.CuentaElementos(), Is.EqualTo(0), "Error: No se ha creado bien la lista, debería tener 0 elementos");
        }

        [Test]
        public void CuentaElementosListaNoVacia() //comprueba que l.CuentaElementos() funciona para una lista no vacía
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(listaNoVacia.CuentaElementos(), Is.EqualTo(3), "Error: No se ha creado bien la lista, debería tener 3 elementos");
        }
        #endregion

        #region Tests_Inserta()
        [Test]
        public void InsertaListaVacia() //comprueba que l.Inserta() funciona para una lista vacía
        {
            //Arrange en SetUp
            //Act
            listaVacia.Inserta(1);
            //Assert
            Assert.That(listaVacia.nElems, Is.EqualTo(1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(listaVacia.VerLista(), Is.EqualTo("1_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }

        [Test]
        public void InsertaListaNoVacia() //comprueba que l.Inserta() funciona para una lista no vacía
        {
            //Arrange en SetUp de Lista
            int longitudPrevia = listaNoVacia.nElems;
            string listaPrevia = listaNoVacia.VerLista();
            //Act
            listaNoVacia.Inserta(6);
            //Assert
            Assert.That(listaNoVacia.nElems, Is.EqualTo(longitudPrevia + 1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(listaNoVacia.VerLista(), Is.EqualTo(listaPrevia + "6_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }
        #endregion

        #region Tests_BorraElemento()
        [Test]
        public void BorraElementoListaVacia() //comprueba que l.BorraElemento() funciona para una lista vacía
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(listaVacia.BorraElemento(8), Is.False, "Error: Se ha devuelto 'true' en una lista vacía, cuando debería devolver 'false'");
            Assert.That(listaVacia.nElems, Is.EqualTo(0), "Error: Se ha reducido elemento sin haber borrado ninguno");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de 1 elemento cuando se borra el elemento en cuestion
        public void BorraElementoLista1ElementoEsta()
        {
            //Arrange
            Lista lista1Elem = new Lista(1, 1);
            //Act-Assert
            Assert.That(lista1Elem.BorraElemento(1), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(lista1Elem.nElems, Is.EqualTo(0), "Error: No debería haber ningun elemento en la lista");
            Assert.That(lista1Elem.VerLista(), Is.EqualTo(""), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de 1 elemento cuando se intenta borra un elemento que no está
        public void BorraElementoLista1ElementoNoEsta()
        {
            //Arrange
            Lista lista1Elem = new Lista(1, 1);
            int nElemsPrevio = lista1Elem.nElems;
            string listaPrevia = lista1Elem.VerLista();
            //Act-Assert
            Assert.That(lista1Elem.BorraElemento(5), Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            Assert.That(lista1Elem.nElems, Is.EqualTo(nElemsPrevio), "Error: Debería mantenerse el numero de elementos");
            Assert.That(lista1Elem.VerLista(), Is.EqualTo(listaPrevia), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el primer elemento
        public void BorraElementoPrimeroListaNoVacia()
        {
            //Arrange en SetUp de Lista
            int nElemsPrevio = listaNoVacia.nElems;
            //Act-Assert
            Assert.That(listaNoVacia.BorraElemento(1), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(listaNoVacia.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(listaNoVacia.VerLista(), Is.EqualTo("2_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el ultimo elemento
        public void BorraElementoUltimoListaNoVacia()
        {
            //Arrange en SetUp de Lista
            int nElemsPrevio = listaNoVacia.nElems;
            //Act-Assert
            Assert.That(listaNoVacia.BorraElemento(3), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(listaNoVacia.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(listaNoVacia.VerLista(), Is.EqualTo("1_2_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento medio
        public void BorraElementoMedioListaNoVacia()
        {
            //Arrange en SetUp de Lista
            int nElemsPrevio = listaNoVacia.nElems;
            //Act-Assert
            Assert.That(listaNoVacia.BorraElemento(2), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(listaNoVacia.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(listaNoVacia.VerLista(), Is.EqualTo("1_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento que no existe
        public void BorraElementoNoExisteListaNoVacia()
        {
            //Arrange en SetUp de Lista
            int nElemsPrevio = listaNoVacia.nElems;
            //Act-Assert
            Assert.That(listaNoVacia.BorraElemento(4), Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            Assert.That(listaNoVacia.nElems, Is.EqualTo(nElemsPrevio), "Error: Debería mantenerse el numero de elementos");
            Assert.That(listaNoVacia.VerLista(), Is.EqualTo("1_2_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento medio repetido posteriormente
        public void BorraElementoListaNoVaciaConRepeticion()
        {
            //Arrange en SetUp de Lista
            int nElemsPrevio = listaConRepeticion.nElems;
            //Act-Assert
            Assert.That(listaConRepeticion.BorraElemento(3), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(listaConRepeticion.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(listaConRepeticion.VerLista(), Is.EqualTo("1_2_4_1_2_3_4_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        #endregion

        #region Tests_N_Esimo()
        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista vacía
        public void N_EsimoListaVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaVacia.N_Esimo(7); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista no vacía al no existir la posicion por la izquierda
        public void N_EsimoNoExistePosicionExtremoIzquierdoListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaNoVacia.N_Esimo(0); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista no vacía al no existir la posicion por la derecha
        public void N_EsimoNoExistePosicionExtremoDerechoListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(() => { listaNoVacia.N_Esimo(listaNoVacia.nElems + 1); }, Throws.Exception, "Error: N_Esimo no lanza excepción pese a que no existe la posicion introducida");
        }

        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista no vacía al existir la posicion
        public void N_EsimoExistePosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidad, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(2), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        private void EjecucionN_EsimoTestsDeUnidad() //metodo auxiliar de ejecucion del aserto en N_EsimoExistePosicionListaNoVacia()
        {
            elem = listaNoVacia.N_Esimo(2);
        }

        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista no vacía en su extremo izquierdo
        public void N_EsimoExistePrimeraPosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidadPrimeraPosicion, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(1), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        private void EjecucionN_EsimoTestsDeUnidadPrimeraPosicion() //metodo auxiliar de ejecucion del aserto en N_EsimoExistePrimeraPosicionListaNoVacia()
        {
            elem = listaNoVacia.N_Esimo(1);
        }

        [Test]
        //comprueba que l.N_Esimo() funciona correctamente para una lista no vacía en su extremo derecho
        public void N_EsimoExisteUltimaPosicionListaNoVacia()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(EjecucionN_EsimoTestsDeUnidadUltimaPosicion, Throws.Nothing, "Error: N_Esimo lanza excepción pese a que existe la posicion introducida");
            Assert.That(elem, Is.EqualTo(3), "Error: N_Esimo no devuelve el valor de la posicion introducida");
        }

        private void EjecucionN_EsimoTestsDeUnidadUltimaPosicion() //metodo auxiliar de ejecucion del aserto en N_EsimoExisteUltimaPosicionListaNoVacia()
        {
            elem = listaNoVacia.N_Esimo(listaNoVacia.nElems);
        }
        #endregion
    }
}