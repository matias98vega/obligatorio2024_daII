using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDatos
{
    public class ServicioExcepcionDatos : Exception
    {
        public ServicioExcepcionDatos(string error) : base(error)
        {

        }
    }
}
