namespace SistemaTutoria.Notificaciones;

public class Notificador : INotificador
{
    public void Enviar(string destinatario, string mensaje)
    {
        Console.WriteLine($"Notificación enviada a {destinatario}: {mensaje}");
    }
}