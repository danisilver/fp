using System;
using P4;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FinNivel
    {
        [TestMethod]
        public void TestNivelTerminado()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Comida);
            t.setNumComida(1);
            t.setPersonaje(0, 0, 0, 0, 1);

            //Act
            t.muevePacman();
            int numcomida = t.getNumComida();
            bool fin = t.finNivel();

            //Assert
            Assert.AreEqual(0, numcomida, "Fallo, aun hay comida");
            Assert.AreEqual(true, fin, "Fallo, no hay comida y termino la partida");
        }
    }
}
