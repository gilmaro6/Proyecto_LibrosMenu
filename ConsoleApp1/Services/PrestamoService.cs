using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class PrestamoService
{
    private readonly List<Prestamo> prestamos = new List<Prestamo>();

    public void Agregar(Prestamo prestamo)
    {
        prestamos.Add(prestamo);
    }

    public bool Eliminar(Prestamo prestamo)
    {
        return prestamos.Remove(prestamo);
    }

    public List<Prestamo> ObtenerTodos()
    {
        return new List<Prestamo>(prestamos);
    }

    public Prestamo BuscarPorId(int id)
    {
        return prestamos.FirstOrDefault(p => p.Id == id);
    }

    public List<Prestamo> BuscarPorEstado(EstadoPrestamo estado)
    {
        return prestamos.Where(p => p.Estado == estado).ToList();
    }

    public List<Prestamo> OrdenarPorFechaLimite()
    {
        return prestamos.OrderBy(p => p.FechaVencimiento).ToList();
    }

    public int TotalPrestamos()
    {
        return prestamos.Count;
    }

    public int TotalActivos()
    {
        return prestamos.Count(p => p.Estado == EstadoPrestamo.Activo);
    }

    public int TotalDevueltos()
    {
        return prestamos.Count(p => p.Estado == EstadoPrestamo.Devuelto);
    }

    public int TotalVencidos()
    {
        return prestamos.Count(p => p.Estado == EstadoPrestamo.Vencido);
    }

    public double PromedioDiasPrestamo()
    {
        if (prestamos.Count == 0)
            return 0;

        return prestamos.Average(p => p.DiasTranscurridos());
    }
}
