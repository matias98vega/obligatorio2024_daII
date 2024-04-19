﻿using GestionEdificios.BusinessLogic.Helpers;
using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.DataAccess.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.Exceptions.ExcepcionesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEdificios.BusinessLogic
{
    public class InvitacionLogica : IInvitacionLogica
    {
        private IInvitacionRepositorio invitaciones;
        private IUsuarioRepositorio usuarios;
        private InvitacionValidaciones validaciones;

        public InvitacionLogica(IInvitacionRepositorio repositorio, IUsuarioRepositorio repositorioUsuario)
        {
            this.invitaciones = repositorio;
            this.usuarios = repositorioUsuario;
            this.validaciones = new InvitacionValidaciones(repositorio, repositorioUsuario);
        }

        public Invitacion Actualizar(int id, Invitacion modificada)
        {
            try
            {
                Invitacion invitacionVieja = Obtener(id);
                validaciones.ValidarInvitacion(modificada);
                validaciones.InvitacionExiste(id);
                invitacionVieja.Actualizar(modificada);
                invitaciones.Actualizar(invitacionVieja);
                invitaciones.Salvar();
                return invitacionVieja;
            }
            catch (ExcepcionDB e)
            {
                throw new InvitacionExcepcionDB(e.Message);
            }
        }

        public Invitacion Agregar(Invitacion invitacion)
        {
            validaciones.ValidarInvitacion(invitacion);
            validaciones.InvitacionYaExiste(invitacion);
            validaciones.ValidarUsuario(invitacion.Encargado);
            invitaciones.Agregar(invitacion);
            invitaciones.Salvar();
            return invitacion;

        }

        public void Eliminar(int Id)
        {
            try
            {
                Invitacion invitacion = invitaciones.Obtener(Id);
                invitaciones.Borrar(invitacion);
                invitaciones.Salvar();
            }
            catch (ExcepcionDB e)
            {
                throw new InvitacionExcepcionDB(e.Message);
            }
        }

        public Invitacion Obtener(int Id)
        {
            return invitaciones.Obtener(Id);
        }

        public IEnumerable<Invitacion> ObtenerInvitacionesPorEncargado(int id)
        {
            try
            {
                return invitaciones.ObtenerInvitacionesPorEncargado(id);
            }
            catch (ExcepcionDB e)
            {
                throw new UsuarioExcepcionDB(e.Message);
            }
        }

        public IEnumerable<Invitacion> ObtenerTodas()
        {
            return invitaciones.ObtenerTodos();
        }

        public bool Existe(Invitacion invitacion)
        {
            return invitaciones.Existe(invitacion);
        }

    }
}
