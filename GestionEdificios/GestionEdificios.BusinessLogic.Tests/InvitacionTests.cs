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
    public class InvitacionTests
    {
        private IInvitacionLogica invitacionLogica;
        private Mock<IInvitacionRepositorio> mockRepositorio;
        
        [TestInitialize]
        public void SetUp()
        {
            mockRepositorio = new Mock<IInvitacionRepositorio>(MockBehavior.Strict);
        }


    }
}
