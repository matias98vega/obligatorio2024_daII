using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDatos
{
    public class BaseDeDatosExcepcion : Exception
    {
        public BaseDeDatosExcepcion(string error, Exception excepcion) : base(error, excepcion) { }
    }
}
