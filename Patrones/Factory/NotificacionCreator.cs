namespace SistemaTutoria.Patrones.Factory;

public abstract class NotificacionCreator
{
    public abstract Notificacion CrearNotificacion();

    public void EnviarNotificacion(string destinatario, string mensaje)
    {
        Notificacion notificacion = CrearNotificacion();
        notificacion.Enviar(destinatario, mensaje);
    }
}