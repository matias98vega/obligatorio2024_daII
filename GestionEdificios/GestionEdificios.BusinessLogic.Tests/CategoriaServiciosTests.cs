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
    public class CategoriaServiciosTests
    {
        private ICategoriaServicioLogica categoriaLogica;
        private Mock<ICategoriaServicioRepositorio> mockRepositorio;

        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<ICategoriaServicioRepositorio>(MockBehavior.Strict);
        }

        /****** Crear ******/
        [TestMethod]
        public void TestCrearCategoriaOK()
        {
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = 1,
                Nombre = "Plomeria",
            };

            mockRepositorio.Setup(m => m.Agregar(It.IsAny<CategoriaServicio>()));
            mockRepositorio.Setup(m => m.Existe(categoria)).Returns(false);
            mockRepositorio.Setup(m => m.Salvar());

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaCreada = categoriaLogica.Agregar(categoria);

            mockRepositorio.VerifyAll();

            Assert.AreEqual(categoria.Id, categoriaCreada.Id);
            Assert.AreEqual(categoria.Nombre, categoriaCreada.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoriaExcepcionDB))]
        public void TestAgregarCategoriaVacia()
        {
            mockRepositorio.Setup(m => m.Agregar(null)).Throws(new ExcepcionDB("", new CategoriaExcepcionDB("")));

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaCreada = categoriaLogica.Agregar(null);

            mockRepositorio.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(CategoriaExcepcionDatos))]
        public void TestCrearCategoriaNombreVacio()
        {
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = 1,
                Nombre = "",
            };


            mockRepositorio.Setup(m => m.Agregar(It.IsAny<CategoriaServicio>()));
            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaCreada = categoriaLogica.Agregar(categoria);
        }

        /***************/

        /****** Borrar *******/
        [TestMethod]
        public void TestBorrarCategoriaPorId()
        {
            var id = 1;
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = id,
                Nombre = "Plomeria",
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(categoria);
            mockRepositorio.Setup(m => m.Borrar(categoria));
            mockRepositorio.Setup(m => m.Salvar());
            mockRepositorio.Setup(m => m.Existe(categoria)).Returns(false);

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            categoriaLogica.Eliminar(id);

            Assert.AreEqual(categoriaLogica.Existe(categoria), false);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoriaExcepcionDB))]
        public void TestBorrarCategoriaConIdIncorrecto()
        {
            var id = 1;
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = id,
                Nombre = "",
            };

            mockRepositorio.Setup(m => m.Obtener(id))
                            .Throws(new ExcepcionDB("", new CategoriaExcepcionDB("")));
            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            categoriaLogica.Eliminar(id);
        }

        /******/

        /***** Actualizar *****/
        [TestMethod]
        public void TestActualizarCategoriaPorId()
        {
            var id = 1;
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = id,
                Nombre = "Plomeria",
            };

            CategoriaServicio categoriaModificada = new CategoriaServicio()
            {
               Nombre = "Sanitaria",
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Returns(categoria);
            mockRepositorio.Setup(m => m.Obtener(categoriaModificada.Id)).Returns((CategoriaServicio)null);
            mockRepositorio.Setup(m => m.Actualizar(categoria));
            mockRepositorio.Setup(m => m.Salvar());

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaRetornada = categoriaLogica.Actualizar(id, categoriaModificada);

            mockRepositorio.VerifyAll();
            Assert.AreEqual(categoria, categoriaRetornada);
        }


        [TestMethod]
        [ExpectedException(typeof(CategoriaExcepcionDB))]
        public void TestActualizarCategoriaInvalida()
        {
            var id = 1;
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = id,
                Nombre = "Plomeria",
            };

            CategoriaServicio modificada = new CategoriaServicio()
            {
               Nombre = "Sanitaria",
            };

            mockRepositorio.Setup(m => m.Obtener(id)).Throws(new ExcepcionDB("", new CategoriaExcepcionDB("")));
            mockRepositorio.Setup(m => m.Actualizar(categoria));

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaModificada = categoriaLogica.Actualizar(id, modificada);
        }

        [TestMethod]
        [ExpectedException(typeof(CategoriaExcepcionDatos))]
        public void TestActualizarCategoriaConNombreVacio()
        {
            var id = 1;
            CategoriaServicio categoria = new CategoriaServicio()
            {
                Id = id,
                Nombre = "Plomeria",
            };

            var modificadoId = 2;
            CategoriaServicio modificada = new CategoriaServicio()
            {
                Id = modificadoId,
                Nombre = "",
            };


            mockRepositorio.Setup(m => m.Obtener(id)).Returns(categoria);
            mockRepositorio.Setup(m => m.Obtener(modificada.Id)).Returns((CategoriaServicio)null);
            mockRepositorio.Setup(m => m.Actualizar(categoria));

            categoriaLogica = new CategoriaServicioLogica(mockRepositorio.Object);
            CategoriaServicio categoriaModificada = categoriaLogica.Actualizar(id, modificada);
        }



        /*******************/
    }
}
