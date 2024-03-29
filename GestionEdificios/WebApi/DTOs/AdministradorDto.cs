using GestionEdificios.Domain;

namespace GestionEdificios.WebApi.DTOs
{
    public class AdministradorDto : ModeloDto<Administrador, AdministradorDto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public AdministradorDto() { }
        public AdministradorDto(Administrador entidad)
        {
            SetModel(entidad);
        }
        public override Administrador ToEntity() => new Administrador()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Apellido = this.Apellido, 
            Email = this.Email,
            Contraseña = this.Contraseña
        };

        protected override AdministradorDto SetModel(Administrador entidad)
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
