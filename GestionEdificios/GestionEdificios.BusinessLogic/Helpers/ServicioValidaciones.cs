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
        public ServicioValidaciones(IServicioRepositorio repository)
        {
            this.repositorio = repository;
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

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }

    }
}
