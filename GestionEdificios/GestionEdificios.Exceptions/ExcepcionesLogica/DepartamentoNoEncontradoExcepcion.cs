using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Exceptions.ExcepcionesLogica
{
    public class DepartamentoNoEncontradoExcepcion : Exception
    {
        public DepartamentoNoEncontradoExcepcion(string error) : base(error) { }
    }
}
