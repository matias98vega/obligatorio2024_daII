using GestionEdificios.Domain.Enumerados;
using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class ServicioValidaciones
    {
        private IServicioRepositorio repositorio;
        private IUsuarioRepositorio usuarioRepositorio;

        public ServicioValidaciones(IServicioRepositorio repositorio, IUsuarioRepositorio usuarios)
        {
            this.repositorio = repositorio;
            this.usuarioRepositorio = usuarios;
        }

        public void ValidarServicio(Servicio servicio)
        {
            if (servicio == null)
            {
                throw new ServicioExcepcionDB("El servicio esta vacío.");
            }
            if (TextoInvalido(servicio.Descripcion) || servicio.Categoria.Nombre == "" || servicio.UsuarioMantenimiento.Email  == "" || servicio.FechaInicio == DateTime.MinValue || servicio.FechaFin == DateTime.MinValue || servicio.CostoTotal == 0 || servicio.Departamento.Id == 0)
            {
                throw new ServicioExcepcionDatos("Los atributos del servicio no pueden estar vacios.");
            }
        }

        public void ServicioYaExiste(Servicio servicio)
        {
            if (repositorio.Existe(servicio))
            {
                throw new ServicioExisteExcepcion("El servicio ya existe en el sistema.");
            }
        }

        public void ServicioExiste(int idServicio)
        {
            Servicio servicio = repositorio.Obtener(idServicio);
            if (servicio == null)
            {
                throw new ServicioNoExiste("El servicio a modificar no existe en el sistema.");
            }
        }

        public void ServicioNoExiste(Servicio servicio)
        {
            if (repositorio.Existe(servicio))
            {
                throw new ServicioNoExiste("El servicio no existe en el sistema.");
            }
        }

        public void ValidarUsuario(int usuarioId) 
        {
            Usuario usuario = usuarioRepositorio.Obtener(usuarioId);
            if (usuario == null)
            {
                throw new UsuarioNoEncontradoExcepcion("El usuario de la solicitud no existe.");
            }

        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }

    }
}
