using System;
using NUnit.Framework;
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
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 4 }, 5, 0);
        }

        #region Tests_FindItemByName()
        //comprueba que FindItemByName() funciona para un elemento medio
        [Test]
        public void FindItemByNameElementoMedio()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 2"), Is.EqualTo(2), "Error: el método no devuelve el indice del item para un elemento medio");
        }

        //comprueba que FindItemByName() funciona para el primer elemento
        [Test]
        public void FindItemByNamePrimerElemento()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.FindItemByName("Item 0"), Is.EqualTo(0), "Error: el método no devuelve el indice del item para el primer elemento");
        }

        //comprueba que FindItemByName() funciona para el ultimo elemento
        [Test]
        public void FindItemByNameUltimoElemento()
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
            Assert.That(map.GetListaElementosSala(5), Is.EqualTo(""), "Error: se ha eliminado un elemento de la sala pese a no haber items");
        }

        //comprueba que PickItemInRoom() funciona cuando no existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsNoEsta()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(3);
            string listaElementosSala = map.GetListaElementosSala(3);
            //Act-Assert
            Assert.That(map.PickItemInRoom(3, 1), Is.False, "Error: el metodo devuelve 'true' pese a no existir el item en la sala");
            Assert.That(map.GetNumeroElementosSala(3), Is.EqualTo(elementosSala), "Error: se ha reducido el nº de elementos en la sala pese a no existir el item");
            Assert.That(map.GetListaElementosSala(3), Is.EqualTo(listaElementosSala), "Error: se ha eliminado un elemento en la sala pese a no existir el item");
        }

        //PREGUNTAR
        //comprueba que PickItemInRoom() funciona cuando existe el elemento en una sala con solo ese item
        [Test]
        public void PickItemInRoomSalaConUnItemEsta()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(2);
            //Act-Assert
            Assert.That(map.PickItemInRoom(2, 1), Is.True, "Error: el metodo devuelve 'false' pese a haber item en la sala");
            Assert.That(map.GetNumeroElementosSala(2), Is.EqualTo(elementosSala - 1), "Error: no se ha reducido el nº de elementos en la sala");
            Assert.That(map.GetListaElementosSala(2), Is.EqualTo(""), "Error: no se ha eliminado un elemento de la sala pese a existir el item");
        }

        //comprueba que PickItemInRoom() funciona cuando existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsEsta()
        {
            //Arrange de 'map' en SetUp
            int elementosSala = map.GetNumeroElementosSala(3);
            //Act-Assert
            Assert.That(map.PickItemInRoom(3, 2), Is.True, "Error: el metodo devuelve 'false' pese a haber item en la sala");
            Assert.That(map.GetNumeroElementosSala(3), Is.EqualTo(elementosSala - 1), "Error: no se ha reducido el nº de elementos en la sala");
            Assert.That(map.GetListaElementosSala(3), Is.EqualTo("0_"), "Error: no se ha eliminado un elemento de la sala pese a existir el item");
        }
        #endregion

        #region Tests_Move()
        //comprueba que Move() devuelve una conexión a una habitación en la dirección norte
        [Test]
        public void MoveNorteExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(0, Direction.North), Is.EqualTo(1), "Error: la habitación no está conectada al norte en esa dirección cuando si debería");
        }

        //comprueba que Move() devuelve una conexión a una habitación en la dirección sur
        [Test]
        public void MoveSurExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(3, Direction.South), Is.EqualTo(4), "Error: la habitación no está conectada al sur en esa dirección cuando si debería");
        }

        //comprueba que Move() devuelve una conexión a una habitación en la dirección este
        [Test]
        public void MoveEsteExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(1, Direction.East), Is.EqualTo(4), "Error: la habitación no está conectada al este en esa dirección cuando si debería");
        }

        //comprueba que Move() devuelve una conexión a una habitación en la dirección oeste
        [Test]
        public void MoveOesteExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(3, Direction.West), Is.EqualTo(2), "Error: la habitación no está conectada al oeste en esa dirección cuando si debería");
        }

        //comprueba que Move() devuelve una conexión no válida en la dirección norte
        [Test]
        public void MoveNorteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(2, Direction.North), Is.EqualTo(-1), "Error: la habitación está conectada al norte en esa dirección cuando no debería");
        }

        //comprueba que Move() devuelve una conexión no válida en la dirección sur
        [Test]
        public void MoveSurNoExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(0, Direction.South), Is.EqualTo(-1), "Error: la habitación está conectada al sur en esa dirección cuando no debería");
        }

        //comprueba que Move() devuelve una conexión no válida en la dirección este
        [Test]
        public void MoveEsteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(3, Direction.East), Is.EqualTo(-1), "Error: la habitación está conectada al este en esa dirección cuando no debería");
        }

        //comprueba que Move() devuelve una conexión no válida en la dirección oeste
        [Test]
        public void MoveOesteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act-Assert
            Assert.That(map.Move(1, Direction.West), Is.EqualTo(-1), "Error: la habitación está conectada al oeste en esa dirección cuando no debería");
        }
        #endregion
    }
}
