using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class InvitacionValidaciones
    {
        private IInvitacionRepositorio repositorio;

        private IUsuarioRepositorio usuarioRepositorio;
        public InvitacionValidaciones(IInvitacionRepositorio repositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            this.repositorio = repositorio;
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public void ValidarInvitacion(Invitacion invitacion)
        {
            if (invitacion == null)
            {
                throw new InvitacionExcepcionDB("La invitación esta vacía.");
            }
            if (TextoInvalido(invitacion.Email) || invitacion.Nombre == "" || invitacion.FechaLimite == DateTime.MinValue)
            {
                throw new ServicioExcepcionDatos("Los atributos de la invitación no pueden estar vacios.");
            }
        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }

        public void ValidarUsuario(int usuarioId)
        {
            Usuario usuario = usuarioRepositorio.Obtener(usuarioId);
            if (usuario == null)
            {
                throw new UsuarioNoEncontradoExcepcion("El usuario encargado de la invitación no existe.");
            }

        }

        public void InvitacionYaExiste(Invitacion invitacion)
        {
            if (repositorio.Existe(invitacion))
            {
                throw new InvitacionExisteExcepcion("El servicio ya existe en el sistema.");
            }
        }

        public void InvitacionNoExiste(Invitacion invitacion)
        {
            if (repositorio.Existe(invitacion))
            {
                throw new InvitacionNoExiste("El servicio no existe en el sistema.");
            }
        }

    }
}
