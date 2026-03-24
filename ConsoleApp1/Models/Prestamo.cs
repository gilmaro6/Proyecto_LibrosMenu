using System;

namespace ConsoleApp1.Models;

public class Prestamo
{
    public int Id { get; set; }
    public Libro? Libro { get; set; }
    public Usuario? Usuario { get; set; }
    public DateTime FechaPrestamo { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public EstadoPrestamo Estado { get; set; } = EstadoPrestamo.Activo;

    // Constructor vacío
    public Prestamo()
    {
        Estado = EstadoPrestamo.Activo;
        FechaPrestamo = DateTime.Now;
        FechaVencimiento = DateTime.Now.AddDays(8);
        FechaDevolucion = null;
    }

    // Constructor con parámetros
    public Prestamo(
        int id,
        Libro libro,
        Usuario usuario,
        DateTime fechaPrestamo,
        DateTime fechaVencimiento
    )
    {
        Id = id;
        Libro = libro;
        Usuario = usuario;
        FechaPrestamo = fechaPrestamo;
        FechaVencimiento = fechaVencimiento;
        FechaDevolucion = null;
        Estado = EstadoPrestamo.Activo;
    }
}
