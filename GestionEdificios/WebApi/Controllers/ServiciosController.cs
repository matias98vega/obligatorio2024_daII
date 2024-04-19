using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : Controller
    {
        private IServicioLogica servicios;
        public ServiciosController(IServicioLogica servicios)
        {
            this.servicios = servicios;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ServicioDto servicioDto)
        {
            try
            {
                ServicioDto servicio = ServicioDto.ToModel(servicios.Agregar(ServicioDto.ToEntity(servicioDto)));
                return base.CreatedAtAction(
                            "Get".ToString(),
                            routeValues: new { id = servicio.Id },
                            value: servicio
                    );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
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
            IEnumerable<Servicio> serviciosResultado = servicios.ObtenerTodos();
            var respuesta = new ModeloRespuesta<IEnumerable<ServicioDto>>()
            {
                Codigo = 200,
                Contenido = ServicioDto.ToModel(serviciosResultado),
                Mensaje = "Se muestran todos los servicios."
            };

            if (serviciosResultado.Count() == 0)
            {
                respuesta.Mensaje = "No hay servicios registrados aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Servicio servicio = servicios.Obtener(id);
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Contenido = ServicioDto.ToModel(servicio),
                    Codigo = 200,
                    Mensaje = "Se muestra información del servicio."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
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
                servicios.Eliminar(id);
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Mensaje = "Servicio eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicioDto servicioDto)
        {
            try
            {
                Servicio servicioActualizado = servicios.Actualizar(id, ServicioDto.ToEntity(servicioDto));
                return CreatedAtAction(
                            "Put",
                            new { id = servicioActualizado.Id },
                            ServicioDto.ToModel(servicioActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpGet("solicitudes")]
        public IActionResult ObtenerSolicitudesPorCategoria([FromQuery] int id)
        {
            try
            {
                IEnumerable<Servicio> solicitudesCategoria = servicios.ObtenerSolicitudesPorCategoria(id);

                var respuesta = new ModeloRespuesta<IEnumerable<ServicioDto>>()
                {
                    Contenido = ServicioDto.ToModel(solicitudesCategoria),
                    Codigo = 200,
                    Mensaje = "Se muestra información de las solicitudes para dicha categoría."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}/asignar")]
        public IActionResult AsignarSolicitud(int id, [FromQuery] int usuarioId)
        {
            try
            {
                Servicio servicioModificado = servicios.AsignarSolicitud(id, usuarioId);

                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Contenido = ServicioDto.ToModel(servicioModificado),
                    Codigo = 200,
                    Mensaje = "Se asignó personal de mantenimiento correctamente."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<ServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
