using Alumnos.Data.Data;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using Alumnos.Service.Repositories.Carrera;
using Alumnos.Service.Validation;
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

        public AlumnoServiceRepository(
            IGenericRepository<Data.Data.Alumno> repository,
            IGenericRepository<Localidade> repositoryLocalidate,
            IGenericRepository<EstadoCivil> repositoryEstadoCivil,
            IGenericRepository<EstadoHabitacional> repositoryEstadoHabitacional,
            IGenericRepository<SituacionLaboral> repositorySituacionLaboral,
            IGenericRepository<Genero> repositoryGenero,
            IGenericRepository<TiposDoc> repositoryTipoDoc,
            ICarreraServiceRepository carreraServiceRepository)
        {
            _repositoryAlumno = repository;
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

        public async Task<InfoAlumnoModels> GetAlumnoInfoAsync(int legajo)
        {
            try
            {
                Data.Data.Alumno alumnoEncontrado = await _repositoryAlumno.GetByIdAsync(legajo);

                if (!alumnoValidation.Validate(alumnoEncontrado))
                {
                    throw new Exception("Alumno Invalido");
                }

                InfoAlumnoModels infoAlumnoModels = await _carreraServiceRepository.GetCarreraInfoUserAsync(alumnoEncontrado.Legajo);

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
    }
}
