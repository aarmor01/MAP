using System;
using NUnit.Framework;
using Listas;
using Adventure;

namespace Tests
{
    [TestFixture]
    public class TestsPlayer
    {
        #region Test_ Player()
        [Test]
        public void Constructora()
        {
            //Arrange
            Player player = new Player("Gato de Guillermo", 1);

            //Act-Assert
            Assert.That(player.GetPlayerInfo(), Is.EqualTo("Name: Gato de Guillermo HP: 10 Inventory weight: 0"),
                "Error: El nombre del jugador, HP del mismo o peso de su inventario no están bien establecido/s");
            Assert.That(player.GetPosition(), Is.EqualTo(1), "Error: sala inicial establecida erroneamente");
            Assert.That(player.inventory.VerLista(), Is.EqualTo(""), "Error: inventario creado erroneamente");
        }
        #endregion
    }
}
