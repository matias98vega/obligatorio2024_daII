using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
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
        private IEdificioLogica edificioLogica;
        private Mock<IEdificioRepositorio> mockRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IEdificioRepositorio>(MockBehavior.Strict);
        }

        [TestMethod]
        public void TestCrearEdificioOk()
        {
            Edificio edificio = new Edificio()
            {
                Nombre = "Edificio 1"
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            mockRepositorio.Setup(m => m.Salvar());

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);

            Assert.AreEqual(edificio.Nombre, edificioCreado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDB))]
        public void TestCrearEdificioVacio() 
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new EdificioExcepcionDB("")));
            
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDatos))]
        public void TestCrearEdificioNombreVacio()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = ""
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }
    }
}
