using System;
using NUnit.Framework;
using Listas;

namespace Tests
{
    [TestFixture]
    public class TestsLista
    {
        [SetUp]
        public void InitVariables()
        {

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
            //Act
            int longitudPrevia = l.nElems;
            string listaPrevia = l.VerLista();
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
        public void BorraElementoLista1ElementoEsta() //comprueba que l.CuentaElementos() funciona para una lista no vacía
        {
            //Arrange
            Lista l = new Lista(1, 1);
            //Act-Assert
            Assert.That(l.BorraElemento(1), Is.True, "Error: Debería devolver 'true' porque el elemento existe y se ha eliminado");
            Assert.That(l.nElems, Is.EqualTo(0), "Error: No debería haber ningun elemento en la lista");
            Assert.That(l.VerLista(), Is.EqualTo(""), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }

        [Test]
        public void BorraElementoLista1ElementoNoEsta() //comprueba que l.CuentaElementos() funciona para una lista no vacía
        {
            //Arrange
            Lista l = new Lista(1, 1);
            //Act-Assert
            Assert.That(l.BorraElemento(5), Is.False, "Error: Debería devolver 'false' porque el elemento no existe y no se ha eliminado");
            //Assert.That(l.nElems, Is.EqualTo(0), "Error: No debería haber ningun elemento en la lista");
            //Assert.That(l.VerLista(), Is.EqualTo(""), "Error: Hay algun elemento en la lista, y deberia estar vacía");
        }
        #endregion
    }
}