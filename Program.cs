using SistemaTutoria.Dominio;
using SistemaTutoria.Notificaciones;
using SistemaTutoria.Servicios;

var estudiante = new Estudiante
{
    Id = 1,
    Nombre = "Juan Pérez",
    Correo = "juan@email.com"
};

var docente = new Docente
{
    Id = 1,
    Nombre = "Pedro García",
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