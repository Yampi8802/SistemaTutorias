namespace SistemaTutoria.Dominio;

public class HorarioTutoria
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public bool Disponible { get; set; }
}
