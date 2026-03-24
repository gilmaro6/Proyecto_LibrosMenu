namespace ConsoleApp1.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Documento { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Contacto { get; set; } = "";
    public bool Activo { get; set; } = true;

    // Constructor vacío
    public Usuario()
    {
        Activo = true;
    }

    // Constructor con parámetros
    public Usuario(
        int id,
        string documento,
        string nombre,
        string contacto,
        bool activo = true
    )
    {
        Id = id;
        Documento = documento;
        Nombre = nombre;
        Contacto = contacto;
        Activo = activo;
    }
}
