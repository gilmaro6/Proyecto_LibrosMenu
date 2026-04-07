namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

class Program
{
    // ===== Services en memoria (sin BD) =====
    static LibroService libroService = new LibroService();
    static UsuarioService usuarioService = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();
    static int nextPrestamoId = 1;

    static void Main(string[] args)
    {
        SeedDataSiVacio();

        bool salir = false;
        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("===== SISTEMA BIBLIOTECA =====");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1": librosMenu(); break;
                case "2": usuariosMenu(); break;    // aún básico en commit 1
                case "3": PrestamosMenu(); break;   // aún básico en commit 1
                case "4": BusquedasReportes(); break;
                case "5": GuardarCargarMenu(); break;
                case "6": PreguntarGuardarAntesDeSalir(); salir = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    // ===================== MENÚ LIBROS =====================
    static void librosMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Libros ===");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Detalles por ISBN");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Volver");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1": RegistrarLibro(); break;
                case "2": ListarLibros(); break;
                case "3": detallesLibro(); break;
                case "4": actualizarLibro(); break;
                case "5": eliminarLibro(); break;
                case "6": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void RegistrarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Registrar Libro ===");

        int id = ReadInt("Id (número): ");
        string isbn = ReadNonEmpty("ISBN: ");
        string titulo = ReadNonEmpty("Título: ");
        string autor = ReadNonEmpty("Autor: ");
        int anio = ReadInt("Año: ");
        string categoria = ReadNonEmpty("Categoría: ");

        Libro? existente = libroService.BuscarPorISBN(isbn);
        if (existente != null)
        {
            Console.WriteLine("Ya existe un libro con ese ISBN.");
            Pause();
            return;
        }

        libroService.Agregar(new Libro(id, isbn, titulo, autor, anio, categoria, true));
        Console.WriteLine("Libro registrado correctamente.");
        Pause();
    }

    static void ListarLibros()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Libros ===");
        Console.WriteLine("1. Todos");
        Console.WriteLine("2. Disponibles");
        Console.WriteLine("3. Prestados");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1": listarTodos(); break;
            case "2": listarDisponibles(); break;
            case "3": listarPrestados(); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
        Pause();
    }

    static void listarTodos()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Todos los Libros ===");

        var todos = libroService.ObtenerTodos();
        if (todos.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }

        foreach (var l in todos)
            Console.WriteLine(l.ResumenCorto());
    }

    static void listarDisponibles()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Libros Disponibles ===");

        var disponibles = libroService.ObtenerTodos().Where(l => l.Disponible).ToList();
        if (disponibles.Count == 0)
        {
            Console.WriteLine("No hay libros disponibles.");
            return;
        }

        foreach (var l in disponibles)
            Console.WriteLine(l.ResumenCorto());
    }

    static void listarPrestados()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Libros Prestados ===");

        var prestados = libroService.ObtenerTodos().Where(l => !l.Disponible).ToList();
        if (prestados.Count == 0)
        {
            Console.WriteLine("No hay libros prestados.");
            return;
        }

        foreach (var l in prestados)
            Console.WriteLine(l.ResumenCorto());
    }

    static void detallesLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Libro (por ISBN) ===");

        string isbn = ReadNonEmpty("ISBN: ");
        Libro? libro = libroService.BuscarPorISBN(isbn);

        if (libro == null)
        {
            Console.WriteLine("Libro no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine(libro.DetalleCompleto());
        Pause();
    }

    static void actualizarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Actualizar Libro ===");

        string isbn = ReadNonEmpty("ISBN del libro: ");
        Libro? libro = libroService.BuscarPorISBN(isbn);

        if (libro == null)
        {
            Console.WriteLine("Libro no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine("1. Editar título");
        Console.WriteLine("2. Editar autor");
        Console.WriteLine("3. Editar año/categoría");
        Console.Write("Seleccione: ");

        switch (Console.ReadLine())
        {
            case "1":
                libro.Titulo = ReadNonEmpty("Nuevo título: ");
                Console.WriteLine("Título actualizado.");
                break;
            case "2":
                libro.Autor = ReadNonEmpty("Nuevo autor: ");
                Console.WriteLine("Autor actualizado.");
                break;
            case "3":
                libro.Anio = ReadInt("Nuevo año: ");
                libro.Categoria = ReadNonEmpty("Nueva categoría: ");
                Console.WriteLine("Año/Categoría actualizados.");
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pause();
    }

    static void eliminarLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Eliminar Libro ===");

        string isbn = ReadNonEmpty("ISBN a eliminar: ");
        Libro? libro = libroService.BuscarPorISBN(isbn);

        if (libro == null)
        {
            Console.WriteLine("Libro no encontrado.");
            Pause();
            return;
        }

        if (!libro.Disponible)
        {
            Console.WriteLine("No se permite eliminar: el libro está prestado.");
            Pause();
            return;
        }

        libroService.Eliminar(libro);
        Console.WriteLine("Libro eliminado.");
        Pause();
    }

    // ===================== MENÚ USUARIOS (placeholder commit 1) =====================
    static void usuariosMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Menú de Usuarios ===");
        Console.WriteLine("Funcionalidad completa en el Commit 2.");
        Pause();
    }

    // ===================== MENÚ PRÉSTAMOS (placeholder commit 1) =====================
    static void PrestamosMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Menú de Préstamos ===");
        Console.WriteLine("Funcionalidad completa en el Commit 3.");
        Pause();
    }

    static void BusquedasReportes()
    {
        Console.Clear();
        Console.WriteLine("=== Búsquedas y reportes ===");
        Console.WriteLine("Funcionalidad completa en el Commit 3.");
        Pause();
    }

    static void GuardarCargarMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Guardar / Cargar datos ===");
        Console.WriteLine("Sin persistencia (solo memoria).");
        Pause();
    }

    static void PreguntarGuardarAntesDeSalir()
    {
        Console.Clear();
        Console.WriteLine("=== Salida ===");
        Console.WriteLine("Saliendo...");
        Pause();
    }

    // ===================== HELPERS =====================
    static string ReadNonEmpty(string label)
    {
        string? input;
        do
        {
            Console.Write(label);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input.Trim();
    }

    static int ReadInt(string label)
    {
        while (true)
        {
            Console.Write(label);
            var input = Console.ReadLine();
            if (int.TryParse(input, out int value)) return value;
            Console.WriteLine("Entrada inválida. Debe ser un número.");
        }
    }

    static void Pause(string msg = "Presione una tecla para continuar...")
    {
        Console.WriteLine(msg);
        Console.ReadKey();
    }

    static void SeedDataSiVacio()
    {
        // Evita duplicar en cada ejecución
        if (libroService.TotalLibros() > 0 || usuarioService.TotalUsuarios() > 0 || prestamoService.TotalPrestamos() > 0)
            return;

        libroService.Agregar(ModelsTesting.Libro1);
        libroService.Agregar(ModelsTesting.Libro2);

        usuarioService.Agregar(ModelsTesting.Usuario1);
        usuarioService.Agregar(ModelsTesting.Usuario2);

        nextPrestamoId = Math.Max(nextPrestamoId, ModelsTesting.Prestamo1.Id + 1);
        prestamoService.Agregar(ModelsTesting.Prestamo1);
    }
}
