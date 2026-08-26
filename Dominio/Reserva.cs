namespace SistemaTutoria.Dominio;

public class Reserva
{
    public int Id { get; set; }

    public Estudiante Estudiante { get; set; } = null!;
    public Docente Docente { get; set; } = null!;
    public Tutoria Tutoria { get; set; } = null!;
    public HorarioTutoria Horario { get; set; } = null!;

    public string Estado { get; set; } = "Pendiente";
}