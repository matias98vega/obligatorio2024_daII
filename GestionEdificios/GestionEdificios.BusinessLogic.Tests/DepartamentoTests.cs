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
            dueño = new Dueño();
        }

        [TestMethod]
        public void TestCrearDepartamentoOk()
        {
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
    }
}
