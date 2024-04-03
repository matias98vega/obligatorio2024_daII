using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.Controllers;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.WebApi3.Tests
{
    [TestClass]
    public class TestsDepartamentoController
    {
        private Mock<IDepartamentoLogica> mockWebApi;
        private DepartamentosController controller;
        private DepartamentoDto? departamentoDto;
        private Departamento departamento;

        [TestInitialize]
        public void SetUp()
        {
            this.mockWebApi = new Mock<IDepartamentoLogica>(MockBehavior.Strict);
            departamentoDto = new DepartamentoDto()
            {
                Piso = 1,
                Numero = 2,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 2
            };

            departamento = new Departamento()
            {
                Id = 1,
                Piso = departamentoDto.Piso,
                Numero = departamentoDto.Numero,
                ConTerraza = departamentoDto.ConTerraza,
                CantidadBaños = departamentoDto.CantidadBaños,
                CantidadCuartos = departamentoDto.CantidadCuartos
            };
        }

        [TestMethod]
        public void TestAgreagarDepartamentoInvalido()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new DepartamentosController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOfType<BadRequestObjectResult>(resultado);
        }

        [TestMethod]
        public void TestAgregarDepartamentoOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<Departamento>())).Returns(departamento);
            controller = new DepartamentosController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(departamentoDto);
            var dto = resultado.Value as DepartamentoDto;
            mockWebApi.VerifyAll();
            Assert.AreEqual(dto.Piso, departamento.Piso);
            Assert.AreEqual(dto.Numero, departamento.Numero);
            Assert.AreEqual(dto.ConTerraza, departamento.ConTerraza);
            Assert.AreEqual(dto.CantidadBaños, departamento.CantidadBaños);
            Assert.AreEqual(dto.CantidadCuartos, departamento.CantidadCuartos);
            Assert.AreEqual(resultado.StatusCode, 201);
        }
    }
}
