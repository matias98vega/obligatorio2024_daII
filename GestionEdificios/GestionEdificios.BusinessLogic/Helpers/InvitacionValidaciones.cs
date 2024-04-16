using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (invitacion.Nombre == "" || invitacion.FechaLimite == DateTime.MinValue )
            {
                throw new InvitacionExcepcionDatos("Los atributos de la invitación no pueden estar vacios.");
            }
            if (EmailInvalido(invitacion.Email))
            {
                throw new InvitacionExcepcionDatos("El email esta vacío o no es correcto, Por favor ingrese otro");
            }
        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }

        public void ValidarUsuario(Usuario encargado)
        {           
            if (encargado == null || encargado.Id == 0 )
            { 
                throw new UsuarioNoEncontradoExcepcion("El usuario encargado de la invitación no existe.");
            }
            if (!usuarioRepositorio.Existe(encargado)) 
            {
                throw new UsuarioNoEncontradoExcepcion("El usuario encargado de la invitación no existe.");
            }

        }

        public void InvitacionYaExiste(Invitacion invitacion)
        {
            if (repositorio.Existe(invitacion))
            {
                throw new InvitacionExisteExcepcion("La invitacion ya existe en el sistema.");
            }
        }

        public void InvitacionExiste(int invitacionId)
        {
            Invitacion invitacion = repositorio.Obtener(invitacionId);
            if (invitacion == null)
            {
                throw new InvitacionNoExiste("La invitacion no existe en el sistema.");
            }
        }

        public bool EmailInvalido(string email)
        {
            return EmailFormatoInvalido(email) || string.IsNullOrWhiteSpace(email);
        }

        public bool EmailFormatoInvalido(string anEmail)
        {
            Regex rex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.CultureInvariant);
            return !(rex.IsMatch(anEmail.ToString(CultureInfo.InvariantCulture)));
        }



    }
}
