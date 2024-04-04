using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasServiciosController : Controller
    {
        private ICategoriaServicioLogica categoriaServicios;
        public CategoriasServiciosController(ICategoriaServicioLogica categoriaServicios) : base()
        {
            this.categoriaServicios = categoriaServicios;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CategoriaServicioDto categoriaServicioDto)
        {
            try
            {
                CategoriaServicio categoriaServicio = categoriaServicios.Agregar(CategoriaServicioDto.ToEntity(categoriaServicioDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = categoriaServicio.Id },
                            value: CategoriaServicioDto.ToModel(categoriaServicio)
                    );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
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
            IEnumerable<CategoriaServicio> categoriasResultado = categoriaServicios.ObtenerTodas();
            var respuesta = new ModeloRespuesta<IEnumerable<CategoriaServicioDto>>()
            {
                Codigo = 200,
                Contenido = CategoriaServicioDto.ToModel(categoriasResultado),
                Mensaje = "Se muestran todas las categorías."
            };

            if (categoriasResultado.Count() == 0)
            {
                respuesta.Mensaje = "No hay categorías registrados aún.";
            }
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                CategoriaServicio categoria = categoriaServicios.Obtener(id);
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
                {
                    Contenido = CategoriaServicioDto.ToModel(categoria),
                    Codigo = 200,
                    Mensaje = "Se muestra información de la categoría."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
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
                categoriaServicios.Eliminar(id);
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
                {
                    Mensaje = "Categoría eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoriaServicioDto categoriaDto)
        {
            try
            {
                CategoriaServicio categoriaActualizado = categoriaServicios.Actualizar(id, CategoriaServicioDto.ToEntity(categoriaDto));
                return CreatedAtAction(
                            "Put",
                            new { id = categoriaActualizado.Id },
                            CategoriaServicioDto.ToModel(categoriaActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<CategoriaServicioDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
