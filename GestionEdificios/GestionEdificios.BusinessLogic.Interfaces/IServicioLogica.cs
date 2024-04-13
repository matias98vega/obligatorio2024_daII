using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Interfaces
{
    public interface IServicioLogica
    {
        Servicio Agregar(Servicio invitacion);
        Servicio Obtener(int Id);
        IEnumerable<Servicio> ObtenerTodos();
        void Eliminar(int Id);
        Servicio Actualizar(int id, Servicio modificado);
        IEnumerable<Servicio> ObtenerSolicitudesPorCategoria(int CategoriaId);

        Servicio AsignarSolicitud(int solicitudId, int usuarioId);

        bool Existe(Servicio servicio);
    }
}
