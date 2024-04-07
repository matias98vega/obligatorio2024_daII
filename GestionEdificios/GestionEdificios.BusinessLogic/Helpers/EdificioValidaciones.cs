using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
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
        }
    }
}
