using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using GestionEdificios.Domain;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using GestionEdificios.Domain.Enumerados;
using System.Security.Cryptography;

namespace GestionEdificios.BusinessLogic.Tests
{
    [TestClass]
    public  class ServicioTests
    {
        private IServicioLogica servicioLogica;
        private Mock<IServicioRepositorio> mockRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IServicioRepositorio>(MockBehavior.Strict);
        }

        /****** Crear ******/
        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDB))]
        public void TestAgregarServicioVacio()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new ServicioExcepcionDB("")));

            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }


        [TestMethod]
        public void TestCrearServicioOK()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(servicio.Id, servicioCreado.Id);
            Assert.AreEqual(servicio.Descripcion, servicioCreado.Descripcion);
            Assert.AreEqual(servicio.Categoria, servicioCreado.Categoria);
            Assert.AreEqual(servicio.Estado, servicioCreado.Estado);
            Assert.AreEqual(servicio.UsuarioMantenimiento, servicioCreado.UsuarioMantenimiento);
            Assert.AreEqual(servicio.FechaInicio, servicioCreado.FechaInicio);
            Assert.AreEqual(servicio.FechaFin, servicioCreado.FechaFin);
            Assert.AreEqual(servicio.CostoTotal, servicioCreado.CostoTotal);
            Assert.AreEqual(servicio.Departamento, servicioCreado.Departamento);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExisteExcepcion))]
        public void TestCrearServicioYaExiste()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(true);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioDescripcionVacia()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioCategoriaVacia()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio(""),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioUsuarioVacio()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioFechaInicioVacia()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "", Roles.Mantenimiento),
                FechaInicio = DateTime.MinValue,
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioFechaFinVacia()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.MinValue,                
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioDepartamentoVacio()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento()
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestCrearServicioCostoVacio()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Servicio>()));
            servicioLogica = new ServicioLogica(mockRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        /*******************/








    }
}
