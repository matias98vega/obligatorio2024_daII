using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IAdministradorLogica
    {
        Administrador Agregar(Administrador admin);
        Administrador Obtener(int Id);
        IEnumerable<Administrador> ObtenerTodos();
        void Eliminar (int Id);
        Administrador Actualizar(int id, Administrador modificado);
    }
}
