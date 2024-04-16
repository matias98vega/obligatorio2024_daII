using GestionEdificios.Domain.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class Invitacion
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaLimite { get; set; }
        public EstadosInvitaciones Estado { get; set; }
        public virtual Usuario Encargado { get; set; }

        public Invitacion Actualizar(Invitacion invitacion)
        {
            this.Email = invitacion.Email;
            this.Nombre = invitacion.Nombre;
            this.FechaLimite = invitacion.FechaLimite;
            this.Estado = invitacion.Estado;
            this.Encargado = invitacion.Encargado;

            return this;
        }
    }
}
