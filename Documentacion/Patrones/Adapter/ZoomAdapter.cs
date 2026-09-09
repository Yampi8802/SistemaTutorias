namespace SistemaTutoria.Patrones.Adapter;

public class ZoomAdapter : Videoconferencia
{
    private readonly ProveedorZoom _proveedorZoom;

    public ZoomAdapter(ProveedorZoom proveedorZoom)
    {
        _proveedorZoom = proveedorZoom;
    }

    public string CrearReunion(string titulo)
    {
        return _proveedorZoom.IniciarReunionZoom(titulo);
    }
}