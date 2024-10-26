using Alumnos.Data.Data;
using Alumnos.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Data.Repositories.Carrera
{
    public class InfoAlumnoRepository : IInfoAlumnoRepository
    {
        private readonly TpiDatosContext _context;

        public InfoAlumnoRepository(TpiDatosContext context)
        {
            _context = context;
        }

        public async Task<List<InfoAlumnoModels>> GetCarreraInfoUserAsync(int legajo)
        {
            try
            {
                var result = await (from ic in _context.InscripcionACarreras
                                    join c in _context.Carreras on ic.Carrera equals c.Id
                                    join mc in _context.Materiasxcarreras on ic.Carrera equals mc.Carrera
                                    join ma in _context.Materias on mc.Materia equals ma.Id
                                    join tm in _context.TiposMaterias on ma.TipoMateria equals tm.Id
                                    join mxc in _context.MateriasXCursados on mc.Id equals mxc.Materiaxcarrera
                                    join iac in _context.InscripcionACursados on mxc.InscripCursado equals iac.Id
                                    where ic.Alumno == legajo
                                    select new InfoAlumnoModels
                                    {
                                        Legajo = ic.Alumno,
                                        Carrera = c.Carrera1,
                                        Materia = ma.Materia1,
                                        TipoMateria = tm.TipoMateria,
                                        FechaInscripcionCursado = iac.Fecha.ToString("dd/MM/yyyy"),
                                        FechaInscripcionMateria = ic.Fecha.ToString("dd/MM/yyyy")
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Carrera Info: " + ex.Message);
            }
        }

        public async Task<List<InfoAlumnoNotasModls>> GetCarreraNotasInfoAsync(int legajo)
        {
            try
            {
                var result = await (from c in _context.Cursadas
                                    join cu in _context.Cursos on c.Curso equals cu.Id
                                    join es in _context.EstadosMaterias on c.EstadoMateria equals es.Id 
                                    join mxc in _context.MateriasXCursados on c.MateriaCursada equals mxc.Id
                                    join mxca in _context.Materiasxcarreras on mxc.Materiaxcarrera equals mxca.Id 
                                    join mat in _context.Materias on mxca.Materia equals mat.Id 
                                    join exc in _context.ExamenesXCursada on c.Id equals exc.Cursada 
                                    join e in _context.Examenes on exc.Examen equals e.Id 
                                    join esx in _context.EstadosExamenes on e.EstadoExamen equals esx.Id 
                                    join iac in _context.InscripcionACursados on mxc.InscripCursado equals iac.Id 
                                    join a in _context.Alumnos on iac.Alumno equals a.Legajo 
                                    where a.Legajo == legajo
                                    select new InfoAlumnoNotasModls
                                    {
                                        Legajo = a.Legajo,
                                        Curso = cu.Curso1,
                                        Estado = es.EstadoMateria,
                                        Materia = mat.Materia1,
                                        Nota = e.Nota.ToString(), 
                                        FechaCursado = c.Fecha
                                    }).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Carrera Notas Info: " + ex.Message);
            }
        }


    }
}
