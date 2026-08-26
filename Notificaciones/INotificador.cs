namespace SistemaTutoria.Notificaciones;

public interface INotificador
{
    void Enviar(string destinatario, string mensaje);
}