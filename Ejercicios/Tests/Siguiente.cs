using System;
using P4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Siguiente
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
            bool puedeMover = tPruebas.siguiente(posX, posY, -1, 0, out nx, out ny);

            //Assert
            Assert.IsTrue(puedeMover);
            Assert.AreEqual(0, nx, "Coordenada X incorrecta al ejecutar siguiente");
            Assert.AreEqual(1, ny, "Coordenada Y incorrecta al ejecutar siguiente");
        }

        [TestMethod]
        public void TestSiguienteCeldaVaciaAbajo()
        {
            //Arrange
            Tablero tPruebas = new Tablero(3, 3);
            int posX = 1;
            int posY = 1;
            int nx, ny;

            //Act
            bool puedeMover = tPruebas.siguiente(posX, posY, 1, 0, out nx, out ny);

            //Assert
            Assert.IsTrue(puedeMover);
            Assert.AreEqual(2, nx, "Coordenada X incorrecta al ejecutar siguiente");
            Assert.AreEqual(1, ny, "Coordenada Y incorrecta al ejecutar siguiente");
        }

        [TestMethod]
        public void TestSiguienteCeldaVaciaIzquierda()
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

        [TestMethod]
        public void TestSiguienteCeldaVaciaDerecha()
        {
            //Arrange
            Tablero tPruebas = new Tablero(3, 3);
            int posX = 1;
            int posY = 1;
            int nx, ny;

            //Act
            bool puedeMover = tPruebas.siguiente(posX, posY, 0, 1, out nx, out ny);

            //Assert
            Assert.IsTrue(puedeMover);
            Assert.AreEqual(1, nx, "Coordenada X incorrecta al ejecutar siguiente");
            Assert.AreEqual(2, ny, "Coordenada Y incorrecta al ejecutar siguiente");
        }

        [TestMethod]
        public void TestSiguienteMuroArriba()
        {
            //Arrange
            Tablero tPruebas = new Tablero(3, 3);
            int posX = 1;
            int posY = 1;
            int nx, ny;
            tPruebas.cambiaCasilla(0, 1, Tablero.Casilla.Muro);

            //Act
            bool puedeMover = tPruebas.siguiente(posX, posY, -1, 0, out nx, out ny);

            //Assert
            Assert.IsFalse(puedeMover);
            Assert.AreEqual(0, nx, "Coordenada X incorrecta al ejecutar siguiente");
            Assert.AreEqual(1, ny, "Coordenada Y incorrecta al ejecutar siguiente");
        }
    }
}
