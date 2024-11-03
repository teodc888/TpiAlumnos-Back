using Alumnos.Data.Data;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using Alumnos.Service.Repositories.Carrera;
using Alumnos.Service.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alumnos.Service.Repositories.Alumno
{
    public class AlumnoServiceRepository : IAlumnoServiceRepository
    {
        private readonly IGenericRepository<Data.Data.Alumno> _repositoryAlumno;
        private readonly IGenericRepository<TiposDoc> _repositoryTipoDoc;
        private readonly IGenericRepository<Localidade> _repositoryLocalidate;
        private readonly IGenericRepository<EstadoCivil> _repositoryEstadoCivil;
        private readonly IGenericRepository<EstadoHabitacional> _repositoryEstadoHabitacional;
        private readonly IGenericRepository<SituacionLaboral> _repositorySituacionLaboral;
        private readonly IGenericRepository<Genero> _repositoryGenero;
        private readonly ICarreraServiceRepository _carreraServiceRepository;
        private readonly AlumnoValidation alumnoValidation;


        //
        private readonly IGenericRepository<InscripcionExamenesFinal> _repositoryInscripcionExamenFinal;
        private readonly IGenericRepository<InscripcionACursado> _repositoryIncripcionCursado;
        private readonly IGenericRepository<MateriasXCursado> _repositoryMateriasCursado;
        private readonly IGenericRepository<EstadosMateria> _repositoryEstadoMateria;
        private readonly IGenericRepository<Cursada> _repositoryCursada;
        //

        public AlumnoServiceRepository(
            IGenericRepository<Data.Data.Alumno> repository,
            IGenericRepository<Localidade> repositoryLocalidate,
            IGenericRepository<EstadoCivil> repositoryEstadoCivil,
            IGenericRepository<EstadoHabitacional> repositoryEstadoHabitacional,
            IGenericRepository<SituacionLaboral> repositorySituacionLaboral,
            IGenericRepository<Genero> repositoryGenero,
            IGenericRepository<TiposDoc> repositoryTipoDoc,
            ICarreraServiceRepository carreraServiceRepository,


            //
            IGenericRepository<InscripcionExamenesFinal> repositoryInscripcionExamenFinal,// Repositorio de Inscripciones
            IGenericRepository<InscripcionACursado> repositoryIncripcionCursado,
            IGenericRepository<MateriasXCursado> repositoryMateriasCursado,
            IGenericRepository<EstadosMateria> repositoryEstadoMateria,
            IGenericRepository<Cursada> repositoryCursada
            //
            )
        {
            _repositoryAlumno = repository;

            //
            _repositoryInscripcionExamenFinal = repositoryInscripcionExamenFinal;
            _repositoryIncripcionCursado = repositoryIncripcionCursado;
            _repositoryMateriasCursado = repositoryMateriasCursado;
            _repositoryEstadoMateria = repositoryEstadoMateria;
            _repositoryCursada = repositoryCursada;
            //

            alumnoValidation = new AlumnoValidation();
            _repositoryLocalidate = repositoryLocalidate;
            _repositoryEstadoCivil = repositoryEstadoCivil;
            _repositoryEstadoHabitacional = repositoryEstadoHabitacional;
            _repositorySituacionLaboral = repositorySituacionLaboral;
            _repositoryGenero = repositoryGenero;
            _repositoryTipoDoc = repositoryTipoDoc;
            _carreraServiceRepository = carreraServiceRepository;

        }

        public async Task<AlumnoModels> GetAlumnoNombreAsync(int legajo)
        {
            try
            {
                AlumnoModels alumnoModels = new AlumnoModels();
                Data.Data.Alumno alumnoEncontrado = await _repositoryAlumno.GetByIdAsync(legajo);

                if (!alumnoValidation.Validate(alumnoEncontrado))
                {
                    throw new Exception("Alumno Invalido");
                }

                alumnoModels.Legajo = alumnoEncontrado.Legajo;
                alumnoModels.Nombre = alumnoEncontrado.Nombre;
                alumnoModels.Apellido = alumnoEncontrado.Apellido;
                alumnoModels.NumeroDocumento = alumnoEncontrado.NroDoc;
                alumnoModels.Direccion = alumnoEncontrado.Direccion;
                alumnoModels.FechaNacimiento = ((DateTime)alumnoEncontrado.FechaNac).ToString("dd/MM/yyyy");

                TiposDoc tiposDoc = await _repositoryTipoDoc.GetByIdAsync(alumnoEncontrado.TipoDoc);
                alumnoModels.TipoDocumento = tiposDoc.TipoDoc;

                Localidade localidad = await _repositoryLocalidate.GetByIdAsync(alumnoEncontrado.Localidad);
                alumnoModels.Localidad = localidad.Nombre;

                EstadoCivil estadoCivil = await _repositoryEstadoCivil.GetByIdAsync(alumnoEncontrado.EstadoCivil);
                alumnoModels.EstadoCivil = estadoCivil.EstadoCivil1;

                EstadoHabitacional estadoHabitacional = await _repositoryEstadoHabitacional.GetByIdAsync(alumnoEncontrado.EstadoHabit);
                alumnoModels.EstadoHabitacional = estadoHabitacional.EstadoHabitacional1;

                SituacionLaboral situacionLaboral = await _repositorySituacionLaboral.GetByIdAsync(alumnoEncontrado.SituacLab);
                alumnoModels.SituacionLaboral = situacionLaboral.SituacionLab;

                Genero genero = await _repositoryGenero.GetByIdAsync(alumnoEncontrado.Genero);
                alumnoModels.Genero = genero.Genero1;

                return alumnoModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<InfoAlumnoModels>> GetAlumnoInfoAsync(int legajo)
        {
            try
            {
                Data.Data.Alumno alumnoEncontrado = await _repositoryAlumno.GetByIdAsync(legajo);

                if (!alumnoValidation.Validate(alumnoEncontrado))
                {
                    throw new Exception("Alumno Invalido");
                }

                List<InfoAlumnoModels> infoAlumnoModels = await _carreraServiceRepository.GetCarreraInfoUserAsync(alumnoEncontrado.Legajo);

                if (infoAlumnoModels == null)
                {
                    throw new Exception("Informacion del alumno invalida");
                }

                return infoAlumnoModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<InfoAlumnoNotasModls>> GetAlumnoInfoNotaAsync(int legajo)
        {
            try
            {
                Data.Data.Alumno alumnoEncontrado = await _repositoryAlumno.GetByIdAsync(legajo);

                if (!alumnoValidation.Validate(alumnoEncontrado))
                {
                    throw new Exception("Alumno Invalido");
                }

                List<InfoAlumnoNotasModls> infoAlumnoModels = await _carreraServiceRepository.GetCarreraInfoNotaUserAsync(alumnoEncontrado.Legajo);

                if (infoAlumnoModels == null)
                {
                    throw new Exception("Informacion del alumno invalida");
                }

                return infoAlumnoModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<Data.Data.Alumno>> GetAlumnosAsync()
        {
            return (List<Data.Data.Alumno>)await _repositoryAlumno.GetAllAsync();
        }

        public async Task InscribirAlumnoEnExamenFinalAsync(int legajo, string nombre, string apellido, int idMateria, DateTime fechaInscripcion)
        {
            // 1. Obtener al alumno usando su legajo
            var alumnoInscripto = await _repositoryAlumno.GetByIdAsync(legajo);
            if (alumnoInscripto == null)
            {
                throw new Exception("El alumno no existe.");
            }

            // 2. Obtener la inscripción a cursado del alumno
            var inscripcionACursado = await _repositoryIncripcionCursado.GetAllAsync();
            var cursadoEncontrado = inscripcionACursado.FirstOrDefault(ic => ic.Alumno == legajo);
            if (cursadoEncontrado == null)
            {
                throw new Exception("No se encontró inscripción a cursado para el alumno.");
            }

            // 3. Verificar que el alumno esté inscripto en la materia para la cual se quiere inscribir al examen
            var materiasXCursado = await _repositoryMateriasCursado.GetAllAsync();
            var materiaXCursada = materiasXCursado.FirstOrDefault(m => m.InscripCursado == cursadoEncontrado.Id && m.Materiaxcarrera == idMateria);
            if (materiaXCursada == null)
            {
                throw new Exception("El alumno no está inscripto en esta materia.");
            }

            // 4. Crear la nueva inscripción al examen final
            var inscripcionExamen = new InscripcionExamenesFinal
            {
                Fecha = DateOnly.FromDateTime(fechaInscripcion),
                Hora = TimeOnly.FromDateTime(DateTime.Now), // Ajusta según tus requerimientos
                Cursada = materiaXCursada.InscripCursado.Value, // Asegúrate de que InscripCursado no sea nulo
                Codigo = $"{legajo}-{idMateria}-{DateTime.Now.Ticks}" // Un código único, ajusta según tu lógica
            };

            // 5. Insertar la inscripción en el repositorio
            await _repositoryInscripcionExamenFinal.InsertAsync(inscripcionExamen);
            await _repositoryInscripcionExamenFinal.SaveAsync();
        }
    }
}
