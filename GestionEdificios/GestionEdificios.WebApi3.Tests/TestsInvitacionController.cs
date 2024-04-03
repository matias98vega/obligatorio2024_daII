using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain.Enumerados;
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
    public class TestsInvitacionController
    {
        private Mock<IInvitacionLogica> mockWebApi;
        private InvitacionesController controller;
        private InvitacionDto? invitacionDto;
        private Invitacion invitacion;

        [TestInitialize]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IInvitacionLogica>(MockBehavior.Strict);
            invitacionDto = new InvitacionDto()
            {
                Email = "invitacion@test.com",
                Nombre = "Nombre invitado",
                FechaLimite = DateTime.Today.AddDays(10),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new UsuarioDto()
            };

            invitacion = new Invitacion()
            {
                Id = 1,
                Email = invitacionDto.Email,
                Nombre = invitacionDto.Nombre,
                FechaLimite = invitacionDto.FechaLimite,
                Estado = invitacionDto.Estado,
                Encargado = UsuarioDto.ToEntity(invitacionDto.Encargado)
            };
        }

        [TestMethod]
        public void TestAgreagarInvitacionInvalida()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new InvitacionesController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOfType<BadRequestObjectResult>(resultado);
        }

        [TestMethod]
        public void TestAgregarInvitacionOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Invitacion>())).Returns(invitacion);
            controller = new InvitacionesController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(invitacionDto);
            var dto = resultado.Value as InvitacionDto;
            mockWebApi.VerifyAll();
            Assert.AreEqual(dto.Email, invitacion.Email);
            Assert.AreEqual(dto.Nombre, invitacion.Nombre);
            Assert.AreEqual(dto.FechaLimite, invitacion.FechaLimite);
            Assert.AreEqual(dto.Estado, invitacion.Estado);
            Assert.AreEqual(dto.Encargado.Id, invitacion.Encargado.Id);
        }

        [TestMethod]
        public void TestAgregarEdificioConNombreExistente()
        {
            InvitacionDto invitacionDto2 = new InvitacionDto()
            {
                Email = "invitacion@test.com",
                Nombre = "Nombre invitado",
                FechaLimite = DateTime.Today.AddDays(10),
                Estado = EstadosInvitaciones.Abierta,
                Encargado = new UsuarioDto()
            };

            Invitacion invitacion2 = new Invitacion()
            {
                Id = 1,
                Email = invitacionDto2.Email,
                Nombre = invitacionDto2.Nombre,
                FechaLimite = invitacionDto2.FechaLimite,
                Estado = invitacionDto2.Estado,
                Encargado = UsuarioDto.ToEntity(invitacionDto2.Encargado)
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<Invitacion>())).Throws(new Exception());
            controller = new InvitacionesController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(invitacionDto2);
            mockWebApi.VerifyAll();
            Assert.AreEqual(resultado.StatusCode, 400);
        }
    }
}