using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.DataAccess.Interfaces
{
    public interface IServicioRepositorio : IRepositorio<Servicio>
    {
        Servicio Obtener(int id);

        List<Servicio> ObtenerXCategoria(int categoriaId);

        Servicio AsignarSolicitud(int solicitudId, int usuarioId);
    }
}
