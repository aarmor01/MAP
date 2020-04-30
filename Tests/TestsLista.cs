using System;
using NUnit.Framework;
using Listas;


namespace Tests
{
    [TestFixture]
    public class TestsLista
    {
        [Test]
        public void NumElemListaVacia()
        {
            //Arrange
            Lista l = new Lista();
            //Act-Assert
            Assert.That(l.CuentaElementos(), Is.EqualTo(0), "Error: No se ha creado bien la lista");
        }
    }
}