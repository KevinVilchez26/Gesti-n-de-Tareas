using System;

namespace GestionTareas.Models
{
    /// <summary>
    /// Componente Concreto (ConcreteComponent) en el patrón Decorator.
    /// Representa la implementación básica de una tarea sin características extra.
    /// </summary>
    public class SimpleTask : ITask
    {
        public string Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; private set; }
        public DateTime CreatedAt { get; private set; }
        
        // Propiedades por defecto que pueden ser alteradas por los decoradores o no utilizadas
        public virtual string Priority => "";
        public virtual string Tags => "";
        public virtual string Type => "Simple";

        public SimpleTask(string title, string description)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedAt = DateTime.Now;
        }

        // Permite reconstruir una tarea existente (útil para la carga de JSON)
        public SimpleTask(string id, string title, string description, bool isCompleted, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;
        }

        public void Complete()
        {
            IsCompleted = true;
        }

        public virtual string GetDetails()
        {
            string status = IsCompleted ? "[X]" : "[ ]";
            return $"{status} {Title} | {Description}";
        }
    }
}
