using System.Collections.Generic;
using System.Linq;
namespace GestionEdificios.WebApi.DTOs
{
    public abstract class ModeloDto<E, M>
        where E : class
        where M : ModeloDto<E, M>, new()
    {
        public static IEnumerable<M> ToModel(IEnumerable<E> entidades)
        {
            return entidades.Select(x => ToModel(x));
        }

        public static M ToModel(E entidad)
        {
            if(entidad == null)
            {
                return null;
            }
            return new M().SetModel(entidad);
        }

        public static IEnumerable<E> ToEntity(ICollection<M> entidad) 
        {
            return entidad.Select(x => ToEntity(x));
        }

        public static E ToEntity(M entidad)
        {
            if(entidad == null)
            {
                return null;
            }
            return entidad.ToEntity();
        }

        public abstract E ToEntity();
        protected abstract M SetModel(E entidad);
    }
}
