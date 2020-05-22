using System;
using NUnit.Framework;
using Listas;
using Adventure;

namespace Tests
{
    [TestFixture]
    public class TestsMap
    {
        Map map;

        [SetUp]
        public void CreateMap()
        {
            map = new Map(100, 100);
            map.CreateMap(9, 5, new int[] { 3, 2, 3, 1, 4, 8}, 1, 8);
        }

        #region Tests_FindItemByName()
        //comprueba que FindItemByName() funciona para un elemento medio
        [Test]
        public void FindItemByNameNombreMedio()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 2"), Is.EqualTo(2), "Error: el método no devuelve el indice del item para un elemento medio");
        }

        //comprueba que FindItemByName() funciona para el primer elemento
        [Test]
        public void FindItemByNamePrimerNombre()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 0"), Is.EqualTo(0), "Error: el método no devuelve el indice del item para el primer elemento");
        }

        //comprueba que FindItemByName() funciona para el ultimo elemento
        [Test]
        public void FindItemByNameUltimoNombre()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 4"), Is.EqualTo(4), "Error: el método no devuelve el indice del item para el ultimo elemento");
        }
        
        //comprueba que FindItemByName() funciona cuando se introduce un elemento inexistente
        [Test]
        public void FindItemByNameNoExiste()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 5"), Is.EqualTo(-1), "Error: el método devuelve un indice pese a que no existe el item");
        }
        #endregion
    }
}
