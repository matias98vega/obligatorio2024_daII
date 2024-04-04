using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDatos
{
    public class EdificioExcepcionDatos : Exception
    {
        public EdificioExcepcionDatos(string error) : base(error)
        {

        }
    }
}
