using GestionEdificios.Domain;

namespace GestionEdificios.WebApi.DTOs
{
    public class UsuarioDto : ModeloDto<Usuario, UsuarioDto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public UsuarioDto() { }
        public UsuarioDto(Usuario entidad)
        {
            SetModel(entidad);
        }
        public override Usuario ToEntity() => new Usuario()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Apellido = this.Apellido, 
            Email = this.Email,
            Contraseña = this.Contraseña
        };

        protected override UsuarioDto SetModel(Usuario entidad)
        {
            Id = entidad.Id;
            Nombre = entidad.Nombre;
            Apellido = entidad.Apellido;
            Email = entidad.Email;
            Contraseña = entidad.Contraseña;

            return this;
        }
    }
}
