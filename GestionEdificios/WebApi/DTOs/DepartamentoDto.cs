using GestionEdificios.Domain;

namespace GestionEdificios.WebApi.DTOs
{
    public class DepartamentoDto : ModeloDto<Departamento, DepartamentoDto>
    {
        public int Id { get; set; }
        public int Piso { get; set; }
        public bool ConTerraza { get; set; }
        public int CantidadBaños { get; set; }
        public int CantidadCuartos { get; set; }
        public Dueño Dueño { get; set; }
        public DepartamentoDto() { }
        public DepartamentoDto(Departamento entidad)
        {
            SetModel(entidad);
        }

        public override Departamento ToEntity() => new Departamento()
        {
            Id = this.Id,
            Piso = this.Piso,
            ConTerraza = this.ConTerraza,
            CantidadBaños = this.CantidadBaños,
            CantidadCuartos = this.CantidadCuartos,
            Dueño = this.Dueño
        };

        protected override DepartamentoDto SetModel(Departamento entidad)
        {
            Id = entidad.Id;
            Piso = entidad.Piso;
            ConTerraza = entidad.ConTerraza;
            CantidadBaños = entidad.CantidadBaños;
            CantidadCuartos = entidad.CantidadCuartos;
            Dueño = entidad.Dueño;

            return this;
        }
    }
}
