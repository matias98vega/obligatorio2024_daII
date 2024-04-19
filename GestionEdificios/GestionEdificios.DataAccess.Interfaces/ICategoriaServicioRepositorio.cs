﻿using GestionEdificios.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.DataAccess.Interfaces
{
    public interface ICategoriaServicioRepositorio : IRepositorio<CategoriaServicio>
    {
        CategoriaServicio Obtener(int id);
    }
}
