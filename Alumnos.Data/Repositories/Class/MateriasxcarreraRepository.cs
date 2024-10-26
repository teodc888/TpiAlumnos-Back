using Alumnos.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Alumnos.Data.Repositories.Class
{
    public class MateriasxcarreraRepository : GenericRepository<Materiasxcarrera>
    {
        public MateriasxcarreraRepository(TpiDatosContext context) : base(context)
        {
        }

        public async Task<Materiasxcarrera> GetByMateriaXCarreraIdCarreraAsync(int carrera)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Carrera == carrera);
        }
    }
}
