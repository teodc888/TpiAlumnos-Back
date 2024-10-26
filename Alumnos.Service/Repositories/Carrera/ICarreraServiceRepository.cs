using Alumnos.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Service.Repositories.Carrera
{
    public interface ICarreraServiceRepository
    {
        Task<InfoAlumnoModels> GetCarreraInfoUserAsync(int legajo);
    }
}
