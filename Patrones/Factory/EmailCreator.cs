namespace SistemaTutoria.Patrones.Factory;

public class EmailCreator : NotificacionCreator
{
    public override Notificacion CrearNotificacion()
    {
        return new NotificacionEmail();
    }
}