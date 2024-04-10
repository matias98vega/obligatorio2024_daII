using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Tests
{
    [TestClass]
    public class DepartamentoTests
    {
        private IDepartamentoLogica departamentoLogica;
        private Mock<IDepartamentoRepositorio> mockRepositorio;
        private Dueño dueño;
        private Departamento departamento;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IDepartamentoRepositorio>(MockBehavior.Strict);
            //edificio = new Edificio() { Id = 1, Nombre = "Constructora 1" };

            this.departamento = new Departamento()
            {
                Piso = 1,
                Numero = 1,
                ConTerraza = true,
                CantidadBaños = 1,
                CantidadCuartos = 3,
                Dueño = dueño
            };
        }

        [TestMethod]
        public void TestCrearDepartamentoOk()
        {

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<Departamento>()));
            //mockRepositorio.Setup(m => m.Existe(It.IsAny<Edificio>())).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            departamentoLogica = new DepartamentoLogica(mockRepositorio.Object);
            Departamento departamentoCreado = departamentoLogica.Agregar(this.departamento);

            Assert.AreEqual(departamento.Piso, departamentoCreado.Piso);
            Assert.AreEqual(departamento.Numero, departamentoCreado.Numero);
        }
    }
}
