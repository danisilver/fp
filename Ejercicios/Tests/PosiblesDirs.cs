using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class PosiblesDirs
    {
        [TestMethod]
        public void TestPosiblesDirecciones()
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
            t.posiblesDirs(1,out l,out cont);
            //Assert
            Assert.AreEqual(0, cont, "Fallo: direcciones invalidas, se espera 0");
            
        }
    }
}
