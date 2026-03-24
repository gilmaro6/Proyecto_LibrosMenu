using System;

namespace ConsoleApp1.Models;

public static class ModelsTesting
{
    // Libros de prueba
    public static Libro Libro1 = new Libro(
        1,
        ""ISBN-001"",
        ""El Quijote"",
        ""Miguel de Cervantes"",
        1605,
        ""Novela"",
        true
    );

    public static Libro Libro2 = new Libro(
        2,
        ""ISBN-002"",
        ""Cien años de soledad"",
        ""Gabriel García Márquez"",
        1967,
        ""Realismo mágico"",
        false
    );

    // Usuarios de prueba
    public static Usuario Usuario1 = new Usuario(
        1,
        ""CC123"",
        ""Juan Pérez"",
        ""juan@email.com"",
        true
    );

    public static Usuario Usuario2 = new Usuario(
        2,
        ""CC456"",
        ""Ana Gómez"",
        ""ana@email.com"",
        true
    );

    // Préstamo de prueba
    public static Prestamo Prestamo1 = new Prestamo(
        1,
        Libro2,
        Usuario1,
        DateTime.Now.AddDays(-10),
        DateTime.Now.AddDays(-3)
    );
}
