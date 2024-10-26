using Alumnos.Data.Data;
using Alumnos.Data.Repositories.Class;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using System;
using System.Threading.Tasks;

namespace Alumnos.Service.Repositories.Carrera
{
    public class CarreraServiceRepository : ICarreraServiceRepository
    {
        private readonly InscripcionACarreraRepository _repositoryInscripcionACarrera;
        private readonly IGenericRepository<Data.Data.Carrera> _repositoryCarrera;
        private readonly MateriasxcarreraRepository _repositoryMateriaXCarrera;
        private readonly IGenericRepository<Materia> _repositoryMateria;
        private readonly IGenericRepository<TiposMateria> _repositoryTiposMateria;
        private readonly IGenericRepository<MateriasXCursado> _repositoryMateriasXCursado;
        private readonly IGenericRepository<InscripcionACursado> _repositoryInscripcionACursado;

        public CarreraServiceRepository(
            InscripcionACarreraRepository repositoryInscripcionACarrera,
            IGenericRepository<Data.Data.Carrera> repositoryCarrera,
            MateriasxcarreraRepository repositoryMateriaXCarrera,
            IGenericRepository<Materia> repositoryMateria,
            IGenericRepository<TiposMateria> repositoryTiposMateria,
            IGenericRepository<MateriasXCursado> repositoryMateriasXCursado,
            IGenericRepository<InscripcionACursado> repositoryInscripcionACursado)
        {
            _repositoryInscripcionACarrera = repositoryInscripcionACarrera;
            _repositoryCarrera = repositoryCarrera;
            _repositoryMateriaXCarrera = repositoryMateriaXCarrera;
            _repositoryMateria = repositoryMateria;
            _repositoryTiposMateria = repositoryTiposMateria;
            _repositoryMateriasXCursado = repositoryMateriasXCursado;
            _repositoryInscripcionACursado = repositoryInscripcionACursado;
        }

        public async Task<InfoAlumnoModels> GetCarreraInfoUserAsync(int legajo)
        {
            try
            {
                InfoAlumnoModels infoAlumnoModels = new InfoAlumnoModels { Legajo = legajo };

                InscripcionACarrera inscripcionACarrera = await _repositoryInscripcionACarrera.GetInscripcionCarreraLegajoAsync(legajo);
                infoAlumnoModels.FechaInscripcionCursado = inscripcionACarrera.Fecha.ToString("dd/MM/yyyy");

                Materiasxcarrera materiasxcarrera = await _repositoryMateriaXCarrera.GetByMateriaXCarreraIdCarreraAsync(inscripcionACarrera.Carrera);
                MateriasXCursado materiasXCursado = await _repositoryMateriasXCursado.GetByIdAsync(materiasxcarrera.Materia);

                Data.Data.Carrera carrera = await _repositoryCarrera.GetByIdAsync(inscripcionACarrera.Carrera);
                infoAlumnoModels.Carrera = carrera.Carrera1;

                Materia materia = await _repositoryMateria.GetByIdAsync(materiasxcarrera.Materia);
                infoAlumnoModels.Materia = materia.Materia1;

                TiposMateria tiposMateria = await _repositoryTiposMateria.GetByIdAsync(materia.TipoMateria);
                infoAlumnoModels.TipoMateria = tiposMateria.TipoMateria;

                InscripcionACursado inscripcionACursado = await _repositoryInscripcionACursado.GetByIdAsync(materiasXCursado.InscripCursado);
                infoAlumnoModels.FechaInscripcionCursado = inscripcionACursado.Fecha.ToString("dd/MM/yyyy");

                return infoAlumnoModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
