using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitacionesController : Controller
    {
        private IInvitacionLogica invitaciones;
        public InvitacionesController(IInvitacionLogica invitaciones) : base()
        {
            this.invitaciones = invitaciones;
        }

        [HttpPost]
        public IActionResult Post([FromBody] InvitacionDto invitacionDto)
        {
            try
            {
                Invitacion invitacion = invitaciones.Agregar(InvitacionDto.ToEntity(invitacionDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = invitacion.Id },
                            value: InvitacionDto.ToModel(invitacion)
                    );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<InvitacionDto>()
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
            IEnumerable<Invitacion> invitacionesResultado = invitaciones.ObtenerTodas();
            var respuesta = new ModeloRespuesta<IEnumerable<InvitacionDto>>()
            {
                Codigo = 200,
                Contenido = InvitacionDto.ToModel(invitacionesResultado),
                Mensaje = "Se muestran todas las invitaciones."
            };

            if (invitacionesResultado.Count() == 0)
            {
                respuesta.Mensaje = "No hay invitaciones registradas aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Invitacion invitacion = invitaciones.Obtener(id);
                var respuesta = new ModeloRespuesta<InvitacionDto>()
                {
                    Contenido = InvitacionDto.ToModel(invitacion),
                    Codigo = 200,
                    Mensaje = "Se muestra información de la invitación."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<InvitacionDto>()
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
                invitaciones.Eliminar(id);
                var respuesta = new ModeloRespuesta<InvitacionDto>()
                {
                    Mensaje = "Invitación eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<InvitacionDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] InvitacionDto invitacionDto)
        {
            try
            {
                Invitacion invitacionActualizado = invitaciones.Actualizar(id, InvitacionDto.ToEntity(invitacionDto));
                return CreatedAtAction(
                            "Put",
                            new { id = invitacionActualizado.Id },
                            InvitacionDto.ToModel(invitacionActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<InvitacionDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpGet("invitaciones")]
        public IActionResult ObtenerInvitacionesPorEncargado([FromQuery] int id)
        {
            try
            {
                IEnumerable<Invitacion> invitacionesEncargado = invitaciones.ObtenerInvitacionesPorEncargado(id);

                var respuesta = new ModeloRespuesta<IEnumerable<InvitacionDto>>()
                {
                    Contenido = InvitacionDto.ToModel(invitacionesEncargado),
                    Codigo = 200,
                    Mensaje = "Se muestra información de la invitación."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<InvitacionDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

    }
}
