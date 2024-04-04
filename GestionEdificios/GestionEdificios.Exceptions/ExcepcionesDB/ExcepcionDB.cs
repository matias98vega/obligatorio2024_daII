using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDB
{
    public class ExcepcionDB : Exception
    {
        public ExcepcionDB(string error, Exception exception) : base(error, exception)
        {

        }
    }
}
