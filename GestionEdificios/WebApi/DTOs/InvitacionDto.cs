using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;

namespace GestionEdificios.WebApi.DTOs
{

    public class InvitacionDto : ModeloDto<Invitacion, InvitacionDto>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaLimite { get; set; }
        public EstadosInvitaciones Estado { get; set; }
        public AdministradorDto Encargado { get; set; }
        public InvitacionDto() { }
        public InvitacionDto(Invitacion entidad)
        {
            SetModel(entidad);
        }
        public override Invitacion ToEntity() => new Invitacion()
        {
            Id = this.Id,
            Email = this.Email,
            Nombre = this.Nombre,
            FechaLimite = this.FechaLimite,
            Estado = this.Estado
        };

        protected override InvitacionDto SetModel(Invitacion entidad)
        {
            Id = entidad.Id;
            Email = entidad.Email;
            Nombre = entidad.Nombre;
            FechaLimite = entidad.FechaLimite;
            Estado = entidad.Estado;
            Encargado = AdministradorDto.ToModel(entidad.Encargado);

            return this;
        }
    }
}
