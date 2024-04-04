using GestionEdificios.Domain;
using GestionEdificios.Domain.Enumerados;

namespace GestionEdificios.WebApi.DTOs
{
    public class ServicioDto : ModeloDto<Servicio, ServicioDto>
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public CategoriaServicioDto Categoria { get; set; }
        public EstadosServicios Estado { get; set; }
        public UsuarioDto UsuarioMantenimiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CostoTotal { get; set; }
        public DepartamentoDto Departamento { get; set; }
        public ServicioDto() { }
        public ServicioDto(Servicio entidad)
        {
            SetModel(entidad);
        }

        public override Servicio ToEntity() => new Servicio()
        {
            Id = this.Id,
            Descripcion = this.Descripcion,
            Categoria = CategoriaServicioDto.ToEntity(this.Categoria),
            Estado = this.Estado,
            UsuarioMantenimiento = UsuarioDto.ToEntity(this.UsuarioMantenimiento),
            FechaInicio = this.FechaInicio,
            FechaFin = this.FechaFin,
            CostoTotal = this.CostoTotal,
            Departamento = DepartamentoDto.ToEntity(this.Departamento)
        };

        protected override ServicioDto SetModel(Servicio entidad)
        {
            Id = entidad.Id;
            Descripcion = entidad.Descripcion;
            Categoria = CategoriaServicioDto.ToModel(entidad.Categoria);
            Estado = entidad.Estado;
            UsuarioMantenimiento = UsuarioDto.ToModel(entidad.UsuarioMantenimiento);
            FechaInicio = entidad.FechaInicio;
            FechaFin= entidad.FechaFin;
            CostoTotal = entidad.CostoTotal;
            Departamento = DepartamentoDto.ToModel(entidad.Departamento);

            return this;
        }
    }
}
