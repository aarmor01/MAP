using System;
using NUnit.Framework;
using Listas;

namespace Tests
{
    [TestFixture]
    public class TestsLista
    {
        Lista listaVacia, listaNoVacia, listaConRepeticion;

        [SetUp]
        public void InitVariables()
        {
            listaVacia = new Lista();
            listaNoVacia = new Lista(3, 1);
            listaConRepeticion = new Lista(4, 2);
        }

        #region Tests_CuentaElementos()
        [Test]
        public void CuentaElementosListaVacia() //comprueba que l.CuentaElementos() funciona para una lista vacía
        {
            //Arrange
            Lista l = new Lista();
            //Act-Assert
            Assert.That(l.CuentaElementos(), Is.EqualTo(0), "Error: No se ha creado bien la lista, debería tener 0 elementos");
        }

        [Test]
        public void CuentaElementosListaNoVacia() //comprueba que l.CuentaElementos() funciona para una lista no vacía
        {
            //Arrange
            Lista l = new Lista(4, 2);
            //Act-Assert
            Assert.That(l.CuentaElementos(), Is.EqualTo(8), "Error: No se ha creado bien la lista, debería tener 8 elementos");
        }
        #endregion

        #region Tests_Inserta()
        [Test]
        public void InsertaListaVacia() //comprueba que l.Inserta() funciona para una lista vacía
        {
            //Arrange
            Lista l = new Lista();
            //Act
            l.Inserta(1);
            //Assert
            Assert.That(l.nElems, Is.EqualTo(1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(l.VerLista(), Is.EqualTo("1_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }

        [Test]
        public void InsertaListaNoVacia() //comprueba que l.Inserta() funciona para una lista no vacía
        {
            //Arrange
            Lista l = new Lista(3, 2);
            int longitudPrevia = l.nElems;
            string listaPrevia = l.VerLista();
            //Act
            l.Inserta(6);
            //Assert
            Assert.That(l.nElems, Is.EqualTo(longitudPrevia + 1), "Error: No se ha añadido el elemento a la lista (no ha aumentado el contador)");
            Assert.That(l.VerLista(), Is.EqualTo(listaPrevia + "6_"), "Error: No se ha añadido correctamente el elemento a la lista (no está en la posición final)");
        }
        #endregion

        #region Tests_BorraElemento()
        [Test]
        public void BorraElementoListaVacia() //comprueba que l.BorraElemento() funciona para una lista vacía
        {
            //Arrange
            Lista l = new Lista();
            //Act-Assert
            Assert.That(l.BorraElemento(8), Is.False, "Error: Se ha devuelto 'true' en una lista vacía, cuando debería devolver 'false'");
            Assert.That(l.nElems, Is.EqualTo(0), "Error: Se ha reducido elemento sin haber borrado ninguno");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de 1 elemento cuando se borra el elemento en cuestion
        public void BorraElementoLista1ElementoEsta()
        {
            //Arrange
            Lista l = new Lista(1, 1);
            //Act-Assert
            Assert.That(l.BorraElemento(1), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(0), "Error: No debería haber ningun elemento en la lista");
            Assert.That(l.VerLista(), Is.EqualTo(""), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de 1 elemento cuando se intenta borra un elemento que no está
        public void BorraElementoLista1ElementoNoEsta()
        {
            //Arrange
            Lista l = new Lista(1, 1);
            int nElemsPrevio = l.nElems;
            string listaPrevia = l.VerLista();
            //Act-Assert
            Assert.That(l.BorraElemento(5), Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(nElemsPrevio), "Error: No debería haber ningun elemento en la lista");
            Assert.That(l.VerLista(), Is.EqualTo(listaPrevia), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }

        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el primer elemento
        public void BorraElementoPrimeroListaNoVacia()
        {
            //Arrange
            Lista l = new Lista(7, 1);
            int nElemsPrevio = l.nElems;
            //Act-Assert
            Assert.That(l.BorraElemento(1), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(l.VerLista(), Is.EqualTo("2_3_4_5_6_7_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        
        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra el ultimo elemento
        public void BorraElementoUltimoListaNoVacia()
        {
            //Arrange
            Lista l = new Lista(7, 1);
            int nElemsPrevio = l.nElems;
            //Act-Assert
            Assert.That(l.BorraElemento(7), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(l.VerLista(), Is.EqualTo("1_2_3_4_5_6_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        
        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento medio
        public void BorraElementoMedioListaNoVacia()
        {
            //Arrange
            Lista l = new Lista(7, 1);
            int nElemsPrevio = l.nElems;
            //Act-Assert
            Assert.That(l.BorraElemento(4), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(l.VerLista(), Is.EqualTo("1_2_3_5_6_7_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        
        [Test]
        //comprueba que l.BorraElemento() funciona para una lista de varios elemento cuando se intenta borra un elemento mediorepetido posteriormente
        public void BorraElementoListaNoVaciaConRepeticion()
        {
            //Arrange
            Lista l = new Lista(3, 2);
            int nElemsPrevio = l.nElems;
            //Act-Assert
            Assert.That(l.BorraElemento(3), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(nElemsPrevio - 1), "Error: Debería reducirse el numero de elementos en uno");
            Assert.That(l.VerLista(), Is.EqualTo("1_2_1_2_3_"), "Error: La lista resultante de la eliminacion difiere con la prevista");
        }
        #endregion

        #region Tests_N_Esimo()


        #endregion
    }
}