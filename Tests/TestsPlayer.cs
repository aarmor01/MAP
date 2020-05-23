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

        [SetUp]
        public void CreateMapYPlayer()
        {
            map = new Map(100, 100);
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 4 }, 5, 0);
            player = new Player("Gato de Guillermo", map.GetEntryRoom());
            player2 = new Player("Gato de Guillermo", 1);
            player3 = new Player("Gato de Guillermo", 3);
        }

        #region Test_Player()
        //comprueba que Player() construye al jugador respecto a los parámetros introducidos
        [Test]
        public void ConstructoraPlayerFunciona()
        {
            //PREGUNTAR
            //Arrange
            Player playerTest = new Player("Gato de Guillermo", 1);
            int MAX_HP = player.GetMAXHP();

            //Act-Assert
            //PREGUNTAR SI HAY QUE HACERLO SEPARADO
            Assert.That(playerTest.GetPlayerInfo(), Is.EqualTo("Name: Gato de Guillermo HP: " + MAX_HP + " Inventory weight: 0"),
                "Error: El nombre del jugador, HP del mismo o peso de su inventario no están bien establecido/s");
            Assert.That(playerTest.GetPosition(), Is.EqualTo(1), "Error: sala inicial establecida erroneamente");
            Assert.That(playerTest.GetInventory().VerLista(), Is.EqualTo(""), "Error: inventario creado erroneamente");
        }
        #endregion

        #region Test_Move()
        [Test]
        public void MoveExisteConexionNorte()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act-Assert
            Assert.That(player.Move(map, Direction.North), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player.GetPosition(), Is.EqualTo(1), "Error: el jugador no ha avanzado a la habitación corresponidente");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial - player.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        [Test]
        public void MoveNoExisteConexionNorte()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player3.GetHP();
            //Act-Assert
            Assert.That(player3.Move(map, Direction.North), Is.False, "Error: el método devuelve true cuando no existe la conexión ");
            Assert.That(player3.GetPosition(), Is.EqualTo(3), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(player3.GetHP(), Is.EqualTo(HPinicial), "Error: la vida no se ha restado correctamente");
        }

        [Test]
        public void MoveExisteConexionSur()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player2.GetHP();
            //Act-Assert
            Assert.That(player2.Move(map, Direction.South), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player2.GetPosition(), Is.EqualTo(0), "Error: el jugador no ha avanzado a la habitación corresponidente");
            Assert.That(player2.GetHP(), Is.EqualTo(HPinicial - player2.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

        [Test]
        public void MoveNoExisteConexionSur()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player.GetHP();
            //Act-Assert
            Assert.That(player.Move(map, Direction.South), Is.False, "Error: el método devuelve true cuando no existe la conexión ");
            Assert.That(player.GetPosition(), Is.EqualTo(0), "Error: el jugador ha avanzado a otra habitación cuando no debería");
            Assert.That(player.GetHP(), Is.EqualTo(HPinicial), "Error: la vida no se ha restado correctamente");
        }

        [Test]
        public void MoveExisteConexionEste()
        {
            //Arrange de 'map' y 'player' en SetUp
            int HPinicial = player2.GetHP();
            //Act-Assert
            Assert.That(player2.Move(map, Direction.East), Is.True, "Error: el método devuelve false cuando existe la conexión ");
            Assert.That(player2.GetPosition(), Is.EqualTo(4), "Error: el jugador no ha avanzado a la habitación corresponidente");
            Assert.That(player2.GetHP(), Is.EqualTo(HPinicial - player2.GetHPPERMOVEMENT()), "Error: la vida no se ha restado correctamente");
        }

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

        #region Test_PickItem()
        //comprueba que PickItem() funciona cuando se introduce un elemento que no existe en el mapa
        [Test]
        public void PickItemNoExisteMapa()
        {
            //Arrange de 'map' y 'player' en SetUp
            int pesoPrevio = player.GetPeso();
            //Act-Assert
            Assert.That(() => { player.PickItem(map, "Item 9"); }, Throws.Exception, "Error: no se lanza excepcion pese a que el item no existe en el mapa");
            Assert.That(player.GetPeso(), Is.EqualTo(pesoPrevio), "Error: ha variado el peso del inventario pese a que no se ha cogido el objeto");
        }


        #endregion

        #region Test_EatItem()
        [Test]
        public void EatItemNoExisteMapa()
        {
            //Arrange de 'map' y 'player' en SetUp
            //Act-Assert
        }
        #endregion
    }
}
