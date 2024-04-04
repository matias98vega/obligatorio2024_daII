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

        //public void testcreateadminwithemptyname()
        //{
        //    usuario admin = new usuario()
        //    {
        //        id = guid.newguid(),
        //        name = "",
        //        email = "test01@gmail.com",
        //        password = "password01"
        //    };

        //    mockrepository.setup(m => m.add(it.isany<administrator>()));
        //    logic = new administratorlogic(mockrepository.object);
        //    administrator usercreated = logic.add(admin);
        //}





    }
}
