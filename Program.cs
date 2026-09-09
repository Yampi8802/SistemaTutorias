using SistemaTutoria.Dominio;
using SistemaTutoria.Notificaciones;
using SistemaTutoria.Servicios;
using SistemaTutoria.Patrones.Factory;
using SistemaTutoria.Patrones.Builder;
using SistemaTutoria.Patrones.Adapter;
using SistemaTutoria.Patrones.Facade;

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

Console.WriteLine("=== SISTEMA DE GESTIÓN DE TUTORÍAS ===");

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

Console.WriteLine("\n=== PRUEBA BUILDER ===");

var reservaBuilder1 = new ReservaBuilder()
    .ConId(2)
    .ConEstudiante(estudiante)
    .ConDocente(docente)
    .ConTutoria(tutoria)
    .ConHorario(horario)
    .Build();

Console.WriteLine($"Reserva creada con Builder. Id: {reservaBuilder1.Id}");
Console.WriteLine($"Estado: {reservaBuilder1.Estado}");

var reservaBuilder2 = new ReservaBuilder()
    .ConEstudiante(estudiante)
    .ConDocente(docente)
    .ConTutoria(tutoria)
    .ConHorario(horario)
    .Build();

Console.WriteLine($"Segunda reserva creada con Builder. Id: {reservaBuilder2.Id}");
Console.WriteLine($"Estado: {reservaBuilder2.Estado}");

try
{
    var reservaInvalida = new ReservaBuilder()
        .ConEstudiante(estudiante)
        .Build();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Validación: {ex.Message}");
}

Console.WriteLine("\n=== PRUEBA FACADE + ADAPTER ===");

INotificador notificador = new Notificador();

var servicioReservas = new ServicioReservas(notificador);

var proveedorZoom = new ProveedorZoom();
Videoconferencia videoconferencia = new ZoomAdapter(proveedorZoom);

var facade = new TutoriasFacade(
    servicioReservas,
    videoconferencia
);

var enlaceTutoria = facade.CrearTutoriaVirtual(reserva);

Console.WriteLine($"Estado de la reserva: {reserva.Estado}");
Console.WriteLine("Tutoría virtual creada.");
Console.WriteLine($"Enlace: {enlaceTutoria}");

Console.WriteLine("\n=== FIN DE PRUEBAS ===");