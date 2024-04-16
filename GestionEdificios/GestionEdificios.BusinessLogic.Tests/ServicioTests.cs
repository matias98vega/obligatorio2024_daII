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
        private Mock<IUsuarioRepositorio> usuarioRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IServicioRepositorio>(MockBehavior.Strict);
            usuarioRepositorio = new Mock<IUsuarioRepositorio>(MockBehavior.Strict);
        }

        /****** Crear ******/
        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDB))]
        public void TestAgregarServicioVacio()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new ServicioExcepcionDB("")));

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
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
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioCreado = servicioLogica.Agregar(servicio);
        }

        /*******************/

        /**** Asignar solicitud ****/
        
        [TestMethod]
        public void TestAsignarSolicitudOK()
        {
            int idUsuario = 1;
            Usuario usuario = new Usuario()
            {
                Id = idUsuario,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = usuario,
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 10,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
           

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.AsignarSolicitud(id, idUsuario)).Returns(servicio);
            usuarioRepositorio.Setup(m => m.Obtener(idUsuario)).Returns(usuario);

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioRetorno = servicioLogica.AsignarSolicitud(id,idUsuario);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(servicio.Id, servicioRetorno.Id);
            Assert.AreEqual(usuario.Id, servicioRetorno.UsuarioMantenimiento.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioNoExiste))]
        public void TestAsignarSolicitudServicioNoExiste()
        {
            int idUsuario = 1;
            Usuario usuario = new Usuario()
            {
                Id = idUsuario,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = usuario,
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 10,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };


            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(true);
            mockRepositorio.Setup(m => m.AsignarSolicitud(id, idUsuario)).Returns(servicio);
            usuarioRepositorio.Setup(m => m.Obtener(idUsuario)).Returns(usuario);

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioRetorno = servicioLogica.AsignarSolicitud(id, idUsuario);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioNoEncontradoExcepcion))]
        public void TestAsignarSolicitudUsuarioNoExiste()
        {
            int idUsuario = 1;
            Usuario usuario = null;
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = usuario,
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 10,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };


            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.AsignarSolicitud(id, idUsuario)).Returns(servicio);
            usuarioRepositorio.Setup(m => m.Obtener(idUsuario)).Returns(usuario);

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioRetorno = servicioLogica.AsignarSolicitud(id, idUsuario);

            mockRepositorio.VerifyAll();
        }

        /*******************/

        /****** Actualizar ******/
        [TestMethod]
        public void TestActualizarServicioOK()
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

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(servicio.Id, servicioModificado.Id);
            Assert.AreEqual(servicio.Descripcion, servicioModificado.Descripcion);
            Assert.AreEqual(servicio.Categoria, servicioModificado.Categoria);
            Assert.AreEqual(servicio.Estado, servicioModificado.Estado);
            Assert.AreEqual(servicio.UsuarioMantenimiento, servicioModificado.UsuarioMantenimiento);
            Assert.AreEqual(servicio.FechaInicio, servicioModificado.FechaInicio);
            Assert.AreEqual(servicio.FechaFin, servicioModificado.FechaFin);
            Assert.AreEqual(servicio.CostoTotal, servicioModificado.CostoTotal);
            Assert.AreEqual(servicio.Departamento, servicioModificado.Departamento);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioNoExiste))]
        public void TestActualizarServicioVacio()
        {
            int id = 1;
            Servicio servicioNoExiste = null; 
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

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>())).Throws(new ExcepcionDB("", new ServicioExcepcionDB(""))); ;
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicioNoExiste);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarDescripcionVacia()
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

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarCategoriaVacia()
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

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarUsuarioVacio()
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

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarFechaInicioVacia()
        {
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.MinValue,
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarFechaFinVacia()
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
                FechaFin = DateTime.MinValue,
                CostoTotal = 100,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarDepartamentoVacio()
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
                Departamento = new Departamento()
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDatos))]
        public void TestActualizarCostoVacio()
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Servicio>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioModificado = servicioLogica.Actualizar(id, servicio);

            mockRepositorio.VerifyAll();
        }

        /*******************/

        /****** Eliminar ******/
        [TestMethod]
        public void TestBorrarServicioPorId()
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            mockRepositorio.Setup(m => m.Borrar(servicio));
            mockRepositorio.Setup(m => m.Salvar());
            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(false);

            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            servicioLogica.Eliminar(id);

            Assert.AreEqual(servicioLogica.Existe(servicio), false);
        }

        [TestMethod]
        [ExpectedException(typeof(ServicioExcepcionDB))]
        public void TestBorrarServicioConIdIncorrecto()
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Throws(new ExcepcionDB("", new ServicioExcepcionDB("")));
            servicioLogica = new ServicioLogica(mockRepositorio.Object,usuarioRepositorio.Object);
            servicioLogica.Eliminar(id);
        }

        /*******************/

        /***** Existe ******/
        [TestMethod]
        public void TestExisteServicio()
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Existe(servicio)).Returns(true);
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            bool existe = servicioLogica.Existe(servicio);
            Assert.IsTrue(existe);
        }

        /*******************/

        /***** Obtener ******/
        [TestMethod]
        public void TestObtenerServicio()
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(servicio);
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Servicio servicioRetornado = servicioLogica.Obtener(id);
            Assert.AreEqual(servicio,servicioRetornado);
        }
        /*****************/

        /*** Solicitudes por Categoria ***/
        [TestMethod]
        public void TestObtenerServiciosPorCategoria()
        {
            List<Servicio> servicios = new List<Servicio>();
            int categoriaId = 1;
            int id = 1;
            Servicio servicio = new Servicio()
            {
                Id = id,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio(categoriaId, "Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
            int id2 = 2;
            Servicio servicio2 = new Servicio()
            {
                Id = id2,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio(categoriaId, "Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
            servicios.Add(servicio);
            servicios.Add(servicio2);

            mockRepositorio.Setup(m => m.ObtenerXCategoria(id)).Returns(servicios);
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            List<Servicio> serviciosRetornados = (List<Servicio>)servicioLogica.ObtenerSolicitudesPorCategoria(categoriaId);
            Assert.AreEqual(servicios, serviciosRetornados);
        }
        /*****************/

        /*** Obtener todas las categorias ***/
        [TestMethod]
        public void TestObtenerTodosLosServicios()
        {
            List<Servicio> servicios = new List<Servicio>();           
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
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
            int id2 = 2;
            Servicio servicio2 = new Servicio()
            {
                Id = id2,
                Descripcion = "Limpiar vidrios",
                Categoria = new CategoriaServicio("Plomeria"),
                Estado = EstadosServicios.Abierto,
                UsuarioMantenimiento = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento),
                FechaInicio = DateTime.Parse("22/02/1991"),
                FechaFin = DateTime.Parse("22/02/1991"),
                CostoTotal = 0,
                Departamento = new Departamento(1, 1, true, 1, 1)
            };
            servicios.Add(servicio);
            servicios.Add(servicio2);

            mockRepositorio.Setup(m => m.ObtenerTodos()).Returns(servicios);
            servicioLogica = new ServicioLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            List<Servicio> serviciosRetornados = (List<Servicio>)servicioLogica.ObtenerTodos();
            Assert.AreEqual(servicios, serviciosRetornados);
        }
        /*****************/
    }
}
