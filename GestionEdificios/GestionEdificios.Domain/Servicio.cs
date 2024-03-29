using GestionEdificios.Domain.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public CategoriaServicio Categoria { get; set; }
        public EstadosServicios Estado { get; set; }
        public Administrador UsuarioMantenimiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CostoTotal { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
