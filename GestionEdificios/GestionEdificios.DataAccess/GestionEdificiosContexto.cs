using GestionEdificios.Domain;
using Microsoft.EntityFrameworkCore;

namespace GestionEdificios.DataAccess
{
    public class GestionEdificiosContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public GestionEdificiosContexto(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurarUsuario(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigurarUsuario(ModelBuilder modelBuilder)
        {
            var usuarioModelo = modelBuilder.Entity<Usuario>();
            usuarioModelo.HasKey(u => u.Id);
            usuarioModelo.Property(u => u.Nombre).IsRequired();
            usuarioModelo.Property(u => u.Apellido).IsRequired();
            usuarioModelo.Property(u => u.Email).IsRequired();
            usuarioModelo.HasIndex(u => u.Email).IsUnique();
            usuarioModelo.Property(u => u.Contraseña).IsRequired();
            usuarioModelo.Property(u => u.Rol).IsRequired();
        }
    }
}
