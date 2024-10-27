using Alumnos.Service.Repositories.Docente;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alumnos.WebApi.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class DocenteController : ControllerBase
    {

        private readonly IDocenteServiceRepository _docenteServiceRepository;
        public DocenteController(IDocenteServiceRepository docenteServiceRepository) { 
            _docenteServiceRepository = docenteServiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocente([FromQuery] int legajo)
        {
            try
            {
               var docente = await _docenteServiceRepository.GetDocente(legajo);

                return Ok(docente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("tribunal")]
        public async Task<IActionResult> GetDocenteXTribunal([FromQuery] int legajo)
        {
            try
            {
                var docente = await _docenteServiceRepository.GetTribunalDocente(legajo);

                return Ok(docente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpGet("materia")]
        public async Task<IActionResult> GetDocenteXMateria([FromQuery] int legajo)
        {
            try
            {
                var docente = await _docenteServiceRepository.GetMateriaDocente(legajo);

                return Ok(docente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
