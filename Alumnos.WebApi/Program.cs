using Alumnos.Data.Data;
using Alumnos.Data.Repositories.Carrera;
using Alumnos.Data.Repositories.Class;
using Alumnos.Data.Repositories.GenericRepository;
using Alumnos.Service.Repositories.Alumno;
using Alumnos.Service.Repositories.Carrera;
using Alumnos.Service.Repositories.Docente;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TpiDatosContext>(optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar el repositorio genérico
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAlumnoServiceRepository, AlumnoServiceRepository>();
builder.Services.AddScoped<IDocenteServiceRepository, DocenteServiceRepository>();
builder.Services.AddScoped<ICarreraServiceRepository, CarreraServiceRepository>();
builder.Services.AddScoped<IInfoAlumnoRepository, InfoAlumnoRepository>();
builder.Services.AddScoped<DocenteXTribunalesRepository>();
builder.Services.AddScoped<InscripcionACarreraRepository>();
builder.Services.AddScoped<MateriasxcarreraRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
