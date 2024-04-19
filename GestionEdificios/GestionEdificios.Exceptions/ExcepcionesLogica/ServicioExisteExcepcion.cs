using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class ServicioExisteExcepcion : Exception
    {
        public ServicioExisteExcepcion(string error) : base(error)
        {

        }
    }
}
