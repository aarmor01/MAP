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

        #region TestsCuentaElementos()
        [Test]
        public void NumElemListaVacia() //comprueba que l.CuentaElementos() funciona para una lista vacía
        {
            //Arrange
            Lista l = new Lista();
            //Act-Assert
            Assert.That(l.CuentaElementos(), Is.EqualTo(0), "Error: No se ha creado bien la lista");
        }

        [Test]
        public void NumElemListaNoVacia() //comprueba que l.CuentaElementos() funciona para una lista no vacía
        {
            //Arrange
            Lista l = new Lista(4, 2);
            //Act-Assert
            Assert.That(l.CuentaElementos(), Is.EqualTo(8), "Error: No se ha creado bien la lista");
        }
        #endregion
    }
}