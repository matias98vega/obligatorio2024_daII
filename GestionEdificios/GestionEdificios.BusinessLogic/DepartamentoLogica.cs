using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class DepartamentoLogica : IDepartamentoLogica
    {
        private IDepartamentoRepositorio departamentos;
        private DepartamentoValidaciones validaciones;

        public DepartamentoLogica(IDepartamentoRepositorio repositorio)
        {
            departamentos = repositorio;
            this.validaciones = new DepartamentoValidaciones(repositorio);
        }
        public Departamento Actualizar(int id, Departamento modificado)
        {
            Departamento departamento = validaciones.ObtenerDepartamento(id);
            validaciones.ValidarDepartamento(modificado);
            departamento.Actualizar(modificado);
            departamentos.Actualizar(departamento);
            return departamento;
        }

        public Departamento Agregar(Departamento departamento)
        {
            validaciones.ValidarDepartamento(departamento);
            validaciones.ValidarSiExisteDepartamento(departamento);
            departamentos.Agregar(departamento);
            departamentos.Salvar();
            return departamento;
        }

        public void Eliminar(int id)
        {
            Departamento departamento = validaciones.ObtenerDepartamento(id);
            departamentos.Borrar(departamento);
            departamentos.Salvar();
        }

        public bool Existe(Departamento departamento)
        {
            return departamentos.Existe(departamento);
        }

        public Departamento Obtener(int id)
        {
            try
            {
                return validaciones.ObtenerDepartamento(id);
            }
            catch(BaseDeDatosExcepcion e)
            {
                throw new DepartamentoExcepcionDatos(e.Message);
            }
        }

        public IEnumerable<Departamento> ObtenerTodos()
        {
            return this.departamentos.ObtenerTodos();
        }
    }
}
