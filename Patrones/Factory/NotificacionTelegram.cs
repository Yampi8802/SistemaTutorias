namespace SistemaTutoria.Patrones.Factory;

public class NotificacionTelegram : Notificacion
{
    public void Enviar(string destinatario, string mensaje)
    {
        Console.WriteLine($"[TELEGRAM] Enviando a {destinatario}: {mensaje}");
    }
}