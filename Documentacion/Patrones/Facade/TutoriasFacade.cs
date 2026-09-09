using SistemaTutoria.Dominio;
using SistemaTutoria.Servicios;
using SistemaTutoria.Patrones.Adapter;

namespace SistemaTutoria.Patrones.Facade;

public class TutoriasFacade
{
    private readonly ServicioReservas _servicioReservas;
    private readonly Videoconferencia _videoconferencia;

    public TutoriasFacade(
        ServicioReservas servicioReservas,
        Videoconferencia videoconferencia)
    {
        _servicioReservas = servicioReservas;
        _videoconferencia = videoconferencia;
    }

    public string CrearTutoriaVirtual(Reserva reserva)
    {
        _servicioReservas.CrearReserva(reserva);

        return _videoconferencia.CrearReunion(
            $"Tutoria de {reserva.Tutoria.Tema}"
        );
    }
}