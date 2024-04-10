using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class EdificioValidaciones
    {
        private IEdificioRepositorio edificios;
        public EdificioValidaciones(IEdificioRepositorio repositorio)
        {
            this.edificios = repositorio;
        }

        public void ValidarEdificio(Edificio edificio)
        {
            if (edificio == null)
            {
                throw new EdificioExcepcionDB("El edificio está vacio");
            }
            if (TextoInvalido(edificio.Nombre) || TextoInvalido(edificio.Direccion) || TextoInvalido(edificio.Ubicacion) || edificio.Constructora is null)
            {
                throw new EdificioExcepcionDatos("Los atributos del edificio no pueden estar vacios.");
            }
        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }

        public Edificio ValidarSiExisteEdificio(int id)
        {
            Edificio edificio = edificios.Obtener(id);
            if(edificio == null)
            {
                throw new EdificioNoEncontradoExcepcion("No se pudo encontrar el edificio con el id " + id);
            }
            return edificio;
        }

        public void EdificioYaExiste(Edificio edificio)
        {
            if (edificios.Existe(edificio))
            {
                throw new EdificioExisteExcepcion("Ya existe un edificio con el nombre ingresado.");
            }
        }
    }
}
