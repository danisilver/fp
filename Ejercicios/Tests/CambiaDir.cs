using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CambiaDir
    {
        [TestMethod]
        public void TestCambiaDireccionAbajo()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);

            //Act
            bool cambiadir = t.cambiaDir('d');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsTrue(cambiadir, "Fallo: no se puede cambiar direccion valida");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(0, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDireccionArriba() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);

            //Act
            bool cambiadir = t.cambiaDir('u');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsTrue(cambiadir, "Fallo: no se puede cambiar direccion valida");
            Assert.AreEqual(0, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(1, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDireccionDerecha() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 1, 0);

            //Act
            bool cambiadir = t.cambiaDir('r');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsTrue(cambiadir, "Fallo: no se puede cambiar direccion valida");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(2, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDireccionIzquierda() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);

            //Act
            bool cambiadir = t.cambiaDir('l');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsTrue(cambiadir, "Fallo: no se puede cambiar direccion valida");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(0, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDirMuroArriba() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);

            //Act
            bool cambiadir = t.cambiaDir('u');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsFalse(cambiadir, "Fallo: puede cambiar direccion");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(2, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDirMuroAbajo() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);
            t.cambiaCasilla(2, 1, Tablero.Casilla.Muro);

            //Act
            bool cambiadir = t.cambiaDir('d');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsFalse(cambiadir, "Fallo: puede cambiar direccion");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(2, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDirMuroDerecha() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, -1, 0);
            t.cambiaCasilla(1, 2, Tablero.Casilla.Muro);

            //Act
            bool cambiadir = t.cambiaDir('r');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsFalse(cambiadir, "Fallo: puede cambiar direccion");
            Assert.AreEqual(0, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(1, p.posY, "Fallo: posicion y incorrecta");
        }
        [TestMethod]
        public void TestCambiaDirMuroIzquierda() {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, -1, 0);
            t.cambiaCasilla(1, 0, Tablero.Casilla.Muro);

            //Act
            bool cambiadir = t.cambiaDir('l');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsFalse(cambiadir, "Fallo: puede cambiar direccion");
            Assert.AreEqual(0, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(1, p.posY, "Fallo: posicion y incorrecta");
        }
    }
}
