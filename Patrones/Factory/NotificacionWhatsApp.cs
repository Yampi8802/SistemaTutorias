namespace SistemaTutoria.Patrones.Factory;

public class NotificacionWhatsApp : Notificacion
{
    public void Enviar(string destinatario, string mensaje)
    {
        Console.WriteLine($"[WHATSAPP] Enviando a {destinatario}: {mensaje}");
    }
}
