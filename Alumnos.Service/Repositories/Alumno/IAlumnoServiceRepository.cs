using Alumnos.Data.Data;
using Alumnos.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnos.Service.Repositories.Alumno
{
    public interface IAlumnoServiceRepository
    {
        List<Data.Data.Alumno> GetAlumnos();
        AlumnoModels GetAlumnoNombre(int legajo);
        InfoAlumnoModels GetAlumnoInfo(int legajo);
    }
}
