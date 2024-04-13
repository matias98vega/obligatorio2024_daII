using GestionEdificios.Domain.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.Domain
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public CategoriaServicio? Categoria { get; set; }
        public EstadosServicios Estado { get; set; }
        public Usuario? UsuarioMantenimiento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int CostoTotal { get; set; }
        public virtual Departamento? Departamento { get; set; }

        public Servicio() { }

        public Servicio(int id, string descripcion, CategoriaServicio categoria, EstadosServicios estado, Usuario usuario, 
            DateTime fechaInicio, DateTime fechaFin, int costoTotal, Departamento depto)
        {
            this.Id = id;
            this.Descripcion = descripcion; 
            this.Categoria = categoria;
            this.Estado = estado;
            this.UsuarioMantenimiento = usuario;
            this.FechaInicio = fechaInicio;
            this.FechaFin = fechaFin;   
            this.CostoTotal = costoTotal;
            this.Departamento = depto;
        }

        public Servicio Actualizar(Servicio servicio)
        {
            this.Descripcion = servicio.Descripcion;
            this.Categoria = servicio.Categoria;
            this.Estado = servicio.Estado;
            this.UsuarioMantenimiento = servicio.UsuarioMantenimiento;
            this.FechaInicio = servicio.FechaInicio;
            this.FechaFin = servicio.FechaFin;
            this.CostoTotal = servicio.CostoTotal;
            this.Departamento = servicio.Departamento;

            return this;
        }
    }
}
