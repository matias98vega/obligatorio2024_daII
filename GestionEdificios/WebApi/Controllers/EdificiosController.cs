using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EdificiosController : Controller
    {
        private IEdificioLogica edificios;
        public EdificiosController(IEdificioLogica edificios) : base()
        {
            this.edificios = edificios;
        }

        [HttpPost]
        public IActionResult Post([FromBody]EdificioDto edificioDto)
        {
            try
            {
                Edificio edificio = edificios.Agregar(EdificioDto.ToEntity(edificioDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = edificio.Id },
                            value: EdificioDto.ToModel(edificio)
                    );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<EdificioDto>()
                {
                    Codigo = 400,
                    Mensaje = e.Message
                };
                return BadRequest( respuesta );
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Edificio> edificiosResultado = edificios.ObtenerTodos();
            var respuesta = new ModeloRespuesta<IEnumerable<EdificioDto>>()
            {
                Codigo = 200,
                Contenido = EdificioDto.ToModel(edificiosResultado),
                Mensaje = "Se muestran todos los edificios."
            };

            if (edificiosResultado.Count() == 0)
            {
                respuesta.Mensaje = "No hay edificios registrados aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Edificio edificio = edificios.Obtener(id);
                var respuesta = new ModeloRespuesta<EdificioDto>()
                {
                    Contenido = EdificioDto.ToModel(edificio),
                    Codigo = 200,
                    Mensaje = "Se muestra información del edificio."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<EdificioDto>()
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
                edificios.Eliminar(id);
                var respuesta = new ModeloRespuesta<EdificioDto>()
                {
                    Mensaje = "Edificio eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<EdificioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EdificioDto edificioDto)
        {
            try
            {
                Edificio edificioActualizado = edificios.Actualizar(id, EdificioDto.ToEntity(edificioDto));
                return CreatedAtAction(
                            "Put",
                            new { id = edificioActualizado.Id },
                            EdificioDto.ToModel(edificioActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<EdificioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
