using System;
using NUnit.Framework;
using Adventure;

namespace Tests
{
    [TestFixture]
    public class TestsPlayer
    {
        Map map;
        Player player, player2, player3;
        int hp, position, weight;
        string inventory;

        [SetUp]
        public void CreateMapYPlayer()
        {
            map = new Map(100, 100);
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 0 }, 5, 0);
            //player, player2, y player3 comienzan en distintas salas para las pruebas de Move() [conexiones] y PickItem()/EatItem() [items en la sala]
            player = new Player("Gato de Guillermo", map.GetEntryRoom()); //comienza en la sala 0 --> conn = {1, -1, -1, -1}
            player2 = new Player("Gato de Guillermo", 1); //comienza en la sala 1 --> conn = {2, 0, 4, -1}
            player3 = new Player("Gato de Guillermo", 3); //comienza en la sala 3 --> conn = {-1, 4, -1, 2}
            //enteros para usar en los distintos tests
            hp = position = weight = -2;
            //string para usar en los distintos tests
            inventory = ":)";
        }

        #region Test_Player()
        //comprueba que Player() construye al jugador respecto a los parámetros introducidos
        [Test]
        public void ConstructoraPlayerFunciona()
        {
            //Arrange
            int MAX_HP = player.GetMAXHP();
            //Act
            Player newPlayer = new Player("Gato de Guillermo", 2);
            string name = newPlayer.GetName();
            weight = newPlayer.GetPeso();
            hp = newPlayer.GetHP();
            position = newPlayer.GetPosition();
            inventory = newPlayer.GetInventory().VerLista();
            //Assert
            Assert.That(name, Is.EqualTo("Gato de Guillermo"), "Error: nombre del jugador establecido erroneamente");
            Assert.That(hp, Is.EqualTo(MAX_HP), "Error: HP establecido erroneamente");
            Assert.That(weight, Is.EqualTo(0), "Error: peso del inventario establecido erroneamente");
            Assert.That(position, Is.EqualTo(2), "Error: sala inicial establecida erroneamente");
            Assert.That(inventory, Is.EqualTo(""), "Error: inventario creado erroneamente");
        }
        #endregion

        #region Test_Move()
        //comprueba que Move() funciona cuando existe una conexión hacia el norte
        [Test]
        public void MoveExisteConexionNorte()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act
            bool move = player.Move(map, Direction.North); //conexión norte --> sala 1
            position = player.GetPosition();
            hp = player.GetHP();
            //Assert
            Assert.That(move, Is.True, "Error: el método devuelve 'false' pese a que existe conexión");
            Assert.That(position, Is.EqualTo(1), "Error: el jugador no ha avanzado a la habitación correspondiente");
            Assert.That(hp, Is.EqualTo(HPinicial - player.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el norte
        [Test]
        public void MoveNoExisteConexionNorte()
        {
            //Arrange de 'map' y 'player3' en SetUp
            int HPinicial = player3.GetHP();
            //Act
            bool move = player3.Move(map, Direction.North); //conexión norte --> ninguna
            position = player3.GetPosition();
            hp = player3.GetHP();
            //Assert
            Assert.That(move, Is.False, "Error: el método devuelve 'true' pese a que no existe conexión");
            Assert.That(position, Is.EqualTo(3), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(hp, Is.EqualTo(HPinicial), "Error: la vida ha variado cuando no debería");
        }

        //comprueba que Move() funciona cuando existe una conexión hacia el sur
        [Test]
        public void MoveExisteConexionSur()
        {
            //Arrange de 'map' y 'player2' en SetUp
            int HPinicial = player2.GetHP();
            //Act
            bool move = player2.Move(map, Direction.South); //conexión sur --> sala 0
            position = player2.GetPosition();
            hp = player2.GetHP();
            //Assert
            Assert.That(move, Is.True, "Error: el método devuelve 'false' pese a que existe conexión");
            Assert.That(position, Is.EqualTo(0), "Error: el jugador no ha avanzado a la habitación correspondiente");
            Assert.That(hp, Is.EqualTo(HPinicial - player2.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el sur
        [Test]
        public void MoveNoExisteConexionSur()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act
            bool move = player.Move(map, Direction.South); //conexión sur --> ninguna
            position = player.GetPosition();
            hp = player.GetHP(); 
            //Assert
            Assert.That(move, Is.False, "Error: el método devuelve 'true' pese a que no existe conexión");
            Assert.That(position, Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(hp, Is.EqualTo(HPinicial), "Error: la vida ha variado cuando no debería");
        }

        //comprueba que Move() funciona cuando existe una conexión hacia el este
        [Test]
        public void MoveExisteConexionEste()
        {
            //Arrange de 'map' y 'player2' en SetUp
            int HPinicial = player2.GetHP();
            //Act
            //Assert
            Assert.That(player2.Move(map, Direction.East), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player2.GetPosition(), Is.EqualTo(4), "Error: el jugador no ha avanzado a la habitación corresponidente");
            Assert.That(player2.GetHP(), Is.EqualTo(HPinicial - player2.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el este
        [Test]
        public void MoveNoExisteConexionEste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act-Assert
            Assert.That(player.Move(map, Direction.East), Is.False, "Error: el método devuelve true cuando no existe la conexión ");
            Assert.That(player.GetPosition(), Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move()funciona cuando existe una conexión hacia el oeste
        [Test]
        public void MoveExisteConexionOeste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player3.GetHP();
            //Act-Assert
            Assert.That(player3.Move(map, Direction.West), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player3.GetPosition(), Is.EqualTo(2), "Error: el jugador no ha avanzado a la habitación corresponidente");
            Assert.That(player3.GetHP(), Is.EqualTo(HPinicial - player3.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el oeste
        [Test]
        public void MoveNoExisteConexionOeste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act-Assert
            Assert.That(player.Move(map, Direction.West), Is.False, "Error: el método devuelve true cuando no existe la conexión ");
            Assert.That(player.GetPosition(), Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona correctamente cuando la vida del jugador es 1
        [Test]
        public void MoveConVidaInicial1()
        {
            //PREGUNTAR SI USAR IsAlive()
            //Arrange
            player.ForzarHP(1);
            //Act-Assert
            Assert.That(player.Move(map, Direction.North), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player.GetPosition(), Is.EqualTo(1), "Error: El jugador no ha avanzado a la habitación correspondiente");
            Assert.That(player.GetHP(), Is.EqualTo(-1), "Error: La vida se resta por debajo de 0");  //PREGUNTAR SI DEBERÍA COMPORBARSE -1 o 0
        }
        #endregion

        /// <summary>
        /// Exception ex = Assert.Catch(() => { player.EatItem(map, "Item 9"); }, "Error excepcion");
        /// Assert.That(ex.Message, Is.EqualTo("Item doesn't exist."));
        /// no sabemos si esto es legal
        /// </summary>
        #region Test_PickItem()
        //comprueba que PickItem() funciona cuando se introduce un elemento que no existe en el mapa
        [Test]
        public void PickItemNoExisteEnMapa()
        {
            //Arrange de 'map' y 'player' en SetUp
            int pesoPrevio = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.PickItem(map, "Item 9"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no existe en la sala");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoPrevio), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(""), "Error: se ha añadido un item inexistente en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento que no existe en la sala, pese a estar en el mapa
        [Test]
        public void PickItemNoExisteEnSala()
        {
            //Arrange de 'map' y 'player' en SetUp
            int pesoPrevio = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.PickItem(map, "Item 0"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no existe en la sala");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoPrevio), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(""), "Error: se ha añadido un item que no está en la sala al inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento que existe en la sala, pero excede el peso maximo del inventario
        [Test]
        public void PickItemPesoExcedeMaximo()
        {
            //Arrange de 'map' y 'player' en SetUp
            //Act
            player.ForzarInventario(4); //inserta de 0 a 3
            player.ForzarPeso(18); //Item n.weight = n + 3 --> 3 + 4 + 5 + 6
            string inventario = player.GetInventory().VerLista(); //0_1_2_3_
            int pesoPrevio = player.GetPeso();
            //Assert
            Assert.That(() => { player.PickItem(map, "Item 4"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no cabe en el inventario");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoPrevio), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(inventario), "Error: se ha añadido un item que no cabe en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento que existe en la sala, pero el inventario está al maximo de peso (MAX_WEIGHT)
        [Test]
        public void PickItemPesoExcedeMaximoMaxWeight()
        {
            //Arrange de 'map' y 'player' en SetUp
            //Act
            player.ForzarPeso(player.GetPesoMaximo());

            //Assert
            Assert.That(() => { player.PickItem(map, "Item 4"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no cabe en el inventario");
            Assert.That(player.GetPeso(), Is.EqualTo(player.GetPesoMaximo()), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(""), "Error: se ha añadido un item que no cabe en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento en el inventario vacío
        [Test]
        public void PickItemInventarioVacio()
        {
            //Arrange de 'map' y 'player' en SetUp
            int pesoPrevio = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.PickItem(map, "Item 4"); }, Throws.Nothing, "Error: se lanza excepcion pese a que el item puede ser introducido en el inventario");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoPrevio + map.GetItemWeight(4)), "Error: no ha variado el peso del inventario pese a que se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("4_"), "Error: no se ha añadido el item pese a que cabe en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento en el inventario no vacío
        [Test]
        public void PickItemInventarioNoVacio()
        {
            //Arrange de 'map' y 'player3' en SetUp (player3 está en la sala 3, que tiene el Item 0 y 2)
            //Act
            player3.ForzarPeso(7); //3 + 4 (Item 0 y 1)
            player3.ForzarInventario(2);
            string inventario = player3.GetInventory().VerLista();

            //Assert
            Assert.That(() => { player3.PickItem(map, "Item 2"); }, Throws.Nothing, "Error: se lanza excepcion pese a que el item puede ser introducido en el inventario");
            Assert.That(player3.GetPeso(), Is.EqualTo(7 + map.GetItemWeight(2)), "Error: no ha variado el peso del inventario pese a que se ha cogido el objeto");
            Assert.That(player3.GetInventory().VerLista(), Is.EqualTo(inventario + "2_"), "Error: no se ha añadido el item pese a que cabe en el inventario");
        }
        #endregion

        #region Test_EatItem()
        //comprueba que EatItem() funciona correctamente cuando el item no existe en el mapa
        [Test]
        public void EatItemNoExisteItemMapa()
        {
            //Arrange de 'map' y 'player' en SetUp
            string listaInicial = player.GetInventory().VerLista();
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 9"); }, Throws.Exception, "Error: No existe el item en el mapa");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha cambiado cuando no debería");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha cambiado cuando no debería");
        }

        //comprueba que EatItem() funciona correctamente cuando el item no existe en el inventario
        [Test]
        public void EatItemNoExisteItemInventario()
        {
            //Arrange de 'map' y 'player' en SetUp
            string listaInicial = player.GetInventory().VerLista();
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 1"); }, Throws.Exception, "Error: No existe el item en el inventario");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha cambiado cuando no debería");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha cambiado cuando no debería");
        }

        //comprueba que EatItem() funciona correctamente cuando el item no es comestible, es decir, su hp es 0
        [Test]
        public void EatItemNoComestible()
        {
            //Arrange de 'map' y 'player' en SetUp
            string listaInicial = player.GetInventory().VerLista();
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 0"); }, Throws.Exception, "Error: El item no es comestible (su hp es 0)");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha cambiado cuando no debería");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha cambiado cuando no debería");
        }

        //comprueba que EatItem() funciona correctamente cuando el item existe en el inventario y en el mapa, además de ser comestible
        [Test]
        public void EatItemComestible()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(2);  //inserta de 0 a 1
            player.ForzarPeso(12); //Item n.weight = n + 3 --> 3 + 4 + 5
            player.ForzarHP(5);  //La vida se cambia a 5
            string listaInicial = player.GetInventory().VerLista();
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 1"); }, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial + 1), "Error: la vida no se ha sumado correctamente");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - 4), "Error: el peso no se ha restado correctamente");
        }

        //comprueba que EatItem() funciona correctamente cuando el jugador tiene su hp máximo
        [Test]
        public void EatItemComestibleConMaxHP()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(2);  //inserta de 0 a 1
            player.ForzarPeso(12); //Item n.weight = n + 3 --> 3 + 4 + 5
            string listaInicial = player.GetInventory().VerLista();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 1"); }, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(player.GetMAXHP()), "Error: la vida no se ha sumado correctamente");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - 4), "Error: el peso no se ha restado correctamente");
        }

        //comprueba que EatItem() funciona correctamente cuando el jugador se come un item que le daría un hp superior al máximo si no se controlase
        [Test]
        public void EatItemComestibleConCasiMaxHP()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(4);  //inserta de 0 a 3
            player.ForzarPeso(18); //Item n.weight = n + 3 --> 3 + 4 + 5 + 6
            player.ForzarHP(8);  //La vida se cambia a 8
            string listaInicial = player.GetInventory().VerLista();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 3"); }, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_1_2_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(player.GetMAXHP()), "Error: la vida no se ha sumado correctamente");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - 6), "Error: el peso no se ha restado correctamente");
        }
        #endregion
    }
}
