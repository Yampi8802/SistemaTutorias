using SistemaTutoria.Dominio;
using SistemaTutoria.Notificaciones;
using SistemaTutoria.Servicios;
using SistemaTutoria.Patrones.Factory;

var estudiante = new Estudiante
{
    Id = 1,
    Nombre = "Jean Orozco",
    Correo = "jean@email.com"
};

var docente = new Docente
{
    Id = 1,
    Nombre = "Francisco Cevallos",
    Especialidad = "Programación"
};

var tutoria = new Tutoria
{
    Id = 1,
    Tema = "Programación en C#",
    Descripcion = "Tutoría sobre fundamentos de C#"
};

var horario = new HorarioTutoria
{
    Id = 1,
    Fecha = DateTime.Today,
    HoraInicio = new TimeSpan(10, 0, 0),
    HoraFin = new TimeSpan(11, 0, 0),
    Disponible = true
};

var reserva = new Reserva
{
    Id = 1,
    Estudiante = estudiante,
    Docente = docente,
    Tutoria = tutoria,
    Horario = horario
};

INotificador notificador = new Notificador();
var servicioReservas = new ServicioReservas(notificador);

servicioReservas.CrearReserva(reserva);

Console.WriteLine($"Estado de la reserva: {reserva.Estado}");
Console.WriteLine("\n=== PRUEBA FACTORY METHOD ===");

NotificacionCreator emailCreator = new EmailCreator();
emailCreator.EnviarNotificacion(
    estudiante.Correo,
    "Su tutoría ha sido confirmada."
);

NotificacionCreator smsCreator = new SmsCreator();
smsCreator.EnviarNotificacion(
    "0999999999",
    "Su tutoría ha sido confirmada."
);

NotificacionCreator whatsappCreator = new WhatsAppCreator();
whatsappCreator.EnviarNotificacion(
    "0999999999",
    "Su tutoría ha sido confirmada."
);
NotificacionCreator telegramCreator = new TelegramCreator();
telegramCreator.EnviarNotificacion(
    "usuario_telegram",
    "Su tutoría ha sido confirmada."
);