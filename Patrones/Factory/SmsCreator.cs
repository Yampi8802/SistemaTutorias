namespace SistemaTutoria.Patrones.Factory;

public class SmsCreator : NotificacionCreator
{
    public override Notificacion CrearNotificacion()
    {
        return new NotificacionSMS();
    }
}