using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.DataAccess.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Usuario Obtener(int id);

        Usuario ObtenerPorEmail(string email);
    }
}
