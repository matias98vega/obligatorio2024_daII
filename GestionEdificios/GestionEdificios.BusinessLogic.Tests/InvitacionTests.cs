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
    public class InvitacionTests
    {
        private IInvitacionLogica invitacionLogica;
        private Mock<IInvitacionRepositorio> mockRepositorio;
        private Mock<IUsuarioRepositorio> usuarioRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IInvitacionRepositorio>(MockBehavior.Strict);
            usuarioRepositorio = new Mock<IUsuarioRepositorio>(MockBehavior.Strict);
        }

        /**** Crear ****/
        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDB))]
        public void TestCrearInvitacionVacia()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new InvitacionExcepcionDB("")));

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        public void TestCrearInvitacionOK()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Existe(invitacion)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());
            usuarioRepositorio.Setup(m => m.Existe(encargado)).Returns(true);

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(invitacion.Id, invitacionCreada.Id);
            Assert.AreEqual(invitacion.Email, invitacionCreada.Email);
            Assert.AreEqual(invitacion.Nombre, invitacionCreada.Nombre);
            Assert.AreEqual(invitacion.FechaLimite, invitacionCreada.FechaLimite);
            Assert.AreEqual(invitacion.Estado, invitacionCreada.Estado);
            Assert.AreEqual(invitacion.Encargado, invitacionCreada.Encargado);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExisteExcepcion))]
        public void TestCrearInvitacionYaExiste()
        {
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Existe(invitacion)).Returns(true);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestCrearInvitacionNombreVacio()
        {
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestCrearInvitacionEmailFormatoInvalido()
        {
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "75373737",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestCrearInvitacionEmailVacio()
        {
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestCrearInvitacionFechaVacia()
        {
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.MinValue,
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new Usuario("Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento)
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);
        }


        [TestMethod]
        [ExpectedException(typeof(UsuarioNoEncontradoExcepcion))]
        public void TestCrearInvitacionUsuarioVacio()
        {
            int id = 1;
            Usuario encargado = null;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado,
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Existe(invitacion)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());
            usuarioRepositorio.Setup(m => m.Existe(encargado)).Returns(false);
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionCreada = invitacionLogica.Agregar(invitacion);
        }


        /***************/

        /****** Actualizar ******/
        [TestMethod]
        public void TestActualizarInvitacionOK()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(invitacion, invitacionModificada);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionNoExiste))]
        public void TestActualizarInvitacionNoExiste()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;

            Invitacion invitacionVacia = null;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacionVacia);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestActualizarEmailIncorrecto()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "3468595",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestActualizarEmailVacio()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestActualizarNombreVacio()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "holis@gmail.com",
                Nombre = "",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestActualizarFechaLimiteVacia()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.MinValue,
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }

        [ExpectedException(typeof(InvitacionExcepcionDatos))]
        public void TestActualizarUsuarioVacio()
        {

            Usuario encargado = null;
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "holis@gmail.com",
                Nombre = "",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Actualizar(It.IsAny<Invitacion>()));
            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Salvar());
            usuarioRepositorio.Setup(m => m.Existe(encargado)).Returns(false);

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionModificada = invitacionLogica.Actualizar(id, invitacion);

            mockRepositorio.VerifyAll();
        }
        /***************/
        /***** Eliminar *****/
        [TestMethod]
        public void TestEliminarInvitacionPorId()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            mockRepositorio.Setup(m => m.Borrar(invitacion));
            mockRepositorio.Setup(m => m.Existe(invitacion)).Returns(false);

            mockRepositorio.Setup(m => m.Salvar());

            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            invitacionLogica.Eliminar(id);


            Assert.AreEqual(invitacionLogica.Existe(invitacion), false);
        }

        [TestMethod]
        [ExpectedException(typeof(InvitacionExcepcionDB))]
        public void TestEliminarInvitacionConIdIncorrecto()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };


            mockRepositorio.Setup(m => m.Obtener(id)).Throws(new ExcepcionDB("", new InvitacionExcepcionDB("")));
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            invitacionLogica.Eliminar(id);
        }


        /***************/

        /***** Obtener ******/
        [TestMethod]
        public void TestObtenerInvitacion()
        {
            Usuario encargado = new Usuario(1, "Pepe", "Veneno", "Holis@gmail.com", Roles.Mantenimiento);
            int id = 1;
            Invitacion invitacion = new Invitacion()
            {
                Id = id,
                Email = "Holis@gmail.com",
                Nombre = "Pepe",
                FechaLimite = DateTime.Parse("22/02/1991"),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = encargado
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(invitacion);
            invitacionLogica = new InvitacionLogica(mockRepositorio.Object, usuarioRepositorio.Object);
            Invitacion invitacionRetornada = invitacionLogica.Obtener(id);
            Assert.AreEqual(invitacion, invitacionRetornada);
        }
        /*****************/








    }
}
