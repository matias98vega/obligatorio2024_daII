using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class CategoriaNoEncontradaExcepcion : Exception
    {
        public CategoriaNoEncontradaExcepcion(string error) : base(error)
        {

        }
    }
}
