using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CambiaDir
    {
        [TestMethod]
        public void TestCambiaDireccion()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            t.cambiaCasilla(0, 1, Tablero.Casilla.Muro);

            //Act
            bool cambiadir = t.cambiaDir('d');
            t.muevePacman();
            Tablero.Personaje p = t.getPersonaje(0);
            //Assert
            Assert.IsTrue(cambiadir, "Fallo: no se puede cambiar direccion valida");
            Assert.AreEqual(1, p.posX, "Fallo: posicion x incorrecta");
            Assert.AreEqual(0, p.posY, "Fallo: posicion y incorrecta");
        }
    }
}
