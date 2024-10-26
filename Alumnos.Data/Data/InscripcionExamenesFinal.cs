using System;
using System.Collections.Generic;

namespace Alumnos.Data.Data;

public partial class InscripcionExamenesFinal
{
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public int Cursada { get; set; }

    public string? Codigo { get; set; }

    public virtual Cursada CursadaNavigation { get; set; } = null!;

    public virtual ICollection<Examene> Examenes { get; set; } = new List<Examene>();
}
