using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PosiblesDirs
    {
        [TestMethod]
        public void TestPosiblesDirecciones0()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 1, 1, 1, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);
            t.cambiaCasilla(1, 0, Tablero.Casilla.Muro);
            t.cambiaCasilla(1, 2, Tablero.Casilla.Muro);
            t.cambiaCasilla(2, 1, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;

            //Act
            t.posiblesDirs(1, out l, out cont);

            //Assert
            Assert.AreEqual(0, cont, "Fallo: direcciones invalidas, se espera 0");
            
        }

        [TestMethod]
        public void TestPosiblesDirecciones1()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 1, 1, 1, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);
            t.cambiaCasilla(1, 0, Tablero.Casilla.Muro);
            t.cambiaCasilla(1, 2, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;

            //Act
            t.posiblesDirs(1, out l, out cont);

            //Assert
            Assert.AreEqual(1, cont, "Fallo: direcciones invalidas, se espera 0");

        }

        [TestMethod]
        public void TestPosiblesDirecciones2()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 1, 1, 1, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);
            t.cambiaCasilla(1, 0, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;

            //Act
            t.posiblesDirs(2, out l, out cont);

            //Assert
            Assert.AreEqual(2, cont, "Fallo: direcciones invalidas, se espera 0");

        }

        [TestMethod]
        public void TestPosiblesDirecciones3()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 1, 1, 1, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);
            ListaPares l;
            int cont;

            //Act
            t.posiblesDirs(1, out l, out cont);

            //Assert
            Assert.AreEqual(3, cont, "Fallo: direcciones invalidas, se espera 0");

        }

        [TestMethod]
        public void TestPosiblesDirecciones4()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 1, 1, 1, 1);
            ListaPares l;
            int cont;

            //Act
            t.posiblesDirs(1, out l, out cont);

            //Assert
            Assert.AreEqual(4, cont, "Fallo: direcciones invalidas, se espera 0");

        }
    }
}
