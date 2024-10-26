using Alumnos.Service.Repositories.Alumno;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.WebApi.Controllers
{
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoServiceRepository _repository;
        public AlumnoController(IAlumnoServiceRepository repository) {
            _repository = repository;
        }

        [HttpGet("api/[controller]")]
        public IActionResult getAlumno([FromQuery]int legajo)
        {
            try
            {
                return Ok(_repository.GetAlumnoNombre(legajo));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("api/[controller]/info")]
        public IActionResult getAlumnoInfo([FromQuery] int legajo)
        {
            try
            {
                return Ok(_repository.GetAlumnoInfo(legajo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
