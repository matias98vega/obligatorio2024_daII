using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class InvitacionLogica : IInvitacionLogica
    {
        private IInvitacionRepositorio invitaciones;
        private InvitacionValidaciones validaciones;

        public InvitacionLogica(IInvitacionRepositorio repositorio)
        {
            this.invitaciones = repositorio;
            this.validaciones = new InvitacionValidaciones(repositorio);
        }
        public Invitacion Actualizar(int id, Invitacion modificada)
        {
            throw new NotImplementedException();
        }

        public Invitacion Agregar(Invitacion invitacion)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Invitacion Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitacion> ObtenerInvitacionesPorEncargado(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitacion> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}
