using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IInvitacionLogica
    {
        Invitacion Agregar(Invitacion invitacion);
        Invitacion Obtener(int Id);
        IEnumerable<Invitacion> ObtenerTodas();
        void Eliminar(int Id);
        Invitacion Actualizar(int id, Invitacion modificada);
        IEnumerable<Invitacion> ObtenerInvitacionesPorEncargado(int id);
    }
}
