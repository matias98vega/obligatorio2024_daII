using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface ICategoriaServicioLogica
    {
        CategoriaServicio Agregar(CategoriaServicio categoria);
        CategoriaServicio Obtener(int Id);
        IEnumerable<CategoriaServicio> ObtenerTodas();
        void Eliminar(int Id);
        CategoriaServicio Actualizar(int id, CategoriaServicio modificado);
        bool Existe(CategoriaServicio categoria);
    }
}
