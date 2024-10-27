using Alumnos.Data.Data;
using Alumnos.Data.Repositories.Class;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Service.Repositories.Docente
{
    public class DocenteServiceRepository : IDocenteServiceRepository
    {
        private readonly IGenericRepository<Data.Data.Docente> _repositoryDocente;
        private readonly IGenericRepository<TiposDoc> _repositoryTipoDoc;
        private readonly IGenericRepository<Localidade> _repositoryLocalidate;
        private readonly IGenericRepository<EstadoCivil> _repositoryEstadoCivil;
        private readonly IGenericRepository<EstadoHabitacional> _repositoryEstadoHabitacional;
        private readonly IGenericRepository<SituacionLaboral> _repositorySituacionLaboral;
        private readonly IGenericRepository<Genero> _repositoryGenero;
        private readonly IGenericRepository<Tribunale> _repositoryTribunal;
        private readonly IGenericRepository<Materia> _repositoryMateria;
        private readonly IGenericRepository<Data.Data.Carrera> _repositoryCarrera;
        private readonly DocenteXTribunalesRepository _docenteXTribunalesRepository;
        private readonly MateriasxcarreraRepository _materiasxcarreraRepository;
        public DocenteServiceRepository(
            IGenericRepository<Data.Data.Docente> repositoryDocente,
            IGenericRepository<Localidade> repositoryLocalidate,
            IGenericRepository<EstadoCivil> repositoryEstadoCivil,
            IGenericRepository<EstadoHabitacional> repositoryEstadoHabitacional,
            IGenericRepository<SituacionLaboral> repositorySituacionLaboral,
            IGenericRepository<Genero> repositoryGenero,
            IGenericRepository<TiposDoc> repositoryTipoDoc,
            IGenericRepository<Tribunale> repositoryTribunal,
            IGenericRepository<Materia> repositoryMateria,
            IGenericRepository<Data.Data.Carrera> repositoryCarrera,
            DocenteXTribunalesRepository docenteXTribunalesRepository,
            MateriasxcarreraRepository materiasxcarreraRepository)
        {
            _repositoryDocente = repositoryDocente;
            _repositoryLocalidate = repositoryLocalidate;
            _repositoryEstadoCivil = repositoryEstadoCivil;
            _repositoryEstadoHabitacional = repositoryEstadoHabitacional;
            _repositorySituacionLaboral = repositorySituacionLaboral;
            _repositoryGenero = repositoryGenero;
            _repositoryTipoDoc = repositoryTipoDoc;
            _repositoryTribunal = repositoryTribunal;
            _repositoryMateria = repositoryMateria;
            _repositoryCarrera = repositoryCarrera;
            _docenteXTribunalesRepository = docenteXTribunalesRepository;
            _materiasxcarreraRepository = materiasxcarreraRepository;
        }

        public async Task<DocenteModels> GetDocente(int legajo)
        {
            try
            {
                DocenteModels docenteModels = new DocenteModels();
                Data.Data.Docente docenteEncontrado = await _repositoryDocente.GetByIdAsync(legajo);

                if (docenteEncontrado == null)
                {
                    throw new Exception("Docente no encontrado");
                }

                docenteModels.Legajo = docenteEncontrado.Legajo;
                docenteModels.Nombre = docenteEncontrado.Nombre;
                docenteModels.Apellido = docenteEncontrado.Apellido;
                docenteModels.NumeroDocumento = docenteEncontrado.NroDoc;
                docenteModels.Direccion = docenteEncontrado.Direccion;
                docenteModels.FechaNacimiento = ((DateTime)docenteEncontrado.FechaNac).ToString("dd/MM/yyyy");
                docenteModels.TituloUniversitario = docenteEncontrado.TituloUniversitario;

                TiposDoc tiposDoc = await _repositoryTipoDoc.GetByIdAsync(docenteEncontrado.TipoDoc);
                docenteModels.TipoDocumento = tiposDoc.TipoDoc;

                Localidade localidad = await _repositoryLocalidate.GetByIdAsync(docenteEncontrado.Localidad);
                docenteModels.Localidad = localidad.Nombre;

                EstadoCivil estadoCivil = await _repositoryEstadoCivil.GetByIdAsync(docenteEncontrado.EstadoCivil);
                docenteModels.EstadoCivil = estadoCivil.EstadoCivil1;

                EstadoHabitacional estadoHabitacional = await _repositoryEstadoHabitacional.GetByIdAsync(docenteEncontrado.EstadoHabit);
                docenteModels.EstadoHabitacional = estadoHabitacional.EstadoHabitacional1;

                SituacionLaboral situacionLaboral = await _repositorySituacionLaboral.GetByIdAsync(docenteEncontrado.SituacLab);
                docenteModels.SituacionLaboral = situacionLaboral.SituacionLab;

                Genero genero = await _repositoryGenero.GetByIdAsync(docenteEncontrado.Genero);
                docenteModels.Genero = genero.Genero1;

                return docenteModels;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<MateriaModels>> GetMateriaDocente(int legajo)
        {
            try
            {
                List<MateriaModels> lstMateria = new List<MateriaModels>();

                List<Materiasxcarrera> lstMateriaxCarrera = await _materiasxcarreraRepository.GetByMateriaXCarreraIdLegajoAsync(legajo);

                foreach (Materiasxcarrera materiasxcarrera in lstMateriaxCarrera)
                {
                    MateriaModels materiaModels = new MateriaModels();

                    materiaModels.Legajo = legajo;

                    Data.Data.Carrera carrera = await _repositoryCarrera.GetByIdAsync(materiasxcarrera.Carrera);
                    materiaModels.Carrera = carrera.Carrera1;
                    materiaModels.AnioPlan = carrera.AnioPlan;

                    Materia materia = await _repositoryMateria.GetByIdAsync(materiasxcarrera.Materia);
                    materiaModels.Materia = materia.Materia1;

                    lstMateria.Add(materiaModels);
                }

                return lstMateria;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<List<TribunalModels>> GetTribunalDocente(int legajo)
        {
            try
            {
                List<TribunalModels> lstTribunales = new List<TribunalModels>();

                List<DocentesXTribunale> lstDocenteXTribunales = await _docenteXTribunalesRepository.GetLegajoTribunal(legajo);

                if (lstDocenteXTribunales.Count == 0)
                {
                    throw new Exception("No se encontraron tribunales");
                }

                foreach (DocentesXTribunale docentesXTribunale in lstDocenteXTribunales)
                {
                    TribunalModels tribunalModels = new TribunalModels();

                    tribunalModels.Legajo = docentesXTribunale.Docente;

                    Tribunale tribunale = await _repositoryTribunal.GetByIdAsync(docentesXTribunale.Tribunal);
                    tribunalModels.Aula = tribunale.Aula;
                    tribunalModels.Fecha = ((DateOnly)tribunale.Fecha).ToString("dd/MM/yyyy");

                    Materia materia = await _repositoryMateria.GetByIdAsync(tribunale.Materia);
                    tribunalModels.Materia = materia.Materia1;

                    lstTribunales.Add(tribunalModels);
                }

                return lstTribunales;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
