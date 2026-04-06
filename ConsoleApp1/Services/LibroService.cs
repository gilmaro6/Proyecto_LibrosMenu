using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class LibroService
{
    private readonly List<Libro> libros = new List<Libro>();

    public void Agregar(Libro libro)
    {
        libros.Add(libro);
    }

    public bool Eliminar(Libro libro)
    {
        return libros.Remove(libro);
    }

    public List<Libro> ObtenerTodos()
    {
        return new List<Libro>(libros);
    }

    public Libro BuscarPorISBN(string isbn)
    {
        return libros.FirstOrDefault(l => l.ISBN == isbn);
    }

    public List<Libro> BuscarPorTitulo(string titulo)
    {
        return libros.Where(l => l.Titulo.Contains(titulo)).ToList();
    }

    public List<Libro> BuscarPorAutor(string autor)
    {
        return libros.Where(l => l.Autor.Contains(autor)).ToList();
    }
}
