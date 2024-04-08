using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class CategoriaServicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public CategoriaServicio() { }

        public CategoriaServicio(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public CategoriaServicio Actualizar(CategoriaServicio categoria)
        {
            this.Nombre = categoria.Nombre;
            return this;
        }
    }
}
