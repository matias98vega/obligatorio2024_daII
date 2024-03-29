using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;
using GestionEdificios.WebApi.Controllers;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.WebApi.Tests
{
    [TestFixture]
    public class TestsInvitacionController
    {
        private Mock<IInvitacionLogica> mockWebApi;
        private InvitacionesController controller;
        private InvitacionDto? invitacionDto;
        private Invitacion invitacion;

        [SetUp]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IInvitacionLogica>(MockBehavior.Strict);
            invitacionDto = new InvitacionDto()
            {
                Email = "invitacion@test.com",
                Nombre = "Nombre invitado",
                FechaLimite = DateTime.Today.AddDays(10),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new AdministradorDto()
            };

            invitacion = new Invitacion()
            {
                Id = 1,
                Email = invitacionDto.Email,
                Nombre = invitacionDto.Nombre,
                FechaLimite = invitacionDto.FechaLimite,
                Estado = invitacionDto.Estado,
                Encargado = AdministradorDto.ToEntity(invitacionDto.Encargado)
            };
        }
        [TearDown]
        public void Teardown()
        {
            controller.Dispose();
            controller = null;
        }

        [Test]
        public void TestAgreagarInvitacionInvalida()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new InvitacionesController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOf<BadRequestObjectResult>(resultado);
        }

        [Test]
        public void TestAgregarInvitacionOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Invitacion>())).Returns(invitacion);
            controller = new InvitacionesController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(invitacionDto);
            var dto = resultado.Value as InvitacionDto;
            mockWebApi.VerifyAll();
            Assert.That(dto.Email, Is.EqualTo(invitacion.Email));
            Assert.That(dto.Nombre, Is.EqualTo(invitacion.Nombre));
            Assert.That(dto.FechaLimite, Is.EqualTo(invitacion.FechaLimite));
            Assert.That(dto.Estado, Is.EqualTo(invitacion.Estado));
            Assert.That(dto.Encargado.Id, Is.EqualTo(invitacion.Encargado.Id));
        }

        [Test]
        public void TestAgregarEdificioConNombreExistente()
        {
            InvitacionDto invitacionDto2 = new InvitacionDto()
            {
                Email = "invitacion@test.com",
                Nombre = "Nombre invitado",
                FechaLimite = DateTime.Today.AddDays(10),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new AdministradorDto()
            };

            Invitacion invitacion2 = new Invitacion()
            {
                Id = 1,
                Email = invitacionDto2.Email,
                Nombre = invitacionDto2.Nombre,
                FechaLimite = invitacionDto2.FechaLimite,
                Estado = invitacionDto2.Estado,
                Encargado = AdministradorDto.ToEntity(invitacionDto2.Encargado)
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<Invitacion>())).Throws(new Exception());
            controller = new InvitacionesController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(invitacionDto2);
            mockWebApi.VerifyAll();
            Assert.That(resultado.StatusCode, Is.EqualTo(400));
        }
    }
}
