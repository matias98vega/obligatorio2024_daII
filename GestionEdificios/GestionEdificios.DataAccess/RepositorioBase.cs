using GestionEdificios.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionEdificios.DataAccess
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T: class
    {
        protected DbContext Contexto { get; set; }

        public void Agregar(T entity)
        {
            Contexto.Set<T>().Add(entity);
        }

        public void Actualizar(T entity)
        {
            Contexto.Entry(entity).State = EntityState.Modified;
        }

        public abstract IEnumerable<T> ObtenerTodos();

        public void Salvar()
        {
            Contexto.SaveChanges();
        }
        public abstract bool Existe(T entity);
        public void Borrar(T entity)
        {
            Contexto.Set<T>().Remove(entity);
        }
    }
}
