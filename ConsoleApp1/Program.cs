namespace ConsoleApp1;
using ConsoleApp1.Models;
using ConsoleApp1.Services;
class Program
{
    // ===== Services (memoria) compartidos para menú funcional =====
    static LibroService libroService = new LibroService();
    static UsuarioService usuarioService = new UsuarioService();
    static PrestamoService prestamoService = new PrestamoService();

    static int nextPrestamoId = 1;
    
static void Main(string[] args)
{
    SeedDataSiVacio();
    // Datos de prueba EV08
libroService.Agregar(ModelsTesting.Libro1);
libroService.Agregar(ModelsTesting.Libro2);

usuarioService.Agregar(ModelsTesting.Usuario1);
usuarioService.Agregar(ModelsTesting.Usuario2);

prestamoService.Agregar(ModelsTesting.Prestamo1);

// Pruebas de búsquedas
var libroBuscado = libroService.BuscarPorISBN("ISBN-001");
var usuariosPorNombre = usuarioService.BuscarPorNombre("Juan");
var prestamosActivos = prestamoService.BuscarPorEstado(EstadoPrestamo.Activo);

// KPIs EV08
Console.WriteLine($"Total Libros: {libroService.TotalLibros()}");
Console.WriteLine($"Total Usuarios: {usuarioService.TotalUsuarios()}");
Console.WriteLine($"Total Prestamos: {prestamoService.TotalPrestamos()}");

// Comparación Array vs List EV08
int[] arrayEjemplo = new int[3] { 1, 2, 3 };
// arrayEjemplo[3] = 4; ❌ Error tamaño fijo

List<int> listaEjemplo = new List<int> { 1, 2, 3 };
listaEjemplo.Add(4); // ✅ tamaño dinámico

Console.WriteLine($"Array size: {arrayEjemplo.Length}");
Console.WriteLine($"List size: {listaEjemplo.Count}");
    bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("=== Menú Principal ===");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1": 
                    librosMenu();
                    break;
                case "2":
                    usuariosMenu();
                    break; 
                case "3":
                    PrestamosMenu(); 
                    break;
                case "4": 
                    BusquedasReportes ();
                    break;
                case "5": 
                    GuardarCargarMenu();
                    break;
                case "6":
                    PreguntarGuardarAntesDeSalir();
                    Console.Write("saliendo del programa...");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla...");
                    Console.ReadKey();
                    break;
            }
        }

    }
    static void librosMenu()
    {
        bool volver = false;
        while (!volver)        {
        Console.Clear();
        Console.WriteLine("=== Menú de Libros ===");
        Console.WriteLine("1. Registrar libro");
        Console.WriteLine("2. Listar libros");
        Console.WriteLine("3. detalles por ID/ISBN");
        Console.WriteLine("4. Actualizar libros");
        Console.WriteLine("5. Eliminar libro");
        Console.WriteLine("6. Volver al menú principal");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1": 
                RegistrarLibro();
                break;
            case "2": 
                ListarLibros();
                break;
            case "3":
                detallesLibro();
                break;
            case "4": 
                actualizarLibro();
                break;
            case "5": 
                eliminarLibro();
                break;
            case "6":
                Console.Write("volviendo al menú principal...");
                volver = true;
                break;
            default:
                Console.WriteLine("Opción inválida. Presione una tecla...");
                Console.ReadKey();
                break;
        }
    }
}
static void RegistrarLibro()
{
    Console.Clear();
    Console.WriteLine("=== Registrar Libro ===");
    
    Console.ReadKey();
}
static void ListarLibros()
{
    Console.Clear();
    Console.WriteLine("=== Listar Libros ===");
    Console.WriteLine("1. listar todos");
    Console.WriteLine("2. listar disponibles");
    Console.WriteLine("3. listar prestados");
    Console.Write("Seleccione una opción: ");
    
    switch (Console.ReadLine())
    {
        case "1":
            listarTodos();
            break; 
        case "2": 
            listarDisponibles();
            break;
        case "3":
            listarPrestados();
            break;
        default:
            Console.WriteLine("Opción inválida. Presione una tecla...");
            break;
    }
    Console.ReadKey();
}
static void listarTodos()
{
    Console.Clear();
    Console.WriteLine("=== Listar Todos los Libros ===");

    ModelsTesting.MostrarResumenes(); 

    Console.ReadKey();
}
static void listarDisponibles()
{
    Console.Clear();
    Console.WriteLine("=== Listar Libros Disponibles ===");
    
    Console.ReadKey();
}
static void listarPrestados()
{
    Console.Clear();
    Console.WriteLine("=== Listar Libros Prestados ===");
    
    Console.ReadKey();
}
static void detallesLibro()
{
    Console.Clear();
    Console.WriteLine("=== Detalles del Libro por ID/ISBN ===");

    ModelsTesting.MostrarDetalles(); 

    Console.ReadKey();
}
static void actualizarLibro()
{
    Console.Clear();
    Console.WriteLine("=== Actualizar Libro ===");
    Console.Write("editar titulo");
    Console.Write("editar autor");
    Console.Write("editar año");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("nuevo titulo: ");
            break; 
        case "2": 
            Console.Write("nuevo autor: ");
            break;
        case "3":
            Console.Write("nuevo año: ");
            break;
        default:
            Console.WriteLine("Opción inválida. Presione una tecla...");
            break;
    }
    
    Console.ReadKey();
}
static void eliminarLibro()
{
    Console.Clear();
    Console.WriteLine("=== Eliminar Libro ===");

    
    Console.ReadKey();
}
//////////////////////////////////////
/// 
/// 
/// 

static void usuariosMenu()
{
    bool volver = false;
    while (!volver)        {
    Console.Clear();
    Console.WriteLine("=== Menú de Usuarios ===");
    Console.WriteLine("1. Registrar usuario");
    Console.WriteLine("2. Listar usuarios");
    Console.WriteLine("3. detalles por ID/Documento");
    Console.WriteLine("4. Actualizar usuario");
    Console.WriteLine("5. Eliminar usuario");
    Console.WriteLine("6. Volver al menú principal");
    Console.Write("Seleccione una opción: ");

    switch (Console.ReadLine())
    {
        case "1": 
            RegistrarUsuario();
            break;
        case "2": 
            ListarUsuarios();
            break;
        case "3":
            detallesUsuario();
            break;
        case "4": 
            actualizarUsuario();
            break;
        case "5": 
            eliminarUsuario();
            break;
        case "6":
            Console.Write("volviendo al menú principal...");
            volver = true;
            break;
        default:
            Console.WriteLine("Opción inválida. Presione una tecla...");
            Console.ReadKey();
            break;
    }
    }
}
static void RegistrarUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Registrar Usuario ===");
    
    Console.ReadKey();
}
static void ListarUsuarios()
{
    Console.Clear();
    Console.WriteLine("=== Listar Usuarios ===");

    ModelsTesting.MostrarResumenes(); 

    Console.ReadKey();
}
static void detallesUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Detalles del Usuario por ID/Documento ===");

    ModelsTesting.MostrarDetalles(); 

    Console.ReadKey();
}
static void actualizarUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Actualizar Usuario ===");
    Console.Write("1. editar nombre");
    Console.Write("");
    Console.Write("2. editar contacto");
    Console.Write("");
    Console.Write("3. Activar/desactivar usuario");

    switch (Console.ReadLine())
    {
        case "1":
            EditarNombre();
            Console.Write("");
            break; 
        case "2": 
            EditarContacto();
            Console.Write("");
            break;
        case "3":
            ActivarDesactivarUsuario();
            Console.Write("");
            break;
        default:
            Console.WriteLine("Opción inválida. Presione una tecla...");
            break;
    }
    
    Console.ReadKey();
}
static void eliminarUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Eliminar Usuario ===");

    
    Console.ReadKey();
}
static void EditarNombre()
{
    Console.Write("nuevo nombre: ");
}
static void EditarContacto()
{
    Console.Write("nuevo contacto: ");
}
static void ActivarDesactivarUsuario()
{
    Console.Write("¿Activar o desactivar usuario? (1/0): ");
}
static void EliminarUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Eliminar Usuario ===");

    
    Console.ReadKey();
}
////////////////////////////////////////
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
        Console.WriteLine("4. Registrar devolución");
        Console.WriteLine("5. Eliminar préstamo");
        Console.WriteLine("6. Volver al menú principal");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                RegistrarPrestamo();
                break;
            case "2":
                ListarPrestamos();
                break;
            case "3":
                detallesPrestamo();
                break;
            case "4":
                actualizarPrestamo();
                break;
            case "5":
                eliminarPrestamo();
                break;
            case "6":
                Console.WriteLine("Volviendo al menú principal...");
                volver = true;
                break;
            default:
                Console.WriteLine("Opción inválida. Presione una tecla...");
                Console.ReadKey();
                break;
        }
    }
}

// Métodos auxiliares fuera del menú
static void RegistrarPrestamo()
{
    Console.Clear();
    Console.WriteLine("=== Registrar Préstamo ===");
    ModelsTesting.MostrarValidaciones();
    Console.ReadKey();
}

static void ListarPrestamos()
{
    Console.Clear();
    Console.WriteLine("=== Listar Préstamos ===");
    Console.WriteLine("1. Todos");
    Console.WriteLine("2. Activos");
    Console.WriteLine("3. Cerrados");
    Console.Write("Seleccione una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            listarTodosPrestamos();
            break;
        case "2":
            listarPrestamosActivos();
            break;
        case "3":
            listarPrestamosCerrados();
            break;
        default:
            Console.WriteLine("Opción inválida. Presione una tecla...");
            break;
    }

    Console.ReadKey();
}

static void listarTodosPrestamos()
{
    Console.Clear();
    Console.WriteLine("=== Listar Todos los Préstamos ===");

    ModelsTesting.MostrarResumenes(); 

    Console.ReadKey();
}

static void listarPrestamosActivos()
{
    Console.Clear();
    Console.WriteLine("=== Listar Préstamos Activos ===");
    Console.ReadKey();
}

static void listarPrestamosCerrados()
{
    Console.Clear();
    Console.WriteLine("=== Listar Préstamos Cerrados ===");
    Console.ReadKey();
}

static void detallesPrestamo()
{
    Console.Clear();
    Console.WriteLine("=== Detalles del Préstamo por ID ===");

    ModelsTesting.MostrarDetalles(); 

    Console.ReadKey();
}

static void actualizarPrestamo()
{
    Console.Clear();
    Console.WriteLine("=== Registrar Devolución ===");
    Console.ReadKey();
}

static void eliminarPrestamo()
{
    Console.Clear();
    Console.WriteLine("=== Eliminar Préstamo ===");
    Console.ReadKey();
}


//////////////////////////


static void BusquedasReportesMenu()
{
    bool volver = false;
    while (!volver)
    {
        Console.Clear();
        Console.WriteLine("=== Menú de Búsquedas y Reportes ===");
        Console.WriteLine("1. Buscar libro (título/autor/id/categoría)");
        Console.WriteLine("2. Buscar usuario (nombre/id)");
        Console.WriteLine("3. Reportes");
        Console.WriteLine("4. Volver al menú principal");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1":
                BuscarLibro();
                break;
            case "2":
                BuscarUsuario();
                break;
            case "3":
                ReportesMenu();
                break;
            case "4":
                Console.WriteLine("Volviendo al menú principal...");
                volver = true;
                break;
            default:
                Console.WriteLine("Opción inválida. Presione una tecla...");
                Console.ReadKey();
                break;
        }
    }
}

static void BuscarLibro()
{
    Console.Clear();
    Console.WriteLine("=== Buscar Libro ===");
    Console.WriteLine("1. Por título");
    Console.WriteLine("2. Por autor");
    Console.WriteLine("3. Por ID");
    Console.WriteLine("4. Por categoría");
    Console.Write("Seleccione una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Buscando por título...");
            break;
        case "2":
            Console.WriteLine("Buscando por autor...");
            break;
        case "3":
            Console.WriteLine("Buscando por ID...");
            break;
        case "4":
            Console.WriteLine("Buscando por categoría...");
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }

    Console.ReadKey();
}

static void BuscarUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Buscar Usuario ===");
    Console.WriteLine("1. Por nombre");
    Console.WriteLine("2. Por ID");
    Console.Write("Seleccione una opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Buscando usuario por nombre...");
            break;
        case "2":
            Console.WriteLine("Buscando usuario por ID...");
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }

    Console.ReadKey();
}


//////////////////////////////////////////////////////////////////////////////////
static void BusquedasReportes()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Búsquedas y Reportes ===");
            Console.WriteLine("1. Buscar libro");
            Console.WriteLine("2. Buscar usuario");
            Console.WriteLine("3. Reportes");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    BuscarLibroMenu();
                    break;
                case "2":
                    BuscarUsuarioMenu();
                    break;
                case "3":
                    ReportesMenu();
                    break;
                case "4":
                    Console.WriteLine("Volviendo al menú principal...");
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void BuscarLibroMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro ===");
        Console.WriteLine("1. Por título");
        Console.WriteLine("2. Por autor");
        Console.WriteLine("3. Por ID/ISBN");
        Console.WriteLine("4. Por categoría");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1": BuscarLibroPorTitulo(); break;
            case "2": BuscarLibroPorAutor(); break;
            case "3": BuscarLibroPorID(); break;
            case "4": BuscarLibroPorCategoria(); break;
            default: Console.WriteLine("Opción inválida."); break;
        }
        Console.ReadKey();
    }

    static void BuscarLibroPorTitulo()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro por Título ===");
        Console.ReadKey();
    }

    static void BuscarLibroPorAutor()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro por Autor ===");
        Console.ReadKey();
    }

    static void BuscarLibroPorID()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro por ID/ISBN ===");
        Console.ReadKey();
    }

    static void BuscarLibroPorCategoria()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Libro por Categoría ===");
        Console.ReadKey();
    }

    static void BuscarUsuarioMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Usuario ===");
        Console.WriteLine("1. Por nombre");
        Console.WriteLine("2. Por ID/Documento");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1": BuscarUsuarioPorNombre();
             break;
            case "2": BuscarUsuarioPorID();
             break;
            default: Console.WriteLine("Opción inválida."); 
            break;
        }
        Console.ReadKey();
    }

    static void BuscarUsuarioPorNombre()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Usuario por Nombre ===");
        Console.ReadKey();
    }

    static void BuscarUsuarioPorID()
    {
        Console.Clear();
        Console.WriteLine("=== Buscar Usuario por ID/Documento ===");
        Console.ReadKey();
    }

    static void ReportesMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Reportes ===");
        Console.WriteLine("1. Préstamos por usuario");
        Console.WriteLine("2. Préstamos por libro");
        Console.WriteLine("3. Préstamos vencidos");
        Console.WriteLine("4. Resumen general");
        Console.Write("Seleccione una opción: ");

        switch (Console.ReadLine())
        {
            case "1": ReportePorUsuario(); 
            break;
            case "2": ReportePorLibro(); 
            break;
            case "3": ReportePrestamosVencidos(); 
            break;
            case "4": ReporteResumenGeneral(); 
            break;
            default: Console.WriteLine("Opción inválida."); break;
        }
        Console.ReadKey();
    }

    static void ReportePorUsuario()
    {
        Console.Clear();
        Console.WriteLine("=== Reporte de Préstamos por Usuario ===");
        Console.ReadKey();
    }

    static void ReportePorLibro()
    {
        Console.Clear();
        Console.WriteLine("=== Reporte de Préstamos por Libro ===");
        Console.ReadKey();
    }

    static void ReportePrestamosVencidos()
    {
        Console.Clear();
        Console.WriteLine("=== Reporte de Préstamos Vencidos ===");
        Console.ReadKey();
    }

    static void ReporteResumenGeneral()
    {
        Console.Clear();
        Console.WriteLine("=== Resumen General ===");
        Console.WriteLine("Total libros: ...");
        Console.WriteLine("Disponibles: ...");
        Console.WriteLine("Prestados: ...");
        Console.ReadKey();
    }

    ////////////////////////////////////////////


    static void GuardarCargarMenu()
    {
        bool volver = false;
        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== Menú Guardar / Cargar Datos ===");
            Console.WriteLine("1. Guardar datos");
            Console.WriteLine("2. Cargar datos");
            Console.WriteLine("3. Reiniciar datos");
            Console.WriteLine("4. Volver al menú principal");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    GuardarDatos();
                    break;
                case "2":
                    CargarDatos();
                    break;
                case "3":
                    ReiniciarDatos();
                    break;
                case "4":
                    Console.WriteLine("Volviendo al menú principal...");
                    volver = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void GuardarDatos()
    {
        Console.Clear();
        Console.WriteLine("=== Guardar Datos ===");
        Console.WriteLine(" guardando datos en archivo/base de datos...");
        Console.ReadKey();
    }

    static void CargarDatos()
    {
        Console.Clear();
        Console.WriteLine("=== Cargar Datos ===");
        Console.WriteLine("cargando datos desde archivo/base de datos...");
        Console.ReadKey();
    }

    static void ReiniciarDatos()
    {
        Console.Clear();
        Console.WriteLine("=== Reiniciar Datos ===");
        Console.Write("¿Está seguro que desea reiniciar los datos? (S/N): ");
        string opcion = Console.ReadLine()?.ToUpper();

        if (opcion == "S")
        {
            ReiniciarConfirmado();
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }

        Console.ReadKey();
    }

    static void ReiniciarConfirmado()
    {
        Console.Clear();
        Console.WriteLine("Reiniciando datos... ");
        Console.ReadKey();
    }

    ////////////////////////////////////////////
    

    static void PreguntarGuardarAntesDeSalir()
    {
        Console.Clear();
        Console.WriteLine("=== Salida del Programa ===");
        Console.Write("¿Desea guardar los datos antes de salir? (S/N): ");
        string opcion = Console.ReadLine()?.ToUpper();

        if (opcion == "S")
        {
            GuardarDatos();
            Console.WriteLine("Datos guardados correctamente.");
        }
        else
        {
            Console.WriteLine("No se guardaron los datos.");
        }

        Console.WriteLine("Saliendo del programa...");
        Console.ReadKey();
    }


    // ===== Helpers de entrada/validación =====
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
            if (int.TryParse(input, out int value))
                return value;

            Console.WriteLine("Entrada inválida. Debe ser un número.");
        }
    }

    static void Pause(string msg = "Presione una tecla para continuar...")
    {
        Console.WriteLine(msg);
        Console.ReadKey();
    }

    // ===== Seed de datos (sin BD) =====
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

