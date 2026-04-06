using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class UsuarioService
{
    private readonly List<Usuario> usuarios = new List<Usuario>();

    public void Agregar(Usuario usuario)
    {
        usuarios.Add(usuario);
    }

    public bool Eliminar(Usuario usuario)
    {
        return usuarios.Remove(usuario);
    }

    public List<Usuario> ObtenerTodos()
    {
        return new List<Usuario>(usuarios);
    }

    public Usuario BuscarPorDocumento(string documento)
    {
        return usuarios.FirstOrDefault(u => u.Documento == documento);
    }

    public List<Usuario> BuscarPorNombre(string nombre)
    {
        return usuarios.Where(u => u.Nombre.Contains(nombre)).ToList();
    }

    public List<Usuario> OrdenarPorNombre()
    {
        return usuarios.OrderBy(u => u.Nombre).ToList();
    }

    public int TotalUsuarios()
    {
        return usuarios.Count;
    }

    public int TotalActivos()
    {
        return usuarios.Count(u => u.Activo);
    }

    public int TotalInactivos()
    {
        return usuarios.Count(u => !u.Activo);
    }
}
