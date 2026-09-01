namespace SistemaTutoria.Patrones.Factory;

public class NotificacionEmail : Notificacion
{
    public void Enviar(string destinatario, string mensaje)
    {
        Console.WriteLine($"[EMAIL] Enviando a {destinatario}: {mensaje}");
    }
}