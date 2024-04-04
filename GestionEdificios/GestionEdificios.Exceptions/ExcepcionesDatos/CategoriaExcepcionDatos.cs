using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDatos
{
    public class CategoriaExcepcionDatos : Exception
    {
        public CategoriaExcepcionDatos(string error) : base(error)
        {

        }
    }
}
