using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
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
        public CategoriaServicio Actualizar(int id, CategoriaServicio modificada)
        {
            try
            {
                CategoriaServicio categoriaVieja = Obtener(id);
                validaciones.ValidarCategoria(modificada);
                //validaciones.CategoriaYaExiste(modificada);
                categoriaVieja.Actualizar(modificada);
                categorias.Actualizar(categoriaVieja);
                categorias.Salvar();
                return categoriaVieja;
            }
            catch (ExcepcionDB e)
            {
                throw new CategoriaExcepcionDB(e.Message);
            }
        }

        public CategoriaServicio Agregar(CategoriaServicio categoria)
        {
            validaciones.ValidarCategoria(categoria);
            validaciones.CategoriaYaExiste(categoria);
            categorias.Agregar(categoria);
            categorias.Salvar();
            return categoria;
        }

        public void Eliminar(int Id)
        {
            try
            {
                CategoriaServicio categoria = Obtener(Id);
                categorias.Borrar(categoria);
                categorias.Salvar();
            }
            catch (ExcepcionDB e)
            {
                throw new CategoriaExcepcionDB(e.Message);
            }
        }

        public bool Existe(CategoriaServicio categoria)
        {
            return categorias.Existe(categoria);
        }

        public CategoriaServicio Obtener(int Id)
        {

            try
            {
                return categorias.Obtener(Id);
            }
            catch (ExcepcionDB e)
            {
                throw new CategoriaExcepcionDB(e.Message);
            }
        }

        public IEnumerable<CategoriaServicio> ObtenerTodas()
        {
            throw new NotImplementedException();
        }
    }
}
