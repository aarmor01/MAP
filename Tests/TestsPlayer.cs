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
        public void CreateVariables()
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
            bool move = player2.Move(map, Direction.East); //conexión este --> sala 4 
            position = player2.GetPosition();
            hp = player2.GetHP();
            //Assert
            Assert.That(move, Is.True, "Error: el método devuelve 'false' pese a que existe conexión");
            Assert.That(position, Is.EqualTo(4), "Error: el jugador no ha avanzado a la habitación correspondiente");
            Assert.That(hp, Is.EqualTo(HPinicial - player2.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el este
        [Test]
        public void MoveNoExisteConexionEste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act
            bool move = player.Move(map, Direction.East); //conexión este --> ninguno
            position = player.GetPosition();
            hp = player.GetHP();
            //Assert
            Assert.That(move, Is.False, "Error: el método devuelve 'true' pese a que no existe conexión");
            Assert.That(position, Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(hp, Is.EqualTo(HPinicial), "Error: la vida ha variado cuando no debería");
        }

        //comprueba que Move()funciona cuando existe una conexión hacia el oeste
        [Test]
        public void MoveExisteConexionOeste()
        {
            //Arrange de 'map' y 'player3' en SetUp
            int HPinicial = player3.GetHP();
            //Act
            bool move = player3.Move(map, Direction.West); //conexión oeste --> sala 2
            position = player3.GetPosition();
            hp = player3.GetHP();
            //Assert
            Assert.That(move, Is.True, "Error: el método devuelve 'false' pese a que existe conexión");
            Assert.That(position, Is.EqualTo(2), "Error: el jugador no ha avanzado a la habitación correspondiente");
            Assert.That(hp, Is.EqualTo(HPinicial - player3.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        //comprueba que Move() funciona cuando no existe una conexión hacia el oeste
        [Test]
        public void MoveNoExisteConexionOeste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act
            bool move = player.Move(map, Direction.West); //conexión oeste --> ninguno
            position = player.GetPosition();
            hp = player.GetHP();
            //Assert
            Assert.That(move, Is.False, "Error: el método devuelve 'true' cuando no existe la conexión");
            Assert.That(position, Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(hp, Is.EqualTo(HPinicial), "Error: la vida ha variado cuando no debería");
        }
        #endregion

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
            player.ForzarInventario(4); //inserta de 0 a 3
            player.ForzarPeso(18); //Item n.weight = n + 3 --> 3 + 4 + 5 + 6
            inventory = player.GetInventory().VerLista(); //0_1_2_3_
            weight = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.PickItem(map, "Item 4"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no cabe en el inventario");
            Assert.That(player.GetPeso(), Is.EqualTo(weight), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(inventory), "Error: se ha añadido un item que no cabe en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento que existe en la sala, pero el inventario está al maximo de peso (MAX_WEIGHT)
        [Test]
        public void PickItemPesoExcedeMaximoMaxWeight()
        {
            //Arrange de 'map' y 'player' en SetUp
            player2.ForzarInventario2(); //insert items 0, 1, 3, 4
            player2.ForzarPeso(player2.GetPesoMaximo()); //3 + 4 + 6 + 7 = 20
            inventory = player2.GetInventory().VerLista(); //0_1_3_4_
            //Act-Assert
            Assert.That(() => { player2.PickItem(map, "Item 2"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no cabe en el inventario");
            Assert.That(player2.GetPeso(), Is.EqualTo(player2.GetPesoMaximo()), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
            Assert.That(player2.GetInventory().VerLista(), Is.EqualTo(inventory), "Error: se ha añadido un item que no cabe en el inventario");
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento en el inventario vacío
        [Test]
        public void PickItemInventarioVacio()
        {
            //Arrange de 'map' y 'player' en SetUp
            int pesoPrevio = player.GetPeso();
            string inventarioPrevio = player.GetInventory().VerLista();
            //Act-Assert
            Assert.That(EjecutarPickItemInventarioVacio, Throws.Nothing, "Error: se lanza excepcion pese a que el item puede ser introducido en el inventario");
            Assert.That(weight, Is.EqualTo(pesoPrevio + map.GetItemWeight(4)), "Error: no ha variado el peso del inventario pese a que se ha cogido el objeto");
            Assert.That(inventory, Is.EqualTo(inventarioPrevio + "4_"), "Error: no se ha añadido el item pese a que cabe en el inventario");
        }

        //metodo auxiliar para la ejecucion del test PickItemInventarioVacio()
        private void EjecutarPickItemInventarioVacio()
        {
            player.PickItem(map, "Item 4");
            weight = player.GetPeso();
            inventory = player.GetInventory().VerLista();
        }

        //comprueba que PickItem() funciona cuando se introduce un elemento en el inventario no vacío
        [Test]
        public void PickItemInventarioNoVacio()
        {
            //Arrange de 'map' y 'player3' en SetUp (player3 está en la sala 3, que tiene el Item 0 y 2)
            player3.ForzarPeso(7); //3 + 4 (Item 0 y 1)
            player3.ForzarInventario(2);
            string inventario = player3.GetInventory().VerLista();
            int pesoPrevio = player3.GetPeso();
            //Act-Assert
            Assert.That(EjecutarPickItemInventarioNoVacio, Throws.Nothing, "Error: se lanza excepcion pese a que el item puede ser introducido en el inventario");
            Assert.That(weight, Is.EqualTo(pesoPrevio + map.GetItemWeight(2)), "Error: no ha variado el peso del inventario pese a que se ha cogido el objeto");
            Assert.That(inventory, Is.EqualTo(inventario + "2_"), "Error: no se ha añadido el item pese a que cabe en el inventario");
        }

        //metodo auxiliar para la ejecucion del test PickItemInventarioNoVacio()
        private void EjecutarPickItemInventarioNoVacio()
        {
            player3.PickItem(map, "Item 2");
            weight = player3.GetPeso();
            inventory = player3.GetInventory().VerLista();
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
            Assert.That(() => { player.EatItem(map, "Item 9"); }, Throws.Exception, "Error: no lanza excepcion pese a que no existe el item en el mapa");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado pese a no comerse el item");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha variado pese a no comerse el item");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha variado pese a no comerse el item");
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
            Assert.That(() => { player.EatItem(map, "Item 1"); }, Throws.Exception, "Error: no lanza excepcion pese a que no existe el item en el inventario");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado pese a no comerse el item");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha variado pese a no comerse el item");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha variado pese a no comerse el item");
        }

        //comprueba que EatItem() funciona correctamente cuando el item no es comestible, es decir, su hp es 0
        [Test]
        public void EatItemNoComestible()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(1);  //inserta el 0
            player.ForzarPeso(3); //item 0 = 3
            string listaInicial = player.GetInventory().VerLista();
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.EatItem(map, "Item 0"); }, Throws.Exception, "Error: no lanza excepcion pese a que el item no es comestible (ya que su hp es 0)");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo(listaInicial), "Error: la lista ha cambiado pese a no comerse el item");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida ha variado pese a no comerse el item");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial), "Error: el peso ha variado pese a no comerse el item");
        }

        //comprueba que EatItem() funciona correctamente cuando el item existe en el inventario y en el mapa, además de ser comestible
        [Test]
        public void EatItemComestible()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(4); //inserta de 0 a 3
            player.ForzarPeso(18); //3 + 4 + 5 + 6
            player.ForzarHP(5); //hp = 5
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(EjecutarEatItem, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_1_2_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial + map.GetItemHP(3)), "Error: la vida no se ha sumado correctamente");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - map.GetItemWeight(3)), "Error: el peso no se ha restado correctamente");
        }


        //comprueba que EatItem() funciona correctamente cuando el jugador tiene su hp máximo
        [Test]
        public void EatItemComestibleConMaxHP()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(4);  //inserta de 0 a 3
            player.ForzarPeso(18); // 3 + 4 + 5 + 6
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(EjecutarEatItem, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_1_2_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida no se ha sumado correctamente (debería ser MAX_HP)");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - map.GetItemWeight(3)), "Error: el peso no se ha restado correctamente");
        }

        //comprueba que EatItem() funciona correctamente cuando el jugador se come un item que le daría un hp superior al máximo si no se controlase
        [Test]
        public void EatItemComestibleConCasiMaxHP()
        {
            //Arrange de 'map' y 'player' en SetUp
            player.ForzarInventario(4);  //inserta de 0 a 3
            player.ForzarPeso(18); //3 + 4 + 5 + 6
            player.ForzarHP(8);  //hp = 8
            int HPinicial = player.GetHP();
            int pesoInicial = player.GetPeso();
            //Act-Assert
            Assert.That(EjecutarEatItem, Throws.Nothing, "Error: El item es comestible, pero se ha producido una excepción");
            Assert.That(player.GetInventory().VerLista(), Is.EqualTo("0_1_2_"), "Error: no se ha borrado el item de la lista correctamente");
            Assert.That(player.GetHP(), Is.EqualTo(player.GetMAXHP()), "Error: la vida no se ha sumado correctamente (debería ser MAX_HP)");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoInicial - map.GetItemWeight(3)), "Error: el peso no se ha restado correctamente");
        }

        //metodo auxiliar para las pruebas de EatItem() cuando no lanzan excepcion
        private void EjecutarEatItem()
        {
            player.EatItem(map, "Item 3");
            weight = player.GetPeso();
            hp = player.GetHP();
            inventory = player.GetInventory().VerLista();
        }
        #endregion
    }
}
