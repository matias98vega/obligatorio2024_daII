using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using GestionEdificios.Exceptions.ExcepcionesDB;

namespace GestionEdificios.DataAccess
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(DbContext contexto)
        {
            Contexto = contexto;
        }

        public Usuario Obtener(int id)
        {
            try
            {
                return Contexto.Set<Usuario>().FirstOrDefault(x => x.Id == id);
            }
            catch (SqlException e)
            {
                throw new ExcepcionDB("Ocurrió un error al intentar obtener al usuario.", e.InnerException);
            }
        }

        public Usuario ObtenerPorEmail(string email)
        {
            try
            {
                return Contexto.Set<Usuario>().FirstOrDefault(x => x.Email == email);
            }
            catch (SqlException e)
            {
                throw new ExcepcionDB("Ocurrió un error al intentar obtener al usuario.", e.InnerException);
            }
        }


        public override IEnumerable<Usuario> ObtenerTodos()
        {
            return Contexto.Set<Usuario>().ToList();
        }

        public override bool Existe(Usuario admin)
        {
            return Contexto.Set<Usuario>().Any(x => x.Email == admin.Email);
        }
    }
}
