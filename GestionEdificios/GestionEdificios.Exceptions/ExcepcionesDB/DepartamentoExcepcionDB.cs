using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDB
{
    public class DepartamentoExcepcionDB : Exception
    {
        public DepartamentoExcepcionDB(string error) : base(error)
        {

        }
    }
}
