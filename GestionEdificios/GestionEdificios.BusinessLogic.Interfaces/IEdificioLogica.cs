using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IEdificioLogica
    {
        Edificio Agregar(Edificio edificio);
        Edificio Obtener(int Id);
        IEnumerable<Edificio> ObtenerTodos();
        void Eliminar(int Id);
        Edificio Actualizar(int id, Edificio modificado);
    }
}
