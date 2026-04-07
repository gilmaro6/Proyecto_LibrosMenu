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
                case "2": usuariosMenu(); break;
                case "3": PrestamosMenu(); break;
                case "4": BusquedasReportes(); break;       // commit 3
                case "5": GuardarCargarMenu(); break;      // sin persistencia real
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

        var existente = libroService.BuscarPorISBN(isbn);
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
        var libro = libroService.BuscarPorISBN(isbn);

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
        var libro = libroService.BuscarPorISBN(isbn);

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
        var libro = libroService.BuscarPorISBN(isbn);

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

    // ===================== MENÚ USUARIOS =====================
    static void usuariosMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Usuarios ===");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Detalles por Documento");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("6. Volver");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1": RegistrarUsuario(); break;
                case "2": ListarUsuarios(); break;
                case "3": detallesUsuario(); break;
                case "4": actualizarUsuario(); break;
                case "5": eliminarUsuario(); break;
                case "6": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void RegistrarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Registrar Usuario ===");

        int id = ReadInt("Id (número): ");
        string doc = ReadNonEmpty("Documento: ");
        string nombre = ReadNonEmpty("Nombre: ");
        string contacto = ReadNonEmpty("Contacto: ");

        var existente = usuarioService.BuscarPorDocumento(doc);
        if (existente != null)
        {
            Console.WriteLine("Ya existe un usuario con ese documento.");
            Pause();
            return;
        }

        usuarioService.Agregar(new Usuario(id, doc, nombre, contacto, true));
        Console.WriteLine("Usuario registrado correctamente.");
        Pause();
    }

    static void ListarUsuarios()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Usuarios ===");

        var todos = usuarioService.ObtenerTodos();
        if (todos.Count == 0)
        {
            Console.WriteLine("No hay usuarios registrados.");
            Pause();
            return;
        }

        foreach (var u in todos)
            Console.WriteLine(u.ResumenCorto());

        Pause();
    }

    static void detallesUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Usuario (por Documento) ===");

        string doc = ReadNonEmpty("Documento: ");
        var u = usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine(u.DetalleCompleto());
        Pause();
    }

    static void actualizarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Actualizar Usuario ===");

        string doc = ReadNonEmpty("Documento del usuario: ");
        var u = usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine("1. Editar nombre");
        Console.WriteLine("2. Editar contacto");
        Console.WriteLine("3. Activar/desactivar");
        Console.Write("Seleccione: ");

        switch (Console.ReadLine())
        {
            case "1":
                u.Nombre = ReadNonEmpty("Nuevo nombre: ");
                Console.WriteLine("Nombre actualizado.");
                break;
            case "2":
                u.Contacto = ReadNonEmpty("Nuevo contacto: ");
                Console.WriteLine("Contacto actualizado.");
                break;
            case "3":
                Console.Write("¿Activar usuario? (S/N): ");
                string op = (Console.ReadLine() ?? "").Trim().ToUpper();
                u.Activo = (op == "S");
                Console.WriteLine("Estado actualizado.");
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Pause();
    }

    static void eliminarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Eliminar Usuario ===");

        string doc = ReadNonEmpty("Documento a eliminar: ");
        var u = usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            Pause();
            return;
        }

        // Regla: no eliminar si tiene préstamos activos (en commit 3 será más completo)
        bool tieneActivos = prestamoService.ObtenerTodos()
            .Any(p => p.Usuario != null && p.Usuario.Documento == doc && p.Estado == EstadoPrestamo.Activo);

        if (tieneActivos)
        {
            Console.WriteLine("No se puede eliminar: el usuario tiene préstamos activos.");
            Pause();
            return;
        }

        usuarioService.Eliminar(u);
        Console.WriteLine("Usuario eliminado.");
        Pause();
    }

    // ===================== MENÚ PRÉSTAMOS (BÁSICO) =====================
    static void PrestamosMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Préstamos ===");
            Console.WriteLine("1. Crear préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Ver detalles por ID");
            Console.WriteLine("4. Registrar devolución (commit 3)");
            Console.WriteLine("5. Eliminar préstamo (commit 3)");
            Console.WriteLine("6. Volver");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1": RegistrarPrestamo(); break;
                case "2": ListarPrestamos(); break;
                case "3": detallesPrestamo(); break;
                case "4": Console.WriteLine("Pendiente commit 3."); Pause(); break;
                case "5": Console.WriteLine("Pendiente commit 3."); Pause(); break;
                case "6": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void RegistrarPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== Crear Préstamo (básico) ===");

        string doc = ReadNonEmpty("Documento del usuario: ");
        var usuario = usuarioService.BuscarPorDocumento(doc);
        if (usuario == null || !usuario.Activo)
        {
            Console.WriteLine("Usuario no existe o está inactivo.");
            Pause();
            return;
        }

        string isbn = ReadNonEmpty("ISBN del libro: ");
        var libro = libroService.BuscarPorISBN(isbn);
        if (libro == null || !libro.Disponible)
        {
            Console.WriteLine("Libro no existe o no está disponible.");
            Pause();
            return;
        }

        var hoy = DateTime.Now;
        var venc = hoy.AddDays(8);

        var prestamo = new Prestamo(nextPrestamoId++, libro, usuario, hoy, venc);
        prestamo.Estado = EstadoPrestamo.Activo;
        prestamo.FechaDevolucion = null;

        // marcar libro como prestado
        libro.Disponible = false;

        prestamoService.Agregar(prestamo);
        Console.WriteLine("Préstamo creado correctamente.");
        Pause();
    }

    static void ListarPrestamos()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Préstamos ===");

        var todos = prestamoService.ObtenerTodos();
        if (todos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
            Pause();
            return;
        }

        foreach (var p in todos)
            Console.WriteLine(p.ResumenCorto());

        Pause();
    }

    static void detallesPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Préstamo por ID ===");

        int id = ReadInt("Id préstamo: ");
        var p = prestamoService.BuscarPorId(id);

        if (p == null)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine(p.DetalleCompleto());
        Pause();
    }

    // ===================== OTROS (placeholder commit 2) =====================
    static void BusquedasReportes()
    {
        Console.Clear();
        Console.WriteLine("=== Búsquedas y Reportes ===");
        Console.WriteLine("Se completa en el Commit 3 (KPIs + ordenaciones + búsquedas).");
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
