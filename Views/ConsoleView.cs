using System;
using GestionTareas.Models;
using GestionTareas.Patterns.Factory;
using GestionTareas.Patterns.Observer;
using GestionTareas.Patterns.Singleton;
using GestionTareas.Patterns.Strategy;

namespace GestionTareas.Views
{
    /// <summary>
    /// Interfaz de Usuario mediante consola.
    /// Consume el Singleton TaskManager e interactúa con el usuario.
    /// </summary>
    public class ConsoleView
    {
        private readonly TaskManager _taskManager;

        public ConsoleView()
        {
            _taskManager = TaskManager.Instance;
            // Registrar el observador para notificaciones
            _taskManager.Attach(new ConsoleNotificationObserver());
        }

        public void RenderMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n=== GESTIÓN DE TAREAS ===");
                Console.WriteLine("1. Ver Tareas");
                Console.WriteLine("2. Agregar Nueva Tarea");
                Console.WriteLine("3. Completar Tarea");
                Console.WriteLine("4. Eliminar Tarea");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowTasksMenu();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        CompleteTask();
                        break;
                    case "4":
                        RemoveTask();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        private void ShowTasksMenu()
        {
            Console.WriteLine("\n--- VER TAREAS ---");
            Console.WriteLine("1. Ordenar por Fecha (Por Defecto)");
            Console.WriteLine("2. Ordenar por Estado (Pendientes primero)");
            Console.WriteLine("3. Ordenar por Prioridad (Alta a Baja)");
            Console.Write("Elija el método de ordenación: ");

            var option = Console.ReadLine();
            ISortStrategy strategy = option switch
            {
                "2" => new SortByStatusStrategy(),
                "3" => new SortByPriorityStrategy(),
                _ => new SortByDateStrategy()
            };

            var tasks = _taskManager.GetTasks(strategy);

            Console.WriteLine("\n--- LISTA DE TAREAS ---");
            int count = 0;
            foreach (var task in tasks)
            {
                count++;
                Console.WriteLine($"ID: {task.Id} | {task.GetDetails()}");
            }
            if (count == 0) Console.WriteLine("No hay tareas registradas.");
        }

        private void AddTask()
        {
            Console.WriteLine("\n--- AGREGAR TAREA ---");
            Console.Write("Título: ");
            string title = Console.ReadLine() ?? string.Empty;
            
            Console.Write("Descripción: ");
            string description = Console.ReadLine() ?? string.Empty;

            Console.Write("Prioridad (Alta/Media/Baja o preionar Enter para omitir): ");
            string priority = Console.ReadLine() ?? string.Empty;

            Console.Write("Etiquetas (Separadas por espacio o presionar Enter para omitir): ");
            string tags = Console.ReadLine() ?? string.Empty;

            // Uso del Factory Method para crear la tarea
            var newTask = GestionTareas.Patterns.Factory.TaskFactory.CreateTask(title, description, "Simple", priority, tags);
            
            _taskManager.AddTask(newTask);
        }

        private void CompleteTask()
        {
            Console.WriteLine("\n--- COMPLETAR TAREA ---");
            Console.Write("Ingrese el ID de la tarea a completar: ");
            string id = Console.ReadLine() ?? string.Empty;

            _taskManager.CompleteTask(id);
        }

        private void RemoveTask()
        {
            Console.WriteLine("\n--- ELIMINAR TAREA ---");
            Console.Write("Ingrese el ID de la tarea a eliminar: ");
            string id = Console.ReadLine() ?? string.Empty;

            _taskManager.RemoveTask(id);
        }
    }
}
