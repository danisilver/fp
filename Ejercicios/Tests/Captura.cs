using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Captura
    {
        [TestMethod]
        public void TestFantasmaCapturaPacman()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            t.setPersonaje(1, 0, 1, 0, -1);

            //Act
            t.mueveFantasmas(0);
            bool captura = t.captura();

            //Assert
            Assert.IsTrue(captura, "Fallo: pacman no capturado");
        }
        [TestMethod]
        public void TestPacmanColisionaFantasma() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 1, 1, 0);
            t.setPersonaje(1, 1, 1, 0, -1);

            //Act
            t.muevePacman();
            bool captura = t.captura();

            //Assert
            Assert.IsTrue(captura, "Fallo: pacman no capturado");
        }
        [TestMethod]
        public void TestFantasmaCapturaPacmanToroide() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            t.setPersonaje(1, 2, 0, 1, 0);

            //Act
            t.mueveFantasmas(0);
            bool captura = t.captura();

            //Assert
            Assert.IsTrue(captura, "Fallo: pacman no capturado");
        }
        [TestMethod]
        public void TestPacmanColisionaFantasmaToroide() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 2, 0, 1);
            t.setPersonaje(1, 2, 0, 0, -1);

            //Act
            t.muevePacman();
            bool captura = t.captura();

            //Assert
            Assert.IsTrue(captura, "Fallo: pacman no capturado");
        }
        [TestMethod]
        public void TestFantasmaNoCapturaPacman()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 1, 0, 0);
            t.setPersonaje(1, 0, 0, 0, 0);

            //Act
            t.mueveFantasmas(0);
            bool captura = t.captura();

            //Assert
            Assert.IsFalse(captura, "Fallo: pacman capturado");
        }
    }
}
