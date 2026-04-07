using System;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Decorator
{
    /// <summary>
    /// Decorador Concreto (ConcreteDecorator).
    /// Añade la funcionalidad o estado extra de 'Prioridad' a una tarea.
    /// </summary>
    public class PriorityDecorator : TaskDecorator
    {
        private readonly string _priorityLevel;

        public PriorityDecorator(ITask task, string priorityLevel) : base(task)
        {
            _priorityLevel = priorityLevel;
        }

        public override string Priority => _priorityLevel;

        public override string GetDetails()
        {
            return $"[{_priorityLevel}] {base.GetDetails()}";
        }
    }
}
