using System;
using NUnit.Framework;
using Adventure;

namespace Tests
{
    [TestFixture]
    public class TestsMap
    {
        Map map;
        int move;

        [SetUp]
        public void CreateMap()
        {
            map = new Map(100, 100);
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 0 }, 5, 0);
            //entero utilizado para pruebas Move()
            move = -2;
        }

        #region Tests_FindItemByName()
        //comprueba que FindItemByName() funciona para un elemento medio
        [Test]
        public void FindItemByNameElementoMedio()
        {
            //Arrange en SetUp
            //Act
            int item = map.FindItemByName("Item 2");
            //Assert
            Assert.That(item, Is.EqualTo(2), "Error: el método no devuelve el indice del item para un elemento medio");
        }

        //comprueba que FindItemByName() funciona para el primer elemento
        [Test]
        public void FindItemByNamePrimerElemento()
        {
            //Arrange en SetUp
            //Act
            int item = map.FindItemByName("Item 0");
            //Assert
            Assert.That(item, Is.EqualTo(0), "Error: el método no devuelve el indice del item para el primer elemento");
        }

        //comprueba que FindItemByName() funciona para el ultimo elemento
        [Test]
        public void FindItemByNameUltimoElemento()
        {
            //Arrange en SetUp
            //Act
            int item = map.FindItemByName("Item 4");
            //Assert
            Assert.That(item, Is.EqualTo(4), "Error: el método no devuelve el indice del item para el ultimo elemento");
        }

        //comprueba que FindItemByName() funciona cuando se introduce un elemento que no existe
        [Test]
        public void FindItemByNameNoExiste()
        {
            //Arrange en SetUp
            //Act
            int item = map.FindItemByName("Item 9");
            //Assert
            Assert.That(item, Is.EqualTo(-1), "Error: el método devuelve un indice pese a que no existe el item");
        }
        #endregion

        #region Tests_PickItemInRoom()
        //comprueba que PickItemInRoom() funciona cuando no existe el elemento en la sala porque no hay elementos
        [Test]
        public void PickItemInRoomSalaSinItems()
        {
            //Arrange de 'map' en SetUp
            int nElemsPrevio = map.GetNumeroElementosSala(5);
            //Act
            bool pickItem = map.PickItemInRoom(5, 3);
            int elementosSala = map.GetNumeroElementosSala(5);
            string listaElementos = map.GetListaElementosSala(5);
            //Assert
            Assert.That(pickItem, Is.False, "Error: el metodo devuelve 'true' pese a no haber items en la sala");
            Assert.That(elementosSala, Is.EqualTo(nElemsPrevio), "Error: se ha reducido el nº de elementos en la sala pese a no haber items");
            Assert.That(listaElementos, Is.EqualTo(""), "Error: se ha eliminado un elemento de la sala pese a no haber items");
        }

        //comprueba que PickItemInRoom() funciona cuando no existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsNoEsta()
        {
            //Arrange de 'map' en SetUp
            int nElemsPrevio = map.GetNumeroElementosSala(3);
            string listaElementosPrevia = map.GetListaElementosSala(3);
            //Act
            bool pickItem = map.PickItemInRoom(3, 1);
            int elementosSala = map.GetNumeroElementosSala(3);
            string listaElementos = map.GetListaElementosSala(3);
            //Assert
            Assert.That(pickItem, Is.False, "Error: el metodo devuelve 'true' pese a no existir el item en la sala");
            Assert.That(elementosSala, Is.EqualTo(nElemsPrevio), "Error: se ha reducido el nº de elementos en la sala pese a no existir el item");
            Assert.That(listaElementos, Is.EqualTo(listaElementosPrevia), "Error: se ha eliminado un elemento en la sala pese a no existir el item");
        }

        //comprueba que PickItemInRoom() funciona cuando existe el elemento en una sala con items
        [Test]
        public void PickItemInRoomSalaConItemsEsta()
        {
            //Arrange de 'map' en SetUp
            int nElemsPrevio = map.GetNumeroElementosSala(3);
            //Act
            bool pickItem = map.PickItemInRoom(3, 2);
            int elementosSala = map.GetNumeroElementosSala(3);
            string listaElementos = map.GetListaElementosSala(3);
            //Assert
            Assert.That(pickItem, Is.True, "Error: el metodo devuelve 'false' pese a haber item en la sala");
            Assert.That(elementosSala, Is.EqualTo(nElemsPrevio - 1), "Error: no se ha reducido el nº de elementos en la sala");
            Assert.That(listaElementos, Is.EqualTo("0_"), "Error: no se ha eliminado un elemento de la sala pese a existir el item");
        }
        #endregion

        #region Tests_Move()
        //comprueba que Move() funciona correctamente cuando hay conexion al norte de la sala (devuelve el índice de la nueva sala)
        [Test]
        public void MoveNorteExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(0, Direction.North); //sala 0 conecta al norte con sala 1
            //Assert
            Assert.That(move, Is.EqualTo(1), "Error: la habitación no tiene conexion al norte pese a que debería");
        }

        //comprueba que Move() funciona correctamente cuando hay conexion al sur de la sala (devuelve el índice de la nueva sala)
        [Test]
        public void MoveSurExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(3, Direction.South); //sala 3 conecta al sur con sala 4
            //Assert
            Assert.That(move, Is.EqualTo(4), "Error: la habitación no tiene conexion al sur pese a que debería");
        }

        //comprueba que Move() funciona correctamente cuando hay conexion al este de la sala (devuelve el índice de la nueva sala)
        [Test]
        public void MoveEsteExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(1, Direction.East); //sala 1 conecta al este con sala 4
            //Assert
            Assert.That(move, Is.EqualTo(4), "Error: la habitación no tiene conexion al este pese a que debería");
        }

        //comprueba que Move() funciona correctamente cuando hay conexion al oeste de la sala (devuelve el índice de la nueva sala)
        [Test]
        public void MoveOesteExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(3, Direction.West); //sala 3 conecta al oeste con sala 2
            //Assert
            Assert.That(move, Is.EqualTo(2), "Error: la habitación no tiene conexion al oeste pese a que debería");
        }

        //comprueba que Move() funciona correctamente cuando no hay conexion al norte de la sala (devuelve -1)
        [Test]
        public void MoveNorteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(2, Direction.North);
            //Assert
            Assert.That(move, Is.EqualTo(-1), "Error: la habitación tiene conexion al norte pese a que no debería");
        }

        //comprueba que Move() funciona correctamente cuando no hay conexion al sur de la sala (devuelve -1)
        [Test]
        public void MoveSurNoExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(0, Direction.South);
            //Assert
            Assert.That(move, Is.EqualTo(-1), "Error: la habitación tiene conexion al sur pese a que no debería");
        }

        //comprueba que Move() funciona correctamente cuando no hay conexion al este de la sala (devuelve -1)
        [Test]
        public void MoveEsteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(3, Direction.East);
            //Assert
            Assert.That(move, Is.EqualTo(-1), "Error: la habitación tiene conexion al este pese a que no debería");
        }

        //comprueba que Move() funciona correctamente cuando no hay conexion al oeste de la sala (devuelve -1)
        [Test]
        public void MoveOesteNoExisteConexion()
        {
            //Arrange en SetUp
            //Act
            move = map.Move(1, Direction.West);
            //Assert
            Assert.That(move, Is.EqualTo(-1), "Error: la habitación tiene conexion al oeste pese a que no debería");
        }
        #endregion
    }
}
