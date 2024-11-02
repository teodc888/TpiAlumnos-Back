using Alumnos.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Data.Repositories.InfoDocente
{
    public interface IInfoDocenteRepository
    {
        Task<DocentePromedioNotasModel> GetDocentePromedioNotasAsync(int docente);
        Task<DocenteTotalAlumnosModel> GetTotalAlumnosPorDocenteAsync(int docente);
        Task<DocenteTotalMateriasModel> GetTotalMateriasPorDocenteAsync(int docenteId);
    }
}
