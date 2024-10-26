﻿using Alumnos.Model.Models;

namespace Alumnos.Service.Repositories.Alumno
{
    public interface IAlumnoServiceRepository
    {
        Task<List<Data.Data.Alumno>> GetAlumnosAsync();
        Task<AlumnoModels> GetAlumnoNombreAsync(int legajo);
        Task<List<InfoAlumnoModels>> GetAlumnoInfoAsync(int legajo);
        Task<List<InfoAlumnoNotasModls>> GetAlumnoInfoNotaAsync(int legajo);
    }
}
