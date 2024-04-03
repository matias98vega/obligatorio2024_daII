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
    public class TestsEdificioController
    {
        private Mock<IEdificioLogica> mockWebApi;
        private EdificiosController controller;
        private EdificioDto? edificioDto;
        private Edificio edificio;

        [TestInitialize]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IEdificioLogica>(MockBehavior.Strict);
            edificioDto = new EdificioDto()
            {
                Nombre = "Nombre Edificio",
                Direccion = "Avenida Siempreviva 742",
                Ubicacion = "L:53, L:50",
                Constructora = "Constructora Edificio",
                GastosComunes = 5000
            };

            edificio = new Edificio()
            {
                Id = 1,
                Nombre = edificioDto.Nombre,
                Direccion = edificioDto.Direccion,
                Ubicacion = edificioDto.Ubicacion,
                Constructora = edificioDto.Constructora,
                GastosComunes = edificioDto.GastosComunes
            };
        }

        [TestMethod]
        public void TestAgreagarEdificioInvalido()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new EdificiosController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOfType<BadRequestObjectResult>(resultado);
        }

        [TestMethod]
        public void TestAgregarEdificioOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Edificio>())).Returns(edificio);
            controller = new EdificiosController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(edificioDto);
            var dto = resultado.Value as EdificioDto;
            mockWebApi.VerifyAll();
            Assert.AreEqual(dto.Nombre, edificio.Nombre);
            Assert.AreEqual(dto.Direccion, edificio.Direccion);
            Assert.AreEqual(dto.Ubicacion, edificio.Ubicacion);
            Assert.AreEqual(dto.Constructora, edificio.Constructora);
            Assert.AreEqual(dto.GastosComunes, edificio.GastosComunes);
            Assert.AreEqual(resultado.StatusCode, 201);
        }

        [TestMethod]
        public void TestAgregarEdificioConNombreExistente()
        {
            EdificioDto edificioDto2 = new EdificioDto()
            {
                Nombre = "Nombre Edificio",
                Direccion = "Calle 1243",
                Ubicacion = "L: 123, L:54",
                Constructora = "Otra constructora",
                GastosComunes = 6500
            };

            Edificio edificio2 = new Edificio()
            {
                Id = 1,
                Nombre = edificioDto2.Nombre,
                Direccion = edificioDto2.Direccion,
                Ubicacion = edificioDto2.Ubicacion,
                Constructora = edificioDto2.Constructora,
                GastosComunes = edificioDto2.GastosComunes
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<Edificio>())).Throws(new Exception());
            controller = new EdificiosController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(edificioDto2);
            mockWebApi.VerifyAll();
            Assert.AreEqual(resultado.StatusCode, 400);
        }
    }
}