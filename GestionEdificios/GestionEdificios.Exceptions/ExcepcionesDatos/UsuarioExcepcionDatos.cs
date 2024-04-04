using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDatos
{
    public class UsuarioExcepcionDatos : Exception
    {
        public UsuarioExcepcionDatos(string error) : base(error)
        {

        }
    }
}
