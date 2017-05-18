using P4;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class MuevePacman
    {
        [TestMethod]
        public void TestPosiblesDirecciones()
        {
            //Arrange
            Tablero t = new Tablero(3, 3);
            t.setPersonaje(0, 0, 0, 0, 1);
            ListaPares l;
            int cont;
            //Act
            t.muevePacman();
            t.posiblesDirs(0, out l, out cont);
            //Assert
            Assert.IsNotNull(l, "Fallo: lista de direcciones nula");
            Assert.AreEqual(3, cont, "Fallo: no hay 3 direcciones");
        }
    }
}
