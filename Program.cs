using System;
using GestionTareas.Views;

namespace GestionTareas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Gestor de Tareas...");

            var view = new ConsoleView();
            view.RenderMenu();

            Console.WriteLine("Aplicación finalizada.");
        }
    }
}
