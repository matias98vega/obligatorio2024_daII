using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class InvitacionNoExiste : Exception
    {
        public InvitacionNoExiste(string error) : base(error)
        {

        }
    }
}
