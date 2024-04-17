using GestionEdificios.Domain;

namespace GestionEdificios.WebApi.DTOs
{
    public class EdificioDto : ModeloDto<Edificio, EdificioDto>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }
        public Constructora Constructora { get; set; }
        public int GastosComunes { get; set; }
        public EdificioDto() { }
        public EdificioDto(Edificio entidad)
        {
            SetModel(entidad);
        }
        public override Edificio ToEntity() => new Edificio()
        {
            Id = this.Id,
            Nombre = this.Nombre,
            Direccion = this.Direccion,
            Ubicacion = this.Ubicacion,
            Constructora = this.Constructora,
            GastosComunes = this.GastosComunes
        };

        protected override EdificioDto SetModel(Edificio entidad)
        {
            Id = entidad.Id;
            Nombre = entidad.Nombre;
            Direccion = entidad.Direccion;
            Ubicacion = entidad.Ubicacion;
            Constructora = entidad.Constructora;
            GastosComunes = entidad.GastosComunes;

            return this;
        }
    }
}
