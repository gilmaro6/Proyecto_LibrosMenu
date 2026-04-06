using System.Collections.Generic;
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
}
