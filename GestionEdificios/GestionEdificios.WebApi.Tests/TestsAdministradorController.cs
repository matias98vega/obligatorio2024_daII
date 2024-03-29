using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.WebApi.Controllers;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using GestionEdificios.Domain;
using Moq;


namespace GestionEdificios.WebApi.Tests
{
    [TestFixture]
    public class TestsAdministradorController
    {
        private Mock<IAdministradorLogica> mockWebApi;
        private AdministradoresController controller;
        private AdministradorDto? adminDto;
        private Administrador admin;

        [SetUp]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IAdministradorLogica>(MockBehavior.Strict);
            adminDto = new AdministradorDto()
            {
                Nombre = "Nombre Admin",
                Apellido = "Apellido Admin",
                Email = "admin@test.com",
                Contraseña = "password"
            };

            admin = new Administrador()
            {
                Id = 1,
                Nombre = adminDto.Nombre,
                Apellido = adminDto.Apellido,
                Email = adminDto.Email,
                Contraseña = adminDto.Contraseña
            };
        }

        [TearDown]
        public void Teardown()
        {
            controller.Dispose();
            controller = null;
        }

        [Test]
        public void TestAgreagarAdminInvalido()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new AdministradoresController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOf<BadRequestObjectResult>(resultado);
        }

        [Test]
        public void TestAgregarAdminOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Administrador>())).Returns(admin);
            controller = new AdministradoresController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(adminDto);
            var dto = resultado.Value as AdministradorDto;
            mockWebApi.VerifyAll();
            Assert.That(dto.Nombre, Is.EqualTo(admin.Nombre));
            Assert.That(dto.Apellido, Is.EqualTo(admin.Apellido));
            Assert.That(dto.Email, Is.EqualTo(admin.Email));
            Assert.That(dto.Contraseña, Is.EqualTo(admin.Contraseña));
            Assert.That(resultado.StatusCode, Is.EqualTo(201));
        }

        [Test]
        public void TestAgregarAdminConEmailExistente()
        {
            AdministradorDto adminDto2 = new AdministradorDto()
            {
                Nombre = "Nombre Admin",
                Apellido = "Apellido Admin",
                Email = "admin@test.com",
                Contraseña = "password"
            };

            Administrador admin2 = new Administrador()
            {
                Id = 1,
                Nombre = adminDto2.Nombre,
                Apellido = adminDto2.Apellido,
                Email = adminDto2.Email,
                Contraseña = adminDto2.Contraseña
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<Administrador>())).Throws(new Exception());
            controller = new AdministradoresController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(adminDto2);
            mockWebApi.VerifyAll();
            Assert.That(resultado.StatusCode, Is.EqualTo(400));
        }
    }
}
