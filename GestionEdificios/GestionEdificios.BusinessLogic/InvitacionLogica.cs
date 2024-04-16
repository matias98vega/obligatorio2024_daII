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
        private IUsuarioRepositorio usuarios;
        private InvitacionValidaciones validaciones;

        public InvitacionLogica(IInvitacionRepositorio repositorio, IUsuarioRepositorio repositorioUsuario)
        {
            this.invitaciones = repositorio;
            this.usuarios = repositorioUsuario;
            this.validaciones = new InvitacionValidaciones(repositorio, repositorioUsuario);
        }

        public Invitacion Actualizar(int id, Invitacion modificada)
        {
            throw new NotImplementedException();
        }

        public Invitacion Agregar(Invitacion invitacion)
        {
            validaciones.ValidarInvitacion(invitacion);
            validaciones.InvitacionYaExiste(invitacion);
            validaciones.ValidarUsuario(invitacion.Encargado);
            invitaciones.Agregar(invitacion);
            invitaciones.Salvar();
            return invitacion;

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
