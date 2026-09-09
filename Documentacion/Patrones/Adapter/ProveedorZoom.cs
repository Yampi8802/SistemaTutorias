namespace SistemaTutoria.Patrones.Adapter;

public class ProveedorZoom
{
    public string IniciarReunionZoom(string temaReunion)
    {
        Console.WriteLine($"Zoom: iniciando reunión '{temaReunion}'.");

        return $"https://zoom.us/j/123456789";
    }
}