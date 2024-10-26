using Alumnos.Service.Repositories.Alumno;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.WebApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoServiceRepository _repository;

        public AlumnoController(IAlumnoServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlumno([FromQuery] int legajo)
        {
            try
            {
                var alumno = await _repository.GetAlumnoNombreAsync(legajo);
                return Ok(alumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetAlumnoInfo([FromQuery] int legajo)
        {
            try
            {
                var alumnoInfo = await _repository.GetAlumnoInfoAsync(legajo);
                return Ok(alumnoInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("info/notas")]
        public async Task<IActionResult> GetAlumnosInfoNota([FromQuery] int legajo)
        {
            try
            {
                var alumnoInfo = await _repository.GetAlumnoInfoNotaAsync(legajo);
                return Ok(alumnoInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
