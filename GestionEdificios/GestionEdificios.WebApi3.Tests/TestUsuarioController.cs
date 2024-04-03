using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.Controllers;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.WebApi3.Tests
{
    [TestClass]
    public class TestsUsuarioController
    {
        private Mock<IUsuarioLogica> mockWebApi;
        private UsuariosController controller;
        private UsuarioDto? adminDto;
        private Usuario admin;

        [TestInitialize]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IUsuarioLogica>(MockBehavior.Strict);
            adminDto = new UsuarioDto()
            {
                Nombre = "Nombre Admin",
                Apellido = "Apellido Admin",
                Email = "admin@test.com",
                Contraseña = "password"
            };

            admin = new Usuario()
            {
                Id = 1,
                Nombre = adminDto.Nombre,
                Apellido = adminDto.Apellido,
                Email = adminDto.Email,
                Contraseña = adminDto.Contraseña
            };
        }

        [TestMethod]
        public void TestAgreagarAdminInvalido()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new UsuariosController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOfType<BadRequestObjectResult>(resultado);
        }

        [TestMethod]
        public void TestAgregarAdminOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Usuario>())).Returns(admin);
            controller = new UsuariosController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(adminDto);
            var dto = resultado.Value as UsuarioDto;
            mockWebApi.VerifyAll();
            Assert.AreEqual(dto.Nombre, admin.Nombre);
            Assert.AreEqual(dto.Apellido, admin.Apellido);
            Assert.AreEqual(dto.Email, admin.Email);
            Assert.AreEqual(dto.Contraseña, admin.Contraseña);
            Assert.AreEqual(resultado.StatusCode, 201);
        }

        [TestMethod]
        public void TestAgregarAdminConEmailExistente()
        {
            UsuarioDto adminDto2 = new UsuarioDto()
            {
                Nombre = "Nombre Admin",
                Apellido = "Apellido Admin",
                Email = "admin@test.com",
                Contraseña = "password"
            };

            Usuario admin2 = new Usuario()
            {
                Id = 1,
                Nombre = adminDto2.Nombre,
                Apellido = adminDto2.Apellido,
                Email = adminDto2.Email,
                Contraseña = adminDto2.Contraseña
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<Usuario>())).Throws(new Exception());
            controller = new UsuariosController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(adminDto2);
            mockWebApi.VerifyAll();
            Assert.AreEqual(resultado.StatusCode, 400);
        }
    }
}