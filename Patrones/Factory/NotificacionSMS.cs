namespace SistemaTutoria.Patrones.Factory;

public class NotificacionSMS : Notificacion
{
    public void Enviar(string destinatario, string mensaje)
    {
        Console.WriteLine($"[SMS] Enviando a {destinatario}: {mensaje}");
    }
}