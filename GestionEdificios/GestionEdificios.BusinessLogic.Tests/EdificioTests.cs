using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Tests
{
    [TestClass]
    public class EdificioTests
    {
        private IEdificioLogica eidificioLogica;
        private Mock<IEdificioLogica> mockRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IEdificioLogica>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCrearEdificioOk()
        {
            Edificio edificio = new Edificio();

            Edificio edificioCreado = eidificioLogica.Agregar(edificio);

            Assert.AreEqual(edificio.Nombre, edificioCreado.Nombre);
        }
    }
}
