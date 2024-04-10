using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class DepartamentoValidaciones
    {
        private IDepartamentoRepositorio departamentos;
        public DepartamentoValidaciones(IDepartamentoRepositorio repositorio)
        {
            this.departamentos = repositorio;
        }

        public void ValidarDepartamento(Departamento departamento)
        {
            if(departamento == null)
            {
                throw new DepartamentoExcepcionDB("El departamento está vacio.");
            }
        }
    }
}
