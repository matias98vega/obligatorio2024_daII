using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
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
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                Constructora = "Constructora 1"
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

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDatos))]
        public void TestCrearEdificioDireccionVacia()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = ""
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDatos))]
        public void TestCrearEdificioUbicacionVacia()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = ""
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDatos))]
        public void TestCrearEdificioConstructoraVacia()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                Constructora = ""
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExisteExcepcion))]
        public void TestCrearEdificioNombreYaExistente()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                Constructora = "Consturctora 1"
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            mockRepositorio.Setup(m => m.Existe(edificio)).Returns(true);

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }
    }
}
