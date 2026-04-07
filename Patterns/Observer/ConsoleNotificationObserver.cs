using System;

namespace GestionTareas.Patterns.Observer
{
    /// <summary>
    /// Observador Concreto del Patrón Observer.
    /// Se suscribe al gestor y muestra una alerta en consola cuando una tarea se actualiza.
    /// </summary>
    public class ConsoleNotificationObserver : ITaskObserver
    {
        public void Update(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n[NOTIFICACIÓN] {message}");
            Console.ResetColor();
        }
    }
}
