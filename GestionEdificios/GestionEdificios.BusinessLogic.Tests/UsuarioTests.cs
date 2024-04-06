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

namespace GestionEdificios.BusinessLogic.Tests
{
    [TestClass]
    public class UsuarioTests
    {

        private IUsuarioLogica usuarioLogica;
        private Mock<IUsuarioRepositorio> mockRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IUsuarioRepositorio>(MockBehavior.Strict);
        }


        /****** Crear ******/
        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDB))]
        public void TestAgregarUsuarioVacio()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new UsuarioExcepcionDB("")));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        public void TestCrearUsuarioOK()
        {
            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            mockRepositorio.Setup(m => m.Existe(usuario)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(usuario);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(usuario.Id, usuarioCreado.Id);
            Assert.AreEqual(usuario.Nombre, usuarioCreado.Nombre);
            Assert.AreEqual(usuario.Apellido, usuarioCreado.Apellido);
            Assert.AreEqual(usuario.Email, usuarioCreado.Email);
            Assert.AreEqual(usuario.Contraseña, usuarioCreado.Contraseña);
            Assert.AreEqual(usuario.Rol, usuarioCreado.Rol);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestCrearUsuarioNombreVacio()
        {
            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestCrearUsuarioApellidoVacio()
        {
            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Pepe",
                Apellido = "",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestCrearUsuarioEmailVacio()
        {
            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(usuario);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestCrearUsuarioEmailFormatoInvalido()
        {
            Usuario usuario = new Usuario()
            {
                Id = 1,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "23524632464%&(%$##",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Usuario>()));
            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioCreado = usuarioLogica.Agregar(usuario);
        }
        /******************/
        
        /****** Borrar *******/
        [TestMethod]
        public void TestBorrarUsuarioPorId()
        {
            var id = 1;
            Usuario usuario = new Usuario()
            {
                Id = id,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis2@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(usuario);
            mockRepositorio.Setup(m => m.Borrar(usuario));
            mockRepositorio.Setup(m => m.Salvar());
            mockRepositorio.Setup(m => m.Existe(usuario)).Returns(false);

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            usuarioLogica.Eliminar(id);

            Assert.AreEqual(usuarioLogica.Existe(usuario), false);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDB))]
        public void TestBorrarUsuarioConIdIncorrecto()
        {
            var id = 1;
            Usuario admin = new Usuario()
            {
                Id = id,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis2@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(id))
                            .Throws(new ExcepcionDB("", new UsuarioExcepcionDB("")));
            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            usuarioLogica.Eliminar(id);
        }

        /******/

        /***** Actualizar *****/
        [TestMethod]
        [ExpectedException(typeof(UsuarioEmailYaExisteExcepcion))]
        public void TestActualizarUsuarioConEmailYaUsado()
        {
            var usuarioId = 1;
            Usuario usuario = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            var modificadoId = 2;
            Usuario modificado = new Usuario()
            {
                Id = modificadoId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis2@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };


            Usuario usuario2 = new Usuario()
            {
                Id = 3,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis2@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };


            mockRepositorio.Setup(m => m.Obtener(usuarioId)).Returns(usuario);
            mockRepositorio.Setup(m => m.ObtenerPorEmail(modificado.Email)).Returns(usuario2);
            mockRepositorio.Setup(m => m.Actualizar(usuario));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioModificado = usuarioLogica.Actualizar(usuarioId, modificado);
        }

        [TestMethod]
        public void TestActualizarUsuarioPorId()
        {
            var id = 1;
            Usuario usuario = new Usuario()
            {
                Id = id,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            Usuario usuarioModificado = new Usuario()
            {
                Nombre = "Nuevo Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(usuario);
            mockRepositorio.Setup(m => m.ObtenerPorEmail(usuarioModificado.Email)).Returns((Usuario)null);
            mockRepositorio.Setup(m => m.Actualizar(usuario));
            mockRepositorio.Setup(m => m.Salvar());

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioRetornado = usuarioLogica.Actualizar(id, usuarioModificado);

            mockRepositorio.VerifyAll();
            Assert.AreEqual(usuario, usuarioRetornado);
        }


        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDB))]
        public void TestActualizarUsuarioInvalido()
        {
            var id = 1;
            Usuario usuario = new Usuario()
            {
                Id = id,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            Usuario modificado = new Usuario()
            {
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Throws(new ExcepcionDB("", new UsuarioExcepcionDB("")));
            mockRepositorio.Setup(m => m.Actualizar(usuario));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioModificado = usuarioLogica.Actualizar(id, modificado);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestActualizarConNombreVacio()
        {
            var usuarioId = 1;
            Usuario usuario = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            var modificadoId = 2;
            Usuario modificado = new Usuario()
            {
                Id = usuarioId,
                Nombre = "",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(usuarioId)).Returns(usuario);
            mockRepositorio.Setup(m => m.ObtenerPorEmail(modificado.Email)).Returns((Usuario)null);
            mockRepositorio.Setup(m => m.Actualizar(usuario));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioModificado = usuarioLogica.Actualizar(usuarioId, modificado);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestActualizarConApellidoVacio()
        {
            var usuarioId = 1;
            Usuario usuario = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            var modificadoId = 2;
            Usuario modificado = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(usuarioId)).Returns(usuario);
            mockRepositorio.Setup(m => m.ObtenerPorEmail(modificado.Email)).Returns((Usuario)null);
            mockRepositorio.Setup(m => m.Actualizar(usuario));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioModificado = usuarioLogica.Actualizar(usuarioId, modificado);
        }

        [TestMethod]
        [ExpectedException(typeof(UsuarioExcepcionDatos))]
        public void TestActualizarConEmailVacio()
        {
            var usuarioId = 1;
            Usuario usuario = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "holis@hotmail.com",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            var modificadoId = 2;
            Usuario modificado = new Usuario()
            {
                Id = usuarioId,
                Nombre = "Pepe",
                Apellido = "Veneno",
                Email = "",
                Contraseña = "1234",
                Rol = Roles.Administrador
            };

            mockRepositorio.Setup(m => m.Obtener(usuarioId)).Returns(usuario);
            mockRepositorio.Setup(m => m.ObtenerPorEmail(modificado.Email)).Returns((Usuario)null);
            mockRepositorio.Setup(m => m.Actualizar(usuario));

            usuarioLogica = new UsuarioLogica(mockRepositorio.Object);
            Usuario usuarioModificado = usuarioLogica.Actualizar(usuarioId, modificado);
        }

        /***********/





    }
}
