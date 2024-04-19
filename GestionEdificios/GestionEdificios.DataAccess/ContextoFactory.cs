using Microsoft.EntityFrameworkCore;

namespace GestionEdificios.DataAccess
{
    public class ContextoFactory
    {
        public static GestionEdificiosContexto GetMemoryContext(string nombreBd)
        {
            var constructor = new DbContextOptionsBuilder<GestionEdificiosContexto>();
            return new GestionEdificiosContexto(GetMemoryConfig(constructor, nombreBd));
        }

        private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder constructor, string nombreBd)
        {
            constructor.UseInMemoryDatabase(nombreBd);
            return constructor.Options;
        }
    }
}
