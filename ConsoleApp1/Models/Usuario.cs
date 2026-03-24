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

    // Métodos
    public string ResumenCorto()
    {
        return $"[Usuario #{Id}] {Nombre} ({Documento}) [{(Activo ? "ACTIVO" : "INACTIVO")}]";
    }

    public string DetalleCompleto()
    {
        return
            $"--- Usuario ---\n" +
            $"Id: {Id}\n" +
            $"Documento: {Documento}\n" +
            $"Nombre: {Nombre}\n" +
            $"Contacto: {Contacto}\n" +
            $"Estado: {(Activo ? "Activo" : "Inactivo")}";
    }

    public override string ToString()
    {
        return ResumenCorto();
    }
}
