using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
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

        public void Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public Edificio Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Edificio> ObtenerTodos()
        {
            throw new NotImplementedException();
        }
    }
}
