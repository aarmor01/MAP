using System;
using NUnit.Framework;
using Listas;
using Adventure;

namespace Tests
{
    [TestFixture]
    public class TestsPlayer
    {
        Map map;
        Player player;

        [SetUp]
        public void CreateMapYPlayer()
        {
            map = new Map(100, 100);
            map.CreateMap(6, 5, new int[] { 3, 2, 3, 1, 4 }, 0, 5);
            player = new Player("Gato de Guillermo", 0);
        }

        #region Test_Player()
        [Test]
        public void ConstructoraPlayerFunciona()
        {
            //Arrange
            Player playerTest = new Player("Gato de Guillermo", 1);

            //Act-Assert
            Assert.That(playerTest.GetPlayerInfo(), Is.EqualTo("Name: Gato de Guillermo HP: 10 Inventory weight: 0"),
                "Error: El nombre del jugador, HP del mismo o peso de su inventario no están bien establecido/s");
            Assert.That(playerTest.GetPosition(), Is.EqualTo(1), "Error: sala inicial establecida erroneamente");
            Assert.That(playerTest.inventory.VerLista(), Is.EqualTo(""), "Error: inventario creado erroneamente");
        }
        #endregion

        #region Test_Move()
        [Test]
        public void Movelol()
        {
            //Arrange de 'map' y 'player' en SetUp
        }
        #endregion
    }
}
