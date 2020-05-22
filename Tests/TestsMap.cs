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
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 4}, 0, 5);
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
        
        //comprueba que FindItemByName() funciona cuando se introduce un elemento que no existe
        [Test]
        public void FindItemByNameNoExiste()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 5"), Is.EqualTo(-1), "Error: el método devuelve un indice pese a que no existe el item");
        }
        #endregion

        #region Tests_PickItemInRoom()
        //comprueba que PickItemInRoom() funciona cuando no existe el elemento en la sala porque no hay elementos
        [Test]
        public void PickItemInRoomSalaSinItems()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(5);
            //Act-Assert
            Assert.That(map.PickItemInRoom(5, 3), Is.False, "Error: el metodo devuelve 'true' pese a no haber items en la sala");
            Assert.That(map.GetNumeroElementosSala(5), Is.EqualTo(elementosSala), "Error: se ha reducido el nº de elementos en la sala pese a no haber items");
        }

        //comprueba que PickItemInRoom() funciona cuando no existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsNoEsta()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(3);
            //Act-Assert
            Assert.That(map.PickItemInRoom(3, 1), Is.False, "Error: el metodo devuelve 'true' pese a no existir el item en la sala");
            Assert.That(map.GetNumeroElementosSala(3), Is.EqualTo(elementosSala), "Error: se ha reducido el nº de elementos en la sala pese a no existir el item");
        }

        //FALTA SALA CON 1 ITEM EXISTE???

        //comprueba que PickItemInRoom() funciona cuando existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsEsta()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(3);
            //Act-Assert
            Assert.That(map.PickItemInRoom(3, 2), Is.True, "Error: el metodo devuelve 'false' pese a haber item en la sala");
            Assert.That(map.GetNumeroElementosSala(3), Is.EqualTo(elementosSala - 1), "Error: no se ha reducido el nº de elementos en la sala");
            //comprobar string??
        }
        #endregion
    }
}
