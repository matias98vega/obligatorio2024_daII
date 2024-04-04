using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.DataAccess.Interfaces
{
    public interface IRepositorio<T>
    {
        void Agregar(T entity);
        IEnumerable<T> ObtenerTodos();
        void Salvar();
        void Borrar(T entity);
        void Actualizar(T entity);
        bool Existe(T entity);
    }

}
