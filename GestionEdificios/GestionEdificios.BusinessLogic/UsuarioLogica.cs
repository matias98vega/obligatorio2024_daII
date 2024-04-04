using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class UsuarioLogica : IUsuarioLogica
    {
        private IUsuarioRepositorio usuarios;
        private UsuarioValidaciones validaciones;
        public UsuarioLogica(IUsuarioRepositorio repositorio) 
        { 
            this.usuarios = repositorio;
            this.validaciones = new UsuarioValidaciones(repositorio);
        
        }
        public Usuario Actualizar(int id, Usuario modificado)
        {
            try
            {
                Usuario usuarioViejo = ObtenerUsuarioPorId(id);
                validaciones.ValidarUsuario(modificado);
                validaciones.ValidarEmailEstaEnUso(usuarioViejo, modificado);
                usuarioViejo.Actualizar(modificado);
                usuarios.Actualizar(usuarioViejo);
                usuarios.Salvar();
                return usuarioViejo;
            }
            catch (ExcepcionDB e)
            {
                throw new UsuarioExcepcionDB(e.Message);
            }
        }

        public Usuario Agregar(Usuario usuario)
        {
            validaciones.ValidarUsuario(usuario);
            validaciones.UsuarioYaExiste(usuario);
            usuarios.Agregar(usuario);
            usuarios.Salvar();
            return usuario;
        }

        public void Eliminar(int Id)
        {
            try
            {
                Usuario usuario = ObtenerUsuarioPorId(Id);
                usuarios.Borrar(usuario);
                usuarios.Salvar();
            }
            catch (ExcepcionDB e)
            {
                throw new UsuarioExcepcionDB(e.Message);
            }
        }

        public Usuario Obtener(int Id)
        {
            try
            {
                return ObtenerUsuarioPorId(Id);
            }
            catch (ExcepcionDB e)
            {
                throw new UsuarioExcepcionDB(e.Message);
            }
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return usuarios.ObtenerTodos();
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario retornado = usuarios.Obtener(id);
            if (retornado == null)
            {
                throw new UsuarioNoEncontradoExcepcion("No se encontro usuario con id: " + id);
            }
            return retornado;
        }
    }
}
