using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDatos;
using GestionEdificios.Exceptions.ExcepcionesDB;
using GestionEdificios.Exceptions.ExcepcionesLogica;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic.Helpers
{
    public class CategoriaServiciosValidaciones
    {
        private ICategoriaServicioRepositorio repositorio;

        public CategoriaServiciosValidaciones(ICategoriaServicioRepositorio repository)
        {
            this.repositorio = repository;
        }

        public void ValidarCategoria(CategoriaServicio categoria) {
            if (categoria == null)
            {
                throw new CategoriaExcepcionDB("La categoria esta vacia.");
            }
            if (String.IsNullOrWhiteSpace(categoria.Nombre))
            {
                throw new CategoriaExcepcionDatos("El nombre de la categoria no puede estar vacio");
            }
        }

        public void CategoriaYaExiste(CategoriaServicio categoria)
        {
            if (repositorio.Existe(categoria))
            {
                throw new UsuarioExisteExcepcion("El usuario ya existe en el sistema.");
            }
        }
    }
}
