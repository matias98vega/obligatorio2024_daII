using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
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
    public class UsuarioValidaciones
    {
        private IUsuarioRepositorio repositorio;
        public UsuarioValidaciones(IUsuarioRepositorio repository)
        {
            this.repositorio = repository;
        }

        public void ValidarUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UsuarioExcepcionDB("El usuario esta vacío.");
            }
            if (TextoInvalido(usuario.Nombre) || TextoInvalido(usuario.Apellido) || TextoInvalido(usuario.Contraseña) || string.IsNullOrWhiteSpace(usuario.Rol.ToString()))
            {
                throw new UsuarioExcepcionDatos("Los atributos del usuario no pueden estar vacios.");
            }
            if (EmailInvalido(usuario.Email))
            {
                throw new UsuarioExcepcionDatos("El email no es correcto, Por favor ingrese otro");
            }
        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
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

        public void UsuarioYaExiste(Usuario usuario)
        {
            if (repositorio.Existe(usuario))
            {
                throw new UsuarioExisteExcepcion("El usuario ya existe en el sistema.");
            }
        }

        public void ValidarEmailEstaEnUso(Usuario nuevoUsuario)
        {
            var usuarioExiste = repositorio.ObtenerPorEmail(nuevoUsuario.Email);
            if (usuarioExiste != null)
            {
                throw new UsuarioEmailYaExisteExcepcion("El email ya esta siendo usado por otro usuario.");
            }
        }

    }
}
