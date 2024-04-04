using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDB
{
    public class ServicioExcepcionDB : Exception
    { 
        public ServicioExcepcionDB(string error) : base(error)
        {

        }
    }
}
