using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HayFantasma
    {
        [TestMethod]
        public void TestHayFantasmaEnCasilla()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(1, 0, 1, 0, 1);

            //Act
            bool hayfantasma = t.hayFantasma(0, 1);

            //Assert
            Assert.IsTrue(hayfantasma, "Fallo: fantasma no detectado");
        }
        [TestMethod]
        public void TestNoHayFantasmaEnCasilla()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);

            //Act
            bool hayfantasma = t.hayFantasma(0, 1);

            //Assert
            Assert.IsFalse(hayfantasma, "Fallo: fantasma detectado");
        }
    }
}
