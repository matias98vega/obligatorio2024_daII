using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : Controller
    {
        private IDepartamentoLogica departamentos;
        public DepartamentosController(IDepartamentoLogica departamentos) : base()
        {
            this.departamentos = departamentos;
        }

        [HttpPost]
        public IActionResult Post([FromBody] DepartamentoDto departamentoDto)
        {
            try
            {
                Departamento departamento = departamentos.Agregar(DepartamentoDto.ToEntity(departamentoDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = departamento.Id },
                            value: DepartamentoDto.ToModel(departamento)
                    );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
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
            IEnumerable<Departamento> departamentosResultado = departamentos.ObtenerTodos();
            var respuesta = new ModeloRespuesta<IEnumerable<DepartamentoDto>>()
            {
                Codigo = 200,
                Contenido = DepartamentoDto.ToModel(departamentosResultado),
                Mensaje = "Se muestran todos los departamentos."
            };

            if (departamentosResultado.Count() == 0)
            {
                respuesta.Mensaje = "No hay departamentos registrados aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                Departamento departamento = departamentos.Obtener(id);
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
                {
                    Contenido = DepartamentoDto.ToModel(departamento),
                    Codigo = 200,
                    Mensaje = "Se muestra información del departamento."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
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
                departamentos.Eliminar(id);
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
                {
                    Mensaje = "Departamento eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DepartamentoDto departamentoDto)
        {
            try
            {
                Departamento departamentoActualizado = departamentos.Actualizar(id, DepartamentoDto.ToEntity(departamentoDto));
                return CreatedAtAction(
                            "Put",
                            new { id = departamentoActualizado.Id },
                            DepartamentoDto.ToModel(departamentoActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<DepartamentoDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
