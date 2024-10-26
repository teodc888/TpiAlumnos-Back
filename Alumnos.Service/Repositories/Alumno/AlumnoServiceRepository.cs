using Alumnos.Data.Data;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using Alumnos.Service.Repositories.Carrera;
using Alumnos.Service.Validation;


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
        public AlumnoServiceRepository(IGenericRepository<Data.Data.Alumno> repository, IGenericRepository<Localidade> repositoryLocalidate, IGenericRepository<EstadoCivil> repositoryEstadoCivil, IGenericRepository<EstadoHabitacional> repositoryEstadoHabitacional, IGenericRepository<SituacionLaboral> repositorySituacionLaboral, IGenericRepository<Genero> repositoryGenero, IGenericRepository<TiposDoc> repositoryTipoDoc, ICarreraServiceRepository carreraServiceRepository)
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
        public AlumnoModels GetAlumnoNombre(int legajo)
        {
            try
            {
                AlumnoModels alumnoModels = new AlumnoModels();
                Data.Data.Alumno alumnoEncontrado = (Data.Data.Alumno)_repositoryAlumno.GetById(legajo);

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

                TiposDoc tiposDoc = (TiposDoc)_repositoryTipoDoc.GetById(alumnoEncontrado.TipoDoc);
                alumnoModels.TipoDocumento = tiposDoc.TipoDoc;

                Localidade localidad = (Localidade)_repositoryLocalidate.GetById(alumnoEncontrado.Localidad);
                alumnoModels.Localidad = localidad.Nombre;

                EstadoCivil estadoCivil = (EstadoCivil)_repositoryEstadoCivil.GetById(alumnoEncontrado.EstadoCivil);
                alumnoModels.EstadoCivil = estadoCivil.EstadoCivil1;

                EstadoHabitacional estadoHabitacional = (EstadoHabitacional)_repositoryEstadoHabitacional.GetById(alumnoEncontrado.EstadoHabit);
                alumnoModels.EstadoHabitacional = estadoHabitacional.EstadoHabitacional1;

                SituacionLaboral situacionLaboral = (SituacionLaboral)_repositorySituacionLaboral.GetById(alumnoEncontrado.SituacLab);
                alumnoModels.SituacionLaboral = situacionLaboral.SituacionLab;

                Genero genero = (Genero)_repositoryGenero.GetById(alumnoEncontrado.Genero);
                alumnoModels.Genero = genero.Genero1;

                return alumnoModels;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public InfoAlumnoModels GetAlumnoInfo(int legajo)
        {
            try
            {
                InfoAlumnoModels infoAlumnoModels = new InfoAlumnoModels();
                Data.Data.Alumno alumnoEncontrado = (Data.Data.Alumno)_repositoryAlumno.GetById(legajo);

                if (!alumnoValidation.Validate(alumnoEncontrado))
                {
                    throw new Exception("Alumno Invalido");
                }

                infoAlumnoModels = _carreraServiceRepository.GetCarreraInfoUser(alumnoEncontrado.Legajo);

                if(infoAlumnoModels == null)
                {
                    throw new Exception("Informacion del alumno invalida");
                }

                return infoAlumnoModels;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<Data.Data.Alumno> GetAlumnos()
        {
            return (List<Data.Data.Alumno>)_repositoryAlumno.GetAll();
        }
    }
}
