using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class UsuarioEmailYaExisteExcepcion : Exception
    {
        public UsuarioEmailYaExisteExcepcion(string error) : base(error)
        {
        }
    }
}
