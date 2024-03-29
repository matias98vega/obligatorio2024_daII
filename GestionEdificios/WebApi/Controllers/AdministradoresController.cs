using GestionEdificios.BusinessLogic.Interfaces;
using GestionEdificios.Domain;
using GestionEdificios.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestionEdificios.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradoresController : Controller
    {
        private IAdministradorLogica admins;
        public AdministradoresController(IAdministradorLogica administradores) : base()
        {
            this.admins = administradores;
        }

        [HttpPost]
        public IActionResult Post([FromBody]AdministradorDto administradorDto)
        {
            try
            {
                Administrador admin = admins.Agregar(AdministradorDto.ToEntity(administradorDto));
                return CreatedAtAction(
                            "Get",
                            routeValues: new { id = admin.Id },
                            value: AdministradorDto.ToModel(admin)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<AdministradorDto>() 
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
            IEnumerable<Administrador> adminsResultado = admins.ObtenerTodos();
            var respuesta = new ModeloRespuesta<IEnumerable<AdministradorDto>>()
            {
                Codigo = 200,
                Contenido = AdministradorDto.ToModel(adminsResultado),
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
                Administrador admin = admins.Obtener(id);
                var respuesta = new ModeloRespuesta<AdministradorDto>()
                {
                    Contenido = AdministradorDto.ToModel(admin),
                    Codigo = 200,
                    Mensaje = "Se muestra información del administrador."
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<AdministradorDto>()
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
                var respuesta = new ModeloRespuesta<AdministradorDto>()
                {
                    Mensaje = "Administrador eliminado con éxito.",
                    Codigo = 200
                };
                return Ok(respuesta);
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<AdministradorDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]AdministradorDto administradorDto)
        {
            try
            {
                Administrador adminActualizado = admins.Actualizar(id, AdministradorDto.ToEntity(administradorDto));
                return CreatedAtAction(
                            "Put",
                            new { id = adminActualizado.Id },
                            AdministradorDto.ToModel(adminActualizado)
                            );
            }
            catch (Exception e)
            {
                var respuesta = new ModeloRespuesta<AdministradorDto>()
                {
                    Mensaje = e.Message,
                    Codigo = 400
                };
                return BadRequest(respuesta);
            }
        }
    }
}
