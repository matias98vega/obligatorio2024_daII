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

        public EdificioLogica(IEdificioRepositorio repositorio) 
        {
            edificios = repositorio;
        }
        public Edificio Actualizar(int id, Edificio modificado)
        {
            throw new NotImplementedException();
        }

        public Edificio Agregar(Edificio edificio)
        {
            throw new NotImplementedException();
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
