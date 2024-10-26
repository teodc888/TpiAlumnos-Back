using Alumnos.Data.Data;
using Alumnos.Data.Repositories.Class;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public InfoAlumnoModels GetCarreraInfoUser(int legajo)
        {
            try
            {
                InfoAlumnoModels infoAlumnoModels = new InfoAlumnoModels();
                infoAlumnoModels.Legajo = legajo;

                InscripcionACarrera inscripcionACarrera = (InscripcionACarrera)_repositoryInscripcionACarrera.GetInscripcionCarreraLegajo(legajo);
                infoAlumnoModels.FechaInscripcionCursado = ((DateOnly)inscripcionACarrera.Fecha).ToString("dd/MM/yyyy");

                Materiasxcarrera materiasxcarrera = (Materiasxcarrera)_repositoryMateriaXCarrera.GetByMateriaXCarreraIdCarrera(inscripcionACarrera.Carrera);
                MateriasXCursado materiasXCursado = (MateriasXCursado)_repositoryMateriasXCursado.GetById(materiasxcarrera.Materia);

                Data.Data.Carrera carrera = (Data.Data.Carrera)_repositoryCarrera.GetById(inscripcionACarrera.Carrera);
                infoAlumnoModels.Carrera = carrera.Carrera1;

                Materia materia = (Materia)_repositoryMateria.GetById(materiasxcarrera.Materia);
                infoAlumnoModels.Materia = materia.Materia1;

                TiposMateria tiposMateria = (TiposMateria)_repositoryTiposMateria.GetById(materia.TipoMateria);
                infoAlumnoModels.TipoMateria = tiposMateria.TipoMateria;

                InscripcionACursado inscripcionACursado = (InscripcionACursado)_repositoryInscripcionACursado.GetById(materiasXCursado.InscripCursado);
                infoAlumnoModels.FechaInscripcionCursado = ((DateOnly)inscripcionACursado.Fecha).ToString("dd/MM/yyyy");

                return infoAlumnoModels;

            }
            catch (Exception ex) {
                throw new Exception(ex.ToString());
            }
        }
    }
}
