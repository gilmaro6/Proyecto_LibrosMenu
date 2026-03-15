namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
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
                case "5": 
                case "6":
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
    
    Console.ReadKey();
}
static void detallesUsuario()
{
    Console.Clear();
    Console.WriteLine("=== Detalles del Usuario por ID/Documento ===");
    
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
}
