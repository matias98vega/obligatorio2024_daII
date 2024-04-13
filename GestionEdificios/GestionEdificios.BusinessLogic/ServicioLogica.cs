using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GestionEdificios.BusinessLogic
{
    public class ServicioLogica : IServicioLogica
    {
        private IServicioRepositorio servicios;
        private ServicioValidaciones validaciones;
        public ServicioLogica(IServicioRepositorio repositorio)
        {
            this.servicios = repositorio;
            this.validaciones = new ServicioValidaciones(repositorio);

        }

        public Servicio Actualizar(int id, Servicio modificado)
        {
            try
            {
                Servicio servicioViejo = Obtener(id);
                validaciones.ValidarServicio(modificado);
                validaciones.ServicioYaExiste(modificado);
                servicioViejo.Actualizar(modificado);
                servicios.Actualizar(servicioViejo);
                servicios.Salvar();
                return servicioViejo;
            }
            catch (ExcepcionDB e)
            {
                throw new ServicioExcepcionDB(e.Message);
            }
        }

        public Servicio Agregar(Servicio invitacion)
        {
            validaciones.ValidarServicio(invitacion);
            validaciones.ServicioYaExiste(invitacion);
            servicios.Agregar(invitacion);
            servicios.Salvar();
            return invitacion;
        }

        public Servicio AsignarSolicitud(int solicitudId, int usuarioId)
        {
            return servicios.AsignarSolicitud(solicitudId, usuarioId);
        }

        public void Eliminar(int Id)
        {
            try
            {
                Servicio servicio = Obtener(Id);
                servicios.Borrar(servicio);
                servicios.Salvar();
            }
            catch (ExcepcionDB e)
            {
                throw new ServicioExcepcionDB(e.Message);
            }
        }

        public bool Existe(Servicio servicio)
        {
            return servicios.Existe(servicio);
        }

        public Servicio Obtener(int Id)
        {
            return servicios.Obtener(Id);
        }

        public IEnumerable<Servicio> ObtenerSolicitudesPorCategoria(int CategoriaId)
        {
           return servicios.ObtenerXCategoria(CategoriaId); 
        }

        public IEnumerable<Servicio> ObtenerTodos()
        {
            return servicios.ObtenerTodos();
        }
    }
}
