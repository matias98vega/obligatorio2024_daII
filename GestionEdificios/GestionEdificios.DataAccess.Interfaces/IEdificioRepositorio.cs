using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.DataAccess.Interfaces
{
    public interface IEdificioRepositorio : IRepositorio<Edificio>
    {
        Edificio Obtener(int id);
    }
}
