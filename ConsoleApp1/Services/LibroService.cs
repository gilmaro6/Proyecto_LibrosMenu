using System.Collections.Generic;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class LibroService
{
    private readonly List<Libro> libros = new List<Libro>();

    public void Agregar(Libro libro)
    {
        libros.Add(libro);
    }

    public List<Libro> ObtenerTodos()
    {
        return new List<Libro>(libros);
    }
}
