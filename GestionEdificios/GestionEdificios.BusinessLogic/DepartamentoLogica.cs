using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class DepartamentoLogica : IDepartamentoLogica
    {
        public Departamento Actualizar(int id, Departamento modificado)
        {
            throw new NotImplementedException();
        }

        public Departamento Agregar(Departamento departamento)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Departamento Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Departamento> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
