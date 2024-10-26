using Alumnos.Data.Data;
using Alumnos.Data.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Data.Repositories.Class
{
    public class MateriasxcarreraRepository : GenericRepository<Materiasxcarrera>
    {
        public MateriasxcarreraRepository(TpiDatosContext context) : base(context)
        {

        }

        public Materiasxcarrera GetByMateriaXCarreraIdCarrera(int carrera)
        {
            return _dbSet.FirstOrDefault(p => p.Carrera == carrera);
        }
    }
}
