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
        reserva.Estado = "Confirmada";

        _notificador.Enviar(
            reserva.Estudiante.Correo,
            $"Su reserva de tutoría sobre {reserva.Tutoria.Tema} ha sido confirmada."
        );
    }
}