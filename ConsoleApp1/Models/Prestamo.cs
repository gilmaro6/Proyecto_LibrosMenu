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

    public Prestamo()
    {
        Estado = EstadoPrestamo.Activo;
        FechaPrestamo = DateTime.Now;
        FechaVencimiento = DateTime.Now.AddDays(8);
        FechaDevolucion = null;
    }

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

    public bool EstaVencido()
    {
        if (Estado == EstadoPrestamo.Devuelto) return false;
        return DateTime.Now.Date > FechaVencimiento.Date;
    }

    public int DiasTranscurridos()
    {
        var hasta = FechaDevolucion?.Date ?? DateTime.Now.Date;
        return (int)(hasta - FechaPrestamo.Date).TotalDays;
    }

    public string ResumenCorto()
    {
        return $"[Préstamo #{Id}] Libro: {Libro?.Titulo ?? "(sin libro)"} ? Usuario: {Usuario?.Nombre ?? "(sin usuario)"} | {Estado}";
    }

    public string DetalleCompleto()
    {
        var devolucion = FechaDevolucion.HasValue ? FechaDevolucion.Value.ToString("yyyy-MM-dd") : "—";
        return $"--- Préstamo ---\n" +
               $"Id: {Id}\n" +
               $"Libro: {(Libro != null ? Libro.Titulo : "(null)")}\n" +
               $"Usuario: {(Usuario != null ? Usuario.Nombre : "(null)")}\n" +
               $"Fecha préstamo: {FechaPrestamo:yyyy-MM-dd}\n" +
               $"Fecha vencimiento: {FechaVencimiento:yyyy-MM-dd}\n" +
               $"Fecha devolución: {devolucion}\n" +
               $"Estado: {Estado}\n" +
               $"Vencido: {(EstaVencido() ? "Sí" : "No")}\n" +
               $"Días transcurridos: {DiasTranscurridos()}";
    }

    public override string ToString()
    {
        return ResumenCorto();
    }
}
