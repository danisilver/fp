using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MuevePacman
    {
        [TestMethod]
        public void TestMuevePacmanArriba()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, -1, 0);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(0,t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanAbajo()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 1, 0);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanDerecha()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanIzquierda()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, -1);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(0, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanArriba()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 2, -1, 0);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanAbajo()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 2, 1, 0);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(0, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanDerecha()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 2, 0, 1);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(0, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanIzquierda()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 0, 0, -1);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanArribaMurp()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);
            t.setPersonaje(0, 1, 1, -1, 0);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanAbajoMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 1, 0);
            t.cambiaCasilla(2, 1, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanDerechaMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, 1);
            t.cambiaCasilla(1, 2, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMuevePacmanIzquierdaMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 1, 1, 0, -1);
            t.cambiaCasilla(1, 0, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(1, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(1, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanArribaMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 2, -1, 0);
            t.cambiaCasilla(2, 2, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(0, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanAbajoMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 2, 1, 0);
            t.cambiaCasilla(0, 2, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanDerechaMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 2, 0, 1);
            t.cambiaCasilla(2, 0, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(2, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }
        [TestMethod]
        public void TestMueveToroidePacmanIzquierdaMuro()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 2, 0, 0, -1);
            t.cambiaCasilla(2, 2, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.AreEqual(2, t.getPersonaje(0).posX, "Posicion X incorrecta de Pacman");
            Assert.AreEqual(0, t.getPersonaje(0).posY, "Posicion Y incorrecta de Pacman");
        }

        /*
        [TestMethod]
        public void TestMuevePacmanComeVitamina()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Vitamina);
            //Act

            t.muevePacman();
            //Assert
            Assert.IsNull(t.cas[2, 0]);
        }

        [TestMethod]
        public void TestMuevePacmanComePunto()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Comida);
            //Act
            t.muevePacman();
            //Assert
            Assert.AreEqual(0, t.getNumComida(), "Fallo: no se comio el punto");
        }*/
    }
}
