using GestionEdificios.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class InvitacionValidaciones
    {
        IInvitacionRepositorio repositorio;
        public InvitacionValidaciones(IInvitacionRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }
    }
}
