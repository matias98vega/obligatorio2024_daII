using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
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
            if (TextoInvalido(edificio.Nombre) || TextoInvalido(edificio.Direccion) || TextoInvalido(edificio.Ubicacion) || TextoInvalido(edificio.Constructora))
            {
                throw new EdificioExcepcionDatos("Los atributos del edificio no pueden estar vacios.");
            }
        }

        public bool TextoInvalido(string valor)
        {
            return String.IsNullOrWhiteSpace(valor);
        }
    }
}
