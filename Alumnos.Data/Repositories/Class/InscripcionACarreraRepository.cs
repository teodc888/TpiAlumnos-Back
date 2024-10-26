using Alumnos.Data.Data;
using Alumnos.Data.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Alumnos.Data.Repositories.Class
{
    public class InscripcionACarreraRepository : GenericRepository<InscripcionACarrera>
    {
        
        public InscripcionACarreraRepository(TpiDatosContext context) : base(context)
        {

        }

        public InscripcionACarrera GetInscripcionCarreraLegajo(int legajo)
        {
            return _dbSet.FirstOrDefault(p => p.Alumno == legajo);
        }
    }
}
