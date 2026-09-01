using SistemaTutoria.Dominio;

namespace SistemaTutoria.Patrones.Builder;

public class ReservaBuilder
{
    private int _id;
    private Estudiante? _estudiante;
    private Docente? _docente;
    private Tutoria? _tutoria;
    private HorarioTutoria? _horario;
    private string _estado = "Pendiente";

    public ReservaBuilder ConEstudiante(Estudiante estudiante)
    {
        _estudiante = estudiante;
        return this;
    }
    public ReservaBuilder ConDocente(Docente docente)
    {
    _docente = docente;
    return this;
    }
    public ReservaBuilder ConTutoria(Tutoria tutoria)
    {
        _tutoria = tutoria;
        return this;
    }
    public ReservaBuilder ConHorario(HorarioTutoria horario)
    {
    _horario = horario;
    return this;
    }
    public ReservaBuilder ConId(int id)
    {
    _id = id;
    return this;
    }
    public ReservaBuilder ConEstado(string estado)
    {
    _estado = estado;
    return this;
    }
        public Reserva Build()
    {
        if (_estudiante is null)
            throw new InvalidOperationException("El estudiante es obligatorio.");

        if (_docente is null)
            throw new InvalidOperationException("El docente es obligatorio.");

        if (_tutoria is null)
            throw new InvalidOperationException("La tutoría es obligatoria.");

        if (_horario is null)
            throw new InvalidOperationException("El horario es obligatorio.");

        return new Reserva
        {
            Id = _id,
            Estudiante = _estudiante,
            Docente = _docente,
            Tutoria = _tutoria,
            Horario = _horario,
            Estado = _estado
        };
    }
}