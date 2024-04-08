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
    public class CategoriaServicioLogica : ICategoriaServicioLogica
    {
        private ICategoriaServicioRepositorio categorias;
        private CategoriaServiciosValidaciones validaciones;
        public CategoriaServicioLogica(ICategoriaServicioRepositorio repositorio)
        {
            this.categorias = repositorio;
            this.validaciones = new CategoriaServiciosValidaciones(repositorio);

        }
        public CategoriaServicio Actualizar(int id, CategoriaServicio modificado)
        {
            throw new NotImplementedException();
        }

        public CategoriaServicio Agregar(CategoriaServicio categoria)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public CategoriaServicio Obtener(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CategoriaServicio> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}
