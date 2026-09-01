namespace SistemaTutoria.Patrones.Factory;

public class WhatsAppCreator : NotificacionCreator
{
    public override Notificacion CrearNotificacion()
    {
        return new NotificacionWhatsApp();
    }
}