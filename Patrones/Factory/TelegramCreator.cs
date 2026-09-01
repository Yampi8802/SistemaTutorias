namespace SistemaTutoria.Patrones.Factory;

public class TelegramCreator : NotificacionCreator
{
    public override Notificacion CrearNotificacion()
    {
        return new NotificacionTelegram();
    }
}