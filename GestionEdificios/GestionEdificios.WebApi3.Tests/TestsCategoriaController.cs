using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.Controllers;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace GestionEdificios.WebApi3.Tests
{
    [TestClass]
    public class TestsCategoriaController
    {
        private Mock<ICategoriaServicioLogica> mockWebApi;
        private CategoriasServiciosController controller;
        private CategoriaServicioDto? categoriaServicioDto;
        private CategoriaServicio categoriaServicio;

        [TestInitialize]
        public void SetUp()
        {
            this.mockWebApi = new Mock<ICategoriaServicioLogica>(MockBehavior.Strict);
            categoriaServicioDto = new CategoriaServicioDto()
            {
                Nombre = "Nombre Categoria"
            };

            categoriaServicio = new CategoriaServicio()
            {
                Id = 1,
                Nombre = categoriaServicioDto.Nombre,
            };
        }

        [TestMethod]
        public void TestAgreagarCategoriaInvalida()
        {
            mockWebApi.Setup(m => m.Agregar(null)).Throws(new Exception());
            controller = new CategoriasServiciosController(mockWebApi.Object);
            var resultado = controller.Post(null);

            mockWebApi.VerifyAll();
            Assert.IsInstanceOfType<BadRequestObjectResult>(resultado);
        }

        [TestMethod]
        public void TestAgregarCategoriaOk()
        {
            mockWebApi.Setup(m => m.Agregar(It.IsAny<CategoriaServicio>())).Returns(categoriaServicio);
            controller = new CategoriasServiciosController(mockWebApi.Object);

            CreatedAtActionResult resultado = (CreatedAtActionResult)controller.Post(categoriaServicioDto);
            var dto = resultado.Value as CategoriaServicioDto;
            mockWebApi.VerifyAll();
            Assert.AreEqual(dto.Nombre, categoriaServicio.Nombre);
            Assert.AreEqual(resultado.StatusCode, 201);
        }

        [TestMethod]
        public void TestAgregarCategoriaConNombreExistente()
        {
            CategoriaServicioDto categoriaServicioDto2 = new CategoriaServicioDto()
            {
                Nombre = "Nombre Categoria",
            };

            CategoriaServicio categoriaServicio2 = new CategoriaServicio()
            {
                Id = 1,
                Nombre = categoriaServicioDto2.Nombre,
            };

            mockWebApi.Setup(m => m.Agregar(It.IsAny<CategoriaServicio>())).Throws(new Exception());
            controller = new CategoriasServiciosController(mockWebApi.Object);

            BadRequestObjectResult resultado = (BadRequestObjectResult)controller.Post(categoriaServicioDto2);
            mockWebApi.VerifyAll();
            Assert.AreEqual(resultado.StatusCode, 400);
        }
    }
}