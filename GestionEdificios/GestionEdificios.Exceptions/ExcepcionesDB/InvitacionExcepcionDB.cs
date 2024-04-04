using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesDB
{
    public class InvitacionExcepcionDB : Exception
    {
        public InvitacionExcepcionDB(string error) : base(error)
        {

        }
    }
}
