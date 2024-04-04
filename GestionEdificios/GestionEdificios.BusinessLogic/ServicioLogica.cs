using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionEdificios.BusinessLogic
{
    public class ServicioLogica : IServicioLogica
    {
        public Servicio Actualizar(int id, Servicio modificado)
        {
            throw new NotImplementedException();
        }

        public Servicio Agregar(Servicio invitacion)
        {
            throw new NotImplementedException();
        }

        public Servicio AsignarSolicitud(int solicitudId, int usuarioId)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Servicio Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Servicio> ObtenerSolicitudesPorCategoria(int CategoriaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Servicio> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
