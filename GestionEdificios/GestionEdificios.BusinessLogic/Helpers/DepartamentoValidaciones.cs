using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
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
            if(departamento.Piso < 0 || departamento.CantidadBaños <= 0 || departamento.CantidadCuartos < 0)
            {
                throw new DepartamentoExcepcionDatos("Los atributos del departamento no pueden ser menores a cero");
            }
            if(departamento.Dueño is null)
            {
                throw new DepartamentoExcepcionDatos("El departamento debe tener un dueño.");
            }
        }

        public void ValidarSiExisteDepartamento(Departamento departamento)
        {
            if (departamentos.BuscarDepartamentoExistente(departamento))
            {
                throw new DepartamentoExisteExcepcion("Ya existe un departamento para dicho edificio, piso y número.");
            }
        }

        internal Departamento ObtenerDepartamento(int id)
        {
            Departamento departamento = departamentos.Obtener(id);
            if (departamento == null)
            {
                throw new DepartamentoNoEncontradoExcepcion("No se pudo encontrar el departamento con el id " + id);
            }
            return departamento;
        }
    }
}
