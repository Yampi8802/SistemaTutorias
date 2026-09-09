using SistemaTutoria.Dominio;
using SistemaTutoria.Notificaciones;

namespace SistemaTutoria.Servicios;

public class ServicioReservas
{
    private readonly INotificador _notificador;

    public ServicioReservas(INotificador notificador)
    {
        _notificador = notificador;
    }

    public void CrearReserva(Reserva reserva)
    {
        if (!reserva.Horario.Disponible)
        {
            throw new InvalidOperationException(
                "No se puede confirmar la reserva porque el horario no está disponible."
            );
        }

        reserva.Estado = "Confirmada";

        _notificador.Enviar(
            reserva.Estudiante.Correo,
            $"Su reserva de tutoría sobre {reserva.Tutoria.Tema} ha sido confirmada."
        );
    }
}