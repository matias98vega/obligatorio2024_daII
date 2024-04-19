using GestionEdificios.Domain.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestionEdificios.Domain
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public Roles Rol { get; set; }
        public Usuario() { }

        public Usuario(int id, string nombre, string apellido, string email, string contraseña, Roles rol) { 
            this.Id = id;
            this.Nombre = nombre;   
            this.Apellido = apellido;
            this.Email = email;
            this.Contraseña = contraseña;
            this.Rol = rol;        
        }


        public Usuario(string nombre, string apellido, string email, Roles rol)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Rol = rol;
        }

        public Usuario(int id, string nombre, string apellido, string email, Roles rol)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Email = email;
            this.Rol = rol;
        }

        public Usuario Actualizar(Usuario usuario)
        {            
            this.Nombre = usuario.Nombre;
            this.Apellido = usuario.Apellido;
            this.Email = usuario.Email;
            this.Contraseña = usuario.Contraseña;

            return this;
        }
    }
}
