using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Tests
{
    [TestClass]
    public class DepartamentoTests
    {
        private IDepartamentoLogica departamentoLogica;
        private Mock<IDepartamentoRepositorio> mockRepositorio;
        private Dueño dueño;
        private Departamento departamento;

        [TestInitialize]
        public void SetUp()
        {
            dueño = new Dueño()
            {
                Email = "dueño@email.com",
                Nombre = "Dueño 1",
                Apellido = "Dueño 2",
                Id = 1
            };
            mockRepositorio = new Mock<IDepartamentoRepositorio>(MockBehavior.Strict);
            this.departamento = new Departamento()
            {
                EdificioId = 1,
                Piso = 1,
                Numero = 1,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 3,
                Dueño = dueño
            };
        }

        [TestMethod]
        public void TestCrearDepartamentoOk()
        {
            mockRepositorio.Setup(m => m.BuscarDepartamentoExistente(departamento)).Returns(false);
            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Departamento>()));
            mockRepositorio.Setup(m => m.Salvar());

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(this.departamento);

            Assert.AreEqual(departamento.Piso, departamentoCreado.Piso);
            Assert.AreEqual(departamento.Numero, departamentoCreado.Numero);
        }

        [TestMethod]
        [ExpectedException(typeof(DepartamentoExcepcionDB))]
        public void TestCrearDepartamentoVacio()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new DepartamentoExcepcionDB("")));

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(DepartamentoExcepcionDatos))]
        public void TestCrearDepartamentoCamposInvalidos()
        {
            Departamento departamentoVacio = new Departamento()
            {
                Id = 1,
                Piso = -1,
                CantidadBaños = 0,
                CantidadCuartos = 1,
                Dueño = dueño
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Departamento>()));
            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(departamentoVacio);
        }
        [TestMethod]
        [ExpectedException(typeof(DepartamentoExcepcionDatos))]
        public void TestCrearDepartamentoSinDueño()
        {
            Departamento departamentoSinDueño = new Departamento()
            {
                Id = 1,
                Piso = 2,
                CantidadCuartos = 1,
                CantidadBaños = 1,
                Dueño = null
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Departamento>()));
            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(departamentoSinDueño);
        }

        [TestMethod]
        [ExpectedException(typeof(DepartamentoExisteExcepcion))]
        public void TestCrearDepartamentoYaExistente()
        {
            Departamento departamentoExistente = new Departamento()
            {
                Id = 1,
                EdificioId = 1,
                Piso = 1,
                Numero = 1,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 3,
                Dueño = dueño
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Departamento>()));
            mockRepositorio.Setup(m => m.BuscarDepartamentoExistente(departamentoExistente)).Returns(true);

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(departamentoExistente);
        }

        [TestMethod]
        public void TestEditarDepartamentoOk()
        {
            Departamento departamentoModificado = new Departamento()
            {
                Id = 1,
                EdificioId = 1,
                Piso = 1,
                Numero = 1,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 5,
                Dueño = dueño
            };
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(departamento);
            mockRepositorio.Setup(m => m.Actualizar(departamento));
            mockRepositorio.Setup(m => m.Salvar());

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoRetornado = departamentoLogica.Actualizar(1, departamentoModificado);
            Assert.AreEqual(departamentoRetornado.CantidadCuartos, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(DepartamentoNoEncontradoExcepcion))]
        public void TestEditarDepartamentoInvalido()
        {
            Departamento departamento = new Departamento()
            {
                Id = 1,
                EdificioId = 1,
                Piso = 1,
                Numero = 1,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 5,
                Dueño = dueño
            };

            Departamento departamentoModificado = new Departamento()
            {
                EdificioId = departamento.EdificioId,
                Piso = departamento.Piso,
                Numero = departamento.Numero,
                CantidadBaños = departamento.CantidadBaños,
                CantidadCuartos = departamento.CantidadCuartos,
                Dueño = departamento.Dueño
            };
            mockRepositorio.Setup(m => m.Obtener(2)).Returns((Departamento)null);

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento edificioRetornado = departamentoLogica.Actualizar(2, departamentoModificado);
        }

        [TestMethod]
        public void TestEliminarDepartamento()
        {
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(departamento);
            mockRepositorio.Setup(m => m.Borrar(departamento));
            mockRepositorio.Setup(m => m.Salvar());
            mockRepositorio.Setup(m => m.Existe(departamento)).Returns(false);

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            departamentoLogica.Eliminar(1);

            Assert.AreEqual(departamentoLogica.Existe(departamento), false);
        }

        [TestMethod]
        public void TestObtenerDepartamentoPorId()
        {
            mockRepositorio.Setup(m => m.Obtener(1)).Returns(departamento);

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamento2 = departamentoLogica.Obtener(1);

            Assert.AreEqual(departamento.Id, departamento2.Id);
            Assert.AreEqual(departamento.Piso, departamento2.Piso);
            Assert.AreEqual(departamento.Numero, departamento2.Numero);
            Assert.AreEqual(departamento.EdificioId, departamento2.EdificioId);
        }

        [TestMethod]
        [ExpectedException(typeof(DepartamentoExcepcionDatos))]
        public void TestObtenerDepartamentoPorIdInvalido()
        {
            mockRepositorio.Setup(m => m.Obtener(1)).Throws(new BaseDeDatosExcepcion("", new DepartamentoExcepcionDatos("")));

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamento2 = departamentoLogica.Obtener(1);
        }

        [TestMethod]
        public void TestObtenerTodosLosDepartamentos()
        {
            Departamento departamento2 = new Departamento()
            {
                Id = 2,
                EdificioId = 1,
                Piso = 1,
                Numero = 2,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 1,
                Dueño = dueño
            };
            List<Departamento> departamentos = new List<Departamento>();
            departamentos.Add(departamento);
            departamentos.Add(departamento2);

            mockRepositorio.Setup(m => m.ObtenerTodos()).Returns(departamentos);
            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            IEnumerable<Departamento> edificiosEsperados = departamentoLogica.ObtenerTodos();

            mockRepositorio.VerifyAll();
            Assert.AreEqual(departamentos, edificiosEsperados);
        }
    }
}
