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
                case "3":
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
}
