using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class EdificioExisteExcepcion : Exception
    {
        public EdificioExisteExcepcion(string error) : base(error) { }
    }
}
