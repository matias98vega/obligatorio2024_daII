using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IDepartamentoLogica
    {
        Departamento Agregar(Departamento departamento);
        Departamento Obtener(int Id);
        IEnumerable<Departamento> ObtenerTodos();
        void Eliminar(int Id);
        Departamento Actualizar(int id, Departamento modificado);
        bool Existe(Departamento departamento);
    }
}
