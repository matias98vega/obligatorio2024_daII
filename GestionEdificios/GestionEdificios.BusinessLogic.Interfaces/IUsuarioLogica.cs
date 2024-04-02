using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IUsuarioLogica
    {
        Usuario Agregar(Usuario admin);
        Usuario Obtener(int Id);
        IEnumerable<Usuario> ObtenerTodos();
        void Eliminar (int Id);
        Usuario Actualizar(int id, Usuario modificado);
    }
}
