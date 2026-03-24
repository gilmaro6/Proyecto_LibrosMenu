namespace ConsoleApp1.Models;

public class Libro
{
    public int Id { get; set; }
    public string ISBN { get; set; } = "";
    public string Titulo { get; set; } = "";
    public string Autor { get; set; } = "";
    public int Anio { get; set; }
    public string Categoria { get; set; } = "";
    public bool Disponible { get; set; } = true; // por defecto true

    // Constructor vacío
    public Libro()
    {
        Disponible = true;
    }

    // Constructor con parámetros
    public Libro(
        int id,
        string isbn,
        string titulo,
        string autor,
        int anio,
        string categoria,
        bool disponible = true
    )
    {
        Id = id;
        ISBN = isbn;
        Titulo = titulo;
        Autor = autor;
        Anio = anio;
        Categoria = categoria;
        Disponible = disponible; 
    }
}