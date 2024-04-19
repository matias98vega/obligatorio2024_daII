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
        private Constructora constructora;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IEdificioRepositorio>(MockBehavior.Strict);
            constructora = new Constructora() { Id = 1, Nombre = "Constructora 1"};

            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };
        }

        [TestMethod]
        public void TestCrearEdificioOk()
        {
            Edificio edificio = new Edificio()
            {
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                Constructora = constructora
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            mockRepositorio.Setup(m => m.Existe(It.IsAny<Edificio>())).Returns(false);
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
                Constructora = null
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
                Constructora = constructora
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Edificio>()));
            mockRepositorio.Setup(m => m.Existe(edificio)).Returns(true);

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioCreado = edificioLogica.Agregar(edificio);
        }

        [TestMethod]
        public void TestEditarEdificioOk()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };

            Edificio edificioModificado = new Edificio()
            {
                Nombre = edificio.Nombre,
                Direccion = edificio.Direccion,
                Ubicacion = edificio.Ubicacion,
                GastosComunes = 4500,
                Constructora = constructora
            };
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(edificio);            
            mockRepositorio.Setup(m => m.Actualizar(edificio));
            mockRepositorio.Setup(m => m.Salvar());

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioRetornado = edificioLogica.Actualizar(1, edificioModificado);
            Assert.AreEqual(edificioRetornado.GastosComunes, 4500);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioNoEncontradoExcepcion))]
        public void TestEditarEdificioInvalido()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };

            Edificio edificioModificado = new Edificio()
            {
                Nombre = edificio.Nombre,
                Direccion = edificio.Direccion,
                Ubicacion = edificio.Ubicacion,
                GastosComunes = 4500,
                Constructora = constructora
            };
            mockRepositorio.Setup(m => m.Obtener(2)).Returns((Edificio)null);

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificioRetornado = edificioLogica.Actualizar(2, edificioModificado);
        }

        [TestMethod]
        public void TestEliminarEdificio()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(edificio);
            mockRepositorio.Setup(m => m.Borrar(edificio));
            mockRepositorio.Setup(m => m.Salvar());
            mockRepositorio.Setup(m => m.Existe(edificio)).Returns(false);

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            edificioLogica.Eliminar(1);

            Assert.AreEqual(edificioLogica.Existe(edificio), false);
        }

        [TestMethod]
        public void TestObtenerEdificioPorId()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(edificio);

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificio2 = edificioLogica.Obtener(1);

            Assert.AreEqual(edificio.Id, edificio2.Id);
            Assert.AreEqual(edificio.Nombre, edificio2.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(EdificioExcepcionDatos))]
        public void TestObtenerEdificioPorIdInvalido()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };
            mockRepositorio.Setup(m => m.Obtener(1)).Throws(new BaseDeDatosExcepcion("", new EdificioExcepcionDatos("")));

            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            Edificio edificio2 = edificioLogica.Obtener(1);
        }

        [TestMethod]
        public void TestObtenerTodosLosEdificios()
        {
            Edificio edificio = new Edificio()
            {
                Id = 1,
                Nombre = "Edificio 1",
                Direccion = "Dirección 1",
                Ubicacion = "34°54'31.6\"S 56°11'27.1\"W",
                GastosComunes = 3600,
                Constructora = constructora
            };

            Edificio edificio2 = new Edificio()
            {
                Id = 2,
                Nombre = "Edificio 2",
                Direccion = "Dirección 2",
                Ubicacion = "34°54'31.6\"S 56°30'27.1\"W",
                GastosComunes = 5500,
                Constructora = constructora
            };
            List<Edificio> edificios = new List<Edificio>();
            edificios.Add(edificio);
            edificios.Add(edificio2);

            mockRepositorio.Setup(m => m.ObtenerTodos()).Returns(edificios);
            edificioLogica = new EdificioLogica(mockRepositorio.Object);
            IEnumerable<Edificio> edificiosEsperados = edificioLogica.ObtenerTodos();

            mockRepositorio.VerifyAll();
            Assert.AreEqual(edificios, edificiosEsperados);
        }
    }
}
