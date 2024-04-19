using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public int EdificioId { get; set; }
        public int Piso { get; set; }
        public int Numero { get; set; }
        public bool ConTerraza { get; set; }
        public int CantidadBaños { get; set; }
        public int CantidadCuartos { get; set; }
        public virtual Dueño Dueño { get; set; }

        public Departamento(int id, int piso, bool conTerraza, int cantidadBaños, int cantidadCuartos, Dueño dueño)
        {
            Id = id;
            Piso = piso;
            ConTerraza = conTerraza;
            CantidadBaños = cantidadBaños;
            CantidadCuartos = cantidadCuartos;
            Dueño = dueño;
        }

        public Departamento(int id, int piso, bool conTerraza, int cantidadBaños, int cantidadCuartos)
        {
            Id = id;
            Piso = piso;
            ConTerraza = conTerraza;
            CantidadBaños = cantidadBaños;
            CantidadCuartos = cantidadCuartos;
        }

        public Departamento Actualizar(Departamento modificado)
        {
            this.Piso = modificado.Piso;
            this.Numero = modificado.Numero;
            this.ConTerraza = modificado.ConTerraza;
            this.CantidadBaños = modificado.CantidadBaños;
            this.CantidadCuartos = modificado.CantidadCuartos;
            return this;
        }
    }
}
