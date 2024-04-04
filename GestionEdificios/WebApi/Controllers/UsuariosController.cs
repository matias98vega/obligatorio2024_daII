using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private IUsuarioLogica admins;
        public UsuariosController(IUsuarioLogica administradores) : base()
        {
            this.admins = administradores;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UsuarioDto administradorDto)
        {
            try
            {
                Usuario admin = admins.Agregar(UsuarioDto.ToEntity(administradorDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = admin.Id },
                            value: UsuarioDto.ToModel(admin)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<UsuarioDto>() 
                { 
                    Codigo = 400,
                    Mensaje = e.Message 
                };
                return BadRequest(respuesta);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Usuario> adminsResultado = admins.ObtenerTodos();
            var respuesta = new ModeloRespuesta<IEnumerable<UsuarioDto>>()
            {
                Codigo = 200,
                Contenido = UsuarioDto.ToModel(adminsResultado),
                Mensaje = "Se muestran todos los administradores."
            };

            if(adminsResultado.Count() == 0) 
            {
                respuesta.Mensaje = "No hay administradores registrados aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Usuario admin = admins.Obtener(id);
                var respuesta = new ModeloRespuesta<UsuarioDto>()
                {
                    Contenido = UsuarioDto.ToModel(admin),
                    Codigo = 200,
                    Mensaje = "Se muestra información del administrador."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<UsuarioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                admins.Eliminar(id);
                var respuesta = new ModeloRespuesta<UsuarioDto>()
                {
                    Mensaje = "Administrador eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<UsuarioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UsuarioDto administradorDto)
        {
            try
            {
                Usuario adminActualizado = admins.Actualizar(id, UsuarioDto.ToEntity(administradorDto));
                return CreatedAtAction(
                            "Put",
                            new { id = adminActualizado.Id },
                            UsuarioDto.ToModel(adminActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<UsuarioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
