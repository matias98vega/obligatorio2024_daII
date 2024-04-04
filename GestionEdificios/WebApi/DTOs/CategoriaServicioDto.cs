using GestionEdificios.Domain;

namespace GestionEdificios.WebApi.DTOs
{
    public class CategoriaServicioDto : ModeloDto<CategoriaServicio, CategoriaServicioDto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public CategoriaServicioDto() { }
        public CategoriaServicioDto(CategoriaServicio entidad)
        {
            SetModel(entidad);
        }

        public override CategoriaServicio ToEntity() => new CategoriaServicio()
        {
            Id = this.Id,
            Nombre = this.Nombre
        };

        protected override CategoriaServicioDto SetModel(CategoriaServicio entidad)
        {
            Id = entidad.Id;
            Nombre = entidad.Nombre;

            return this;
        }
    }
}
