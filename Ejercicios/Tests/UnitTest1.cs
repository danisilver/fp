using System;
using P4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSiguienteCeldaVaciaArriba()
        {
            //Arrange
            Tablero tPruebas = new Tablero(3, 3);
            int posX = 1;
            int posY = 1;
            int nx, ny;

            //Act
            bool puedeMover = tPruebas.siguiente(posX, posY, 0, -1, out nx, out ny);

            //Assert
            Assert.IsTrue(puedeMover);
            Assert.AreEqual(1, nx, "Coordenada X incorrecta al ejecutar siguiente");
            Assert.AreEqual(0, ny, "Coordenada Y incorrecta al ejecutar siguiente");
        }
    }
}
