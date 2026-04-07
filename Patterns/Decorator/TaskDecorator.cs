using System;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Decorator
{
    /// <summary>
    /// (5) PATRÓN DECORATOR - Clase Base Decorador (Decorator).
    /// Envuelve un ITask y delega su comportamiento al objeto encapsulado.
    /// Sirve de base para extender las funcionalidades dinámicamente.
    /// </summary>
    public abstract class TaskDecorator : ITask
    {
        protected internal ITask _task;

        public TaskDecorator(ITask task)
        {
            _task = task;
        }

        public string Id => _task.Id;
        public string Title 
        { 
            get => _task.Title; 
            set => _task.Title = value; 
        }
        public string Description 
        { 
            get => _task.Description; 
            set => _task.Description = value; 
        }
        public bool IsCompleted => _task.IsCompleted;
        public DateTime CreatedAt => _task.CreatedAt;
        public string Type => _task.Type;

        // Por defecto delega a la tarea base, que puede o no estar decorada
        public virtual string Priority => _task.Priority;
        public virtual string Tags => _task.Tags;

        public virtual void Complete()
        {
            _task.Complete();
        }

        public virtual string GetDetails()
        {
            return _task.GetDetails();
        }
    }
}
