using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class EdificioLogica : IEdificioLogica
    {
        private IEdificioRepositorio edificios;
        private EdificioValidaciones validaciones;

        public EdificioLogica(IEdificioRepositorio repositorio) 
        {
            edificios = repositorio;
            this.validaciones = new EdificioValidaciones(repositorio);
        }
        public Edificio Actualizar(int id, Edificio modificado)
        {
            Edificio edificio = validaciones.ValidarSiExisteEdificio(id);
            validaciones.ValidarEdificio(modificado);
            edificio.Actualizar(modificado);
            edificios.Actualizar(edificio);
            edificios.Salvar();
            return edificio;

        }

        public Edificio Agregar(Edificio edificio)
        {
            validaciones.ValidarEdificio(edificio);
            validaciones.EdificioYaExiste(edificio);
            edificios.Agregar(edificio);
            edificios.Salvar();
            return edificio;
        }

        public void Eliminar(int id)
        {
            Edificio edificio = validaciones.ValidarSiExisteEdificio(id);
            edificios.Borrar(edificio);
            edificios.Salvar();
        }

        public bool Existe(Edificio edificio)
        {
            return edificios.Existe(edificio);
        }

        public Edificio Obtener(int Id)
        {
            try
            {
                return validaciones.ValidarSiExisteEdificio(Id);
            }
            catch (BaseDeDatosExcepcion e)
            {
                throw new EdificioExcepcionDatos(e.Message);
            }
        }

        public IEnumerable<Edificio> ObtenerTodos()
        {
            return this.edificios.ObtenerTodos();
        }
    }
}
