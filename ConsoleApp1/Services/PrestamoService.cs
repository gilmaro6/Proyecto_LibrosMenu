using System.Collections.Generic;
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
}
