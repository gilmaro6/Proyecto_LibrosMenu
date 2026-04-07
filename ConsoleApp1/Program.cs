namespace ConsoleApp1;

using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Models;
using ConsoleApp1.Services;

class Program
{
    // ===== Services (memoria) =====
    static LibroService libroService = new LibroService();
    static UsuarioService usuarioService = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    static int nextPrestamoId = 1;

    // ===== “Guardar/Cargar” en memoria (snapshot) =====
    static List<Libro>? snapLibros = null;
    static List<Usuario>? snapUsuarios = null;
    static List<PrestamoSnap>? snapPrestamos = null;

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
            Console.WriteLine("6. Comparar Arrays vs List");
            Console.WriteLine("7. Pruebas Services / KPIs");
            Console.WriteLine("8. Salir");
            Console.Write("Seleccione una opción: ");

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": LibrosMenu(); break;
                case "2": UsuariosMenu(); break;
                case "3": PrestamosMenu(); break;
                case "4": BusquedasReportesMenu(); break;   // Parte 2
                case "5": GuardarCargarMenu(); break;       // Parte 2
                case "6": CompararArrayVsList(); break;     // Parte 2
                case "7": PruebaServices(); break;          // Parte 2
                case "8":
                    PreguntarGuardarAntesDeSalir(); 
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    Pause();
                    break;
            }
        }
    }

    // ===================== MENÚ LIBROS =====================
    static void LibrosMenu()
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

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": RegistrarLibro(); break;
                case "2": ListarLibrosMenu(); break;
                case "3": DetallesLibro(); break;
                case "4": ActualizarLibro(); break;
                case "5": EliminarLibro(); break;
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

        if (libroService.BuscarPorISBN(isbn) != null)
        {
            Console.WriteLine("Ya existe un libro con ese ISBN.");
            Pause();
            return;
        }

        libroService.Agregar(new Libro(id, isbn, titulo, autor, anio, categoria, true));
        Console.WriteLine("Libro registrado correctamente.");
        Pause();
    }

    static void ListarLibrosMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Libros ===");
        Console.WriteLine("1. Todos");
        Console.WriteLine("2. Disponibles");
        Console.WriteLine("3. Prestados");
        Console.Write("Seleccione una opción: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1": ListarLibros(todos: true, disponibles: false); break;
            case "2": ListarLibros(todos: false, disponibles: true); break;
            case "3": ListarLibros(todos: false, disponibles: false); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
        Pause();
    }

    static void ListarLibros(bool todos, bool disponibles)
    {
        var list = libroService.ObtenerTodos();

        if (!todos)
        {
            list = disponibles
                ? list.Where(l => l.Disponible).ToList()
                : list.Where(l => !l.Disponible).ToList();
        }

        Console.Clear();
        Console.WriteLine("=== Resultado ===");

        if (list.Count == 0)
        {
            Console.WriteLine("No hay registros para mostrar.");
            return;
        }

        foreach (var l in list)
            Console.WriteLine(l.ResumenCorto());
    }

    static void DetallesLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Libro (ISBN) ===");
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

    static void ActualizarLibro()
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
        Console.WriteLine("3. Editar año");
        Console.WriteLine("4. Editar categoría");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1": libro.Titulo = ReadNonEmpty("Nuevo título: "); break;
            case "2": libro.Autor = ReadNonEmpty("Nuevo autor: "); break;
            case "3": libro.Anio = ReadInt("Nuevo año: "); break;
            case "4": libro.Categoria = ReadNonEmpty("Nueva categoría: "); break;
            default: Console.WriteLine("Opción inválida."); Pause(); return;
        }

        Console.WriteLine("Libro actualizado.");
        Pause();
    }

    static void EliminarLibro()
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
    static void UsuariosMenu()
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

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": RegistrarUsuario(); break;
                case "2": ListarUsuarios(); break;
                case "3": DetallesUsuario(); break;
                case "4": ActualizarUsuario(); break;
                case "5": EliminarUsuario(); break;
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

        if (usuarioService.BuscarPorDocumento(doc) != null)
        {
            Console.WriteLine("Ya existe un usuario con ese documento.");
            Pause();
            return;
        }

        usuarioService.Agregar(new Usuario(id, doc, nombre, contacto, true));
        Console.WriteLine("Usuario registrado.");
        Pause();
    }

    static void ListarUsuarios()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Usuarios ===");

        var list = usuarioService.ObtenerTodos();
        if (list.Count == 0)
        {
            Console.WriteLine("No hay usuarios.");
            Pause();
            return;
        }

        foreach (var u in list)
            Console.WriteLine(u.ResumenCorto());

        Pause();
    }

    static void DetallesUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Usuario (Documento) ===");
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

    static void ActualizarUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Actualizar Usuario ===");
        string doc = ReadNonEmpty("Documento: ");

        var u = usuarioService.BuscarPorDocumento(doc);
        if (u == null)
        {
            Console.WriteLine("Usuario no encontrado.");
            Pause();
            return;
        }

        Console.WriteLine("1. Editar nombre");
        Console.WriteLine("2. Editar contacto");
        Console.WriteLine("3. Activar/Desactivar");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1": u.Nombre = ReadNonEmpty("Nuevo nombre: "); break;
            case "2": u.Contacto = ReadNonEmpty("Nuevo contacto: "); break;
            case "3":
                Console.Write("¿Activar usuario? (S/N): ");
                string op = (Console.ReadLine() ?? "").Trim().ToUpper();
                u.Activo = (op == "S");
                break;
            default:
                Console.WriteLine("Opción inválida.");
                Pause();
                return;
        }

        Console.WriteLine("Usuario actualizado.");
        Pause();
    }

    static void EliminarUsuario()
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

        // Regla: no eliminar si tiene préstamos activos
        bool tieneActivos = prestamoService.ObtenerTodos()
            .Any(p => p.Usuario != null && p.Usuario.Documento == doc && p.Estado == EstadoPrestamo.Activo);

        if (tieneActivos)
        {
            Console.WriteLine("No se puede eliminar: usuario tiene préstamos activos.");
            Pause();
            return;
        }

        usuarioService.Eliminar(u);
        Console.WriteLine("Usuario eliminado.");
        Pause();
    }

    // ===================== MENÚ PRÉSTAMOS =====================
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
            Console.WriteLine("4. Registrar devolución");     // commit 2 no implementa
            Console.WriteLine("5. Eliminar préstamo");        // commit 2 no implementa
            Console.WriteLine("6. Volver");
            Console.Write("Seleccione una opción: ");

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": CrearPrestamoBasico(); break;
                case "2": ListarPrestamosBasico(); break;
                case "3": DetallesPrestamo(); break;
                case "4": RegistrarDevolucion(); break;
                case "5": EliminarPrestamo(); break;
                case "6": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void CrearPrestamoBasico()
    {
        Console.Clear();
        Console.WriteLine("=== Crear Préstamo ===");

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

        libro.Disponible = false;
        prestamoService.Agregar(prestamo);

        Console.WriteLine("Préstamo creado.");
        Pause();
    }

    static void ListarPrestamosBasico()
    {
        Console.Clear();
        Console.WriteLine("=== Listar Préstamos ===");

        var list = prestamoService.ObtenerTodos();
        if (list.Count == 0)
        {
            Console.WriteLine("No hay préstamos.");
            Pause();
            return;
        }

        foreach (var p in list)
            Console.WriteLine(p.ResumenCorto());

        Pause();
    }

    static void DetallesPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== Detalles del Préstamo ===");
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

    
    static void BusquedasReportesMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Búsquedas y Reportes ===");
            Console.WriteLine("1. Buscar Libro");
            Console.WriteLine("2. Buscar Usuario");
            Console.WriteLine("3. Buscar Préstamo");
            Console.WriteLine("4. Reportes / KPIs");
            Console.WriteLine("5. Volver");
            Console.Write("Seleccione: ");

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": BuscarLibroMenu(); break;
                case "2": BuscarUsuarioMenu(); break;
                case "3": BuscarPrestamoMenu(); break;
                case "4": ReportesKpisMenu(); break;
                case "5": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void BuscarLibroMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro ===");
        Console.WriteLine("1. Por ISBN");
        Console.WriteLine("2. Por Título (contiene)");
        Console.WriteLine("3. Por Autor (contiene)");
        Console.WriteLine("4. Volver");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1":
            {
                string isbn = ReadNonEmpty("ISBN: ");
                var l = libroService.BuscarPorISBN(isbn);
                if (l == null) Console.WriteLine("No encontrado.");
                else Console.WriteLine(l.DetalleCompleto());
                Pause();
                break;
            }
            case "2":
            {
                string t = ReadNonEmpty("Título (parte): ");
                var list = libroService.BuscarPorTitulo(t);
                Console.WriteLine($"Resultados: {list.Count}");
                foreach (var l in list) Console.WriteLine(l.ResumenCorto());
                Pause();
                break;
            }
            case "3":
            {
                string a = ReadNonEmpty("Autor (parte): ");
                var list = libroService.BuscarPorAutor(a);
                Console.WriteLine($"Resultados: {list.Count}");
                foreach (var l in list) Console.WriteLine(l.ResumenCorto());
                Pause();
                break;
            }
            default:
                break;
        }
    }

    static void BuscarUsuarioMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Usuario ===");
        Console.WriteLine("1. Por Documento");
        Console.WriteLine("2. Por Nombre (contiene)");
        Console.WriteLine("3. Volver");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1":
            {
                string doc = ReadNonEmpty("Documento: ");
                var u = usuarioService.BuscarPorDocumento(doc);
                if (u == null) Console.WriteLine("No encontrado.");
                else Console.WriteLine(u.DetalleCompleto());
                Pause();
                break;
            }
            case "2":
            {
                string n = ReadNonEmpty("Nombre (parte): ");
                var list = usuarioService.BuscarPorNombre(n);
                Console.WriteLine($"Resultados: {list.Count}");
                foreach (var u in list) Console.WriteLine(u.ResumenCorto());
                Pause();
                break;
            }
            default:
                break;
        }
    }

    static void BuscarPrestamoMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Préstamo ===");
        Console.WriteLine("1. Por ID");
        Console.WriteLine("2. Por Estado");
        Console.WriteLine("3. Volver");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1":
            {
                int id = ReadInt("Id: ");
                var p = prestamoService.BuscarPorId(id);
                if (p == null) Console.WriteLine("No encontrado.");
                else Console.WriteLine(p.DetalleCompleto());
                Pause();
                break;
            }
            case "2":
            {
                var estado = ReadEstado();
                var list = prestamoService.BuscarPorEstado(estado);
                Console.WriteLine($"Resultados: {list.Count}");
                foreach (var p in list) Console.WriteLine(p.ResumenCorto());
                Pause();
                break;
            }
            default:
                break;
        }
    }

    static void ReportesKpisMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Reportes / KPIs ===");

        // Libros
        Console.WriteLine("---- Libros ----");
        Console.WriteLine($"Total: {libroService.TotalLibros()}");
        Console.WriteLine($"Disponibles: {libroService.TotalDisponibles()}");
        Console.WriteLine($"Prestados: {libroService.TotalPrestados()}");
        Console.WriteLine();

        // Usuarios
        Console.WriteLine("---- Usuarios ----");
        Console.WriteLine($"Total: {usuarioService.TotalUsuarios()}");
        Console.WriteLine($"Activos: {usuarioService.TotalActivos()}");
        Console.WriteLine($"Inactivos: {usuarioService.TotalInactivos()}");
        Console.WriteLine();

        // Préstamos
        Console.WriteLine("---- Préstamos ----");
        Console.WriteLine($"Total: {prestamoService.TotalPrestamos()}");
        Console.WriteLine($"Activos: {prestamoService.TotalActivos()}");
        Console.WriteLine($"Devueltos: {prestamoService.TotalDevueltos()}");
        Console.WriteLine($"Vencidos: {prestamoService.TotalVencidos()}");
        Console.WriteLine($"Promedio días: {prestamoService.PromedioDiasPrestamo():0.00}");
        Console.WriteLine();

        Console.WriteLine("Ordenación rápida:");
        Console.WriteLine("1. Libros por Título");
        Console.WriteLine("2. Usuarios por Nombre");
        Console.WriteLine("3. Préstamos por Fecha límite");
        Console.WriteLine("4. Volver");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1":
            {
                var list = libroService.OrdenarPorTitulo();
                foreach (var l in list) Console.WriteLine(l.ResumenCorto());
                Pause();
                break;
            }
            case "2":
            {
                var list = usuarioService.OrdenarPorNombre();
                foreach (var u in list) Console.WriteLine(u.ResumenCorto());
                Pause();
                break;
            }
            case "3":
            {
                var list = prestamoService.OrdenarPorFechaLimite();
                foreach (var p in list) Console.WriteLine(p.ResumenCorto());
                Pause();
                break;
            }
            default:
                break;
        }
    }

    // ===================== GUARDAR / CARGAR (SIMULADO EN MEMORIA) =====================
    static void GuardarCargarMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Guardar / Cargar Datos (Memoria) ===");
            Console.WriteLine("1. Guardar (snapshot)");
            Console.WriteLine("2. Cargar (restore snapshot)");
            Console.WriteLine("3. Reiniciar datos (vaciar y seed)");
            Console.WriteLine("4. Volver");
            Console.Write("Seleccione: ");

            switch ((Console.ReadLine() ?? "").Trim())
            {
                case "1": GuardarSnapshot(); break;
                case "2": CargarSnapshot(); break;
                case "3": ReiniciarDatos(); break;
                case "4": volver = true; break;
                default: Console.WriteLine("Opción inválida."); Pause(); break;
            }
        }
    }

    static void GuardarSnapshot()
    {
        snapLibros = libroService.ObtenerTodos().Select(CloneLibro).ToList();
        snapUsuarios = usuarioService.ObtenerTodos().Select(CloneUsuario).ToList();
        snapPrestamos = prestamoService.ObtenerTodos().Select(p => PrestamoSnap.FromPrestamo(p)).ToList();

        Console.WriteLine("✅ Snapshot guardado en memoria.");
        Pause();
    }

    static void CargarSnapshot()
    {
        if (snapLibros == null || snapUsuarios == null || snapPrestamos == null)
        {
            Console.WriteLine("No hay snapshot guardado.");
            Pause();
            return;
        }

        // Reset services (sin agregar archivos nuevos)
        libroService = new LibroService();
        usuarioService = new UsuarioService();
        prestamoService = new PrestamoService();

        foreach (var l in snapLibros) libroService.Agregar(CloneLibro(l));
        foreach (var u in snapUsuarios) usuarioService.Agregar(CloneUsuario(u));

        int maxId = 0;
        foreach (var ps in snapPrestamos)
        {
            var libro = libroService.BuscarPorISBN(ps.IsbnLibro) ?? new Libro(0, ps.IsbnLibro, "DESCONOCIDO", "DESCONOCIDO", 0, "NA", true);
            var usuario = usuarioService.BuscarPorDocumento(ps.DocUsuario) ?? new Usuario(0, ps.DocUsuario, "DESCONOCIDO", "NA", true);

            var p = new Prestamo(ps.Id, libro, usuario, ps.FechaPrestamo, ps.FechaVencimiento);
            p.Estado = ps.Estado;
            p.FechaDevolucion = ps.FechaDevolucion;

            // Si el préstamo está activo, el libro debe estar prestado
            if (p.Estado == EstadoPrestamo.Activo) libro.Disponible = false;

            prestamoService.Agregar(p);
            if (p.Id > maxId) maxId = p.Id;
        }

        nextPrestamoId = Math.Max(nextPrestamoId, maxId + 1);

        Console.WriteLine("✅ Snapshot cargado.");
        Pause();
    }

    static void ReiniciarDatos()
    {
        Console.Write("¿Seguro que desea reiniciar? (S/N): ");
        string op = (Console.ReadLine() ?? "").Trim().ToUpper();
        if (op != "S")
        {
            Console.WriteLine("Operación cancelada.");
            Pause();
            return;
        }

        libroService = new LibroService();
        usuarioService = new UsuarioService();
        prestamoService = new PrestamoService();
        nextPrestamoId = 1;

        SeedDataSiVacio();
        Console.WriteLine("✅ Datos reiniciados.");
        Pause();
    }

    static void PreguntarGuardarAntesDeSalir()
    {
        Console.Clear();
        Console.WriteLine("=== Salida del Programa ===");
        Console.Write("¿Desea guardar (snapshot) antes de salir? (S/N): ");
        string op = (Console.ReadLine() ?? "").Trim().ToUpper();
        if (op == "S") GuardarSnapshot();
    }

    // ===================== ARRAYS VS LIST =====================
    static void CompararArrayVsList()
    {
        Console.Clear();
        Console.WriteLine("=== Comparación Array vs List ===");

        int[] arrayEjemplo = new int[3] { 1, 2, 3 };
        // arrayEjemplo[3] = 4; // ❌ Error: tamaño fijo

        List<int> listaEjemplo = new List<int> { 1, 2, 3 };
        listaEjemplo.Add(4); // ✅ tamaño dinámico

        Console.WriteLine($"Array size: {arrayEjemplo.Length}");
        Console.WriteLine($"List size: {listaEjemplo.Count}");
        Console.WriteLine("Array: tamaño fijo / List: tamaño dinámico (crece con Add).");
        Pause();
    }

    // ===================== PRUEBAS SERVICES / KPIs =====================
    static void PruebaServices()
    {
        Console.Clear();
        Console.WriteLine("=== Pruebas Services / KPIs ===");

        Console.WriteLine($"Total Libros: {libroService.TotalLibros()}");
        Console.WriteLine($"Total Usuarios: {usuarioService.TotalUsuarios()}");
        Console.WriteLine($"Total Prestamos: {prestamoService.TotalPrestamos()}");
        Console.WriteLine($"Prestamos Activos: {prestamoService.TotalActivos()}");
        Console.WriteLine($"Prestamos Devueltos: {prestamoService.TotalDevueltos()}");
        Console.WriteLine($"Prestamos Vencidos: {prestamoService.TotalVencidos()}");
        Console.WriteLine($"Promedio días préstamo: {prestamoService.PromedioDiasPrestamo():0.00}");

        Console.WriteLine();
        Console.WriteLine("Ordenar préstamos por fecha límite:");
        foreach (var p in prestamoService.OrdenarPorFechaLimite())
            Console.WriteLine(p.ResumenCorto());

        Pause();
    }

    // ===================== PRÉSTAMOS COMPLETOS (para conectar opciones 4/5) =====================
    static void RegistrarDevolucion()
    {
        Console.Clear();
        Console.WriteLine("=== Registrar Devolución ===");
        int id = ReadInt("Id préstamo: ");

        var p = prestamoService.BuscarPorId(id);
        if (p == null)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Pause();
            return;
        }

        if (p.Estado != EstadoPrestamo.Activo)
        {
            Console.WriteLine("El préstamo no está activo (ya fue devuelto o vencido).");
            Pause();
            return;
        }

        p.Estado = EstadoPrestamo.Devuelto;
        p.FechaDevolucion = DateTime.Now;

        if (p.Libro != null)
            p.Libro.Disponible = true;

        Console.WriteLine("Devolución registrada correctamente.");
        Pause();
    }

    static void EliminarPrestamo()
    {
        Console.Clear();
        Console.WriteLine("=== Eliminar Préstamo ===");
        int id = ReadInt("Id préstamo: ");

        var p = prestamoService.BuscarPorId(id);
        if (p == null)
        {
            Console.WriteLine("Préstamo no encontrado.");
            Pause();
            return;
        }

        // Regla: si está activo, al eliminarlo devolver libro
        if (p.Estado == EstadoPrestamo.Activo && p.Libro != null)
            p.Libro.Disponible = true;

        prestamoService.Eliminar(p);
        Console.WriteLine("Préstamo eliminado.");
        Pause();
    }

    static EstadoPrestamo ReadEstado()
    {
        Console.WriteLine("Estado:");
        Console.WriteLine("1. Activo");
        Console.WriteLine("2. Devuelto");
        Console.WriteLine("3. Vencido");
        Console.Write("Seleccione: ");

        switch ((Console.ReadLine() ?? "").Trim())
        {
            case "1": return EstadoPrestamo.Activo;
            case "2": return EstadoPrestamo.Devuelto;
            case "3": return EstadoPrestamo.Vencido;
            default: return EstadoPrestamo.Activo;
        }
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

    // ===================== SEED =====================
    static void SeedDataSiVacio()
    {
        if (libroService.TotalLibros() > 0 || usuarioService.TotalUsuarios() > 0 || prestamoService.TotalPrestamos() > 0)
            return;

        libroService.Agregar(CloneLibro(ModelsTesting.Libro1));
        libroService.Agregar(CloneLibro(ModelsTesting.Libro2));

        usuarioService.Agregar(CloneUsuario(ModelsTesting.Usuario1));
        usuarioService.Agregar(CloneUsuario(ModelsTesting.Usuario2));

        nextPrestamoId = Math.Max(nextPrestamoId, ModelsTesting.Prestamo1.Id + 1);

        // Prestamo test
        var l2 = libroService.BuscarPorISBN(ModelsTesting.Libro2.ISBN);
        var u1 = usuarioService.BuscarPorDocumento(ModelsTesting.Usuario1.Documento);
        if (l2 != null && u1 != null)
        {
            l2.Disponible = false;
            prestamoService.Agregar(new Prestamo(ModelsTesting.Prestamo1.Id, l2, u1, ModelsTesting.Prestamo1.FechaPrestamo, ModelsTesting.Prestamo1.FechaVencimiento));
        }
    }

    // ===================== CLONES (evita referencias raras) =====================
    static Libro CloneLibro(Libro l) => new Libro(l.Id, l.ISBN, l.Titulo, l.Autor, l.Anio, l.Categoria, l.Disponible);
    static Usuario CloneUsuario(Usuario u) => new Usuario(u.Id, u.Documento, u.Nombre, u.Contacto, u.Activo);

    // ===================== SNAP PARA PRÉSTAMOS =====================
    class PrestamoSnap
    {
        public int Id { get; set; }
        public string IsbnLibro { get; set; } = "";
        public string DocUsuario { get; set; } = "";
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public EstadoPrestamo Estado { get; set; }

        public static PrestamoSnap FromPrestamo(Prestamo p)
        {
            return new PrestamoSnap
            {
                Id = p.Id,
                IsbnLibro = p.Libro?.ISBN ?? "",
                DocUsuario = p.Usuario?.Documento ?? "",
                FechaPrestamo = p.FechaPrestamo,
                FechaVencimiento = p.FechaVencimiento,
                FechaDevolucion = p.FechaDevolucion,
                Estado = p.Estado
            };
        }
    }
}


