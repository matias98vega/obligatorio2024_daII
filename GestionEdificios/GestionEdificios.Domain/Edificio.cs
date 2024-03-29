using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class Edificio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }
        public string Constructora { get; set; }
        public int GastosComunes { get; set; }
        public  ICollection<Departamento> Departamentos { get; set; }

        public Edificio() { }
    }
}
