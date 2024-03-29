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
        public virtual Administrador Encargado { get; set; }
    }
}
