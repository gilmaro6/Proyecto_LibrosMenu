using System;

namespace ConsoleApp1.Models;

public static class ModelsTesting
{
    // Objetos de prueba
    public static Libro Libro1 = new Libro(
        1,
        "ISBN-001",
        "El Quijote",
        "Miguel de Cervantes",
        1605,
        "Novela",
        true
    );

    public static Libro Libro2 = new Libro(
        2,
        "ISBN-002",
        "Cien años de soledad",
        "Gabriel García Márquez",
        1967,
        "Realismo mágico",
        false
    );

    public static Usuario Usuario1 = new Usuario(
        1,
        "CC123",
        "Juan Pérez",
        "juan@email.com",
        true
    );

    public static Usuario Usuario2 = new Usuario(
        2,
        "CC456",
        "Ana Gómez",
        "ana@email.com",
        true
    );

    public static Prestamo Prestamo1 = new Prestamo(
        1,
        Libro2,
        Usuario1,
        DateTime.Now.AddDays(-10),
        DateTime.Now.AddDays(-3)
    );

    // ✅ Mostrar resúmenes
    public static void MostrarResumenes()
    {
        Console.WriteLine(Libro1.ResumenCorto());
        Console.WriteLine(Libro2.ResumenCorto());
        Console.WriteLine(Usuario1.ResumenCorto());
        Console.WriteLine(Usuario2.ResumenCorto());
        Console.WriteLine(Prestamo1.ResumenCorto());
    }

    // ✅ Mostrar detalles completos
    public static void MostrarDetalles()
    {
        Console.WriteLine(Libro1.DetalleCompleto());
        Console.WriteLine();
        Console.WriteLine(Libro2.DetalleCompleto());
        Console.WriteLine();
        Console.WriteLine(Usuario1.DetalleCompleto());
        Console.WriteLine();
        Console.WriteLine(Prestamo1.DetalleCompleto());
    }

    // ✅ Mostrar validaciones
    public static void MostrarValidaciones()
    {
        Console.WriteLine($"Libro '{Libro1.Titulo}' Disponible: {Libro1.Disponible}");
        Console.WriteLine($"Libro '{Libro2.Titulo}' Disponible: {Libro2.Disponible}");
        Console.WriteLine($"Usuario '{Usuario1.Nombre}' Activo: {Usuario1.Activo}");
        Console.WriteLine($"Usuario '{Usuario2.Nombre}' Activo: {Usuario2.Activo}");
        Console.WriteLine($"Estado del préstamo: {Prestamo1.Estado}");
        Console.WriteLine($"¿Préstamo vencido?: {Prestamo1.EstaVencido()}");
        Console.WriteLine($"Días transcurridos: {Prestamo1.DiasTranscurridos()}");
    }
}
