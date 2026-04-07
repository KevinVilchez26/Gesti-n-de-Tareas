using System;
using System.Linq;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Decorator
{
    /// <summary>
    /// Decorador Concreto (ConcreteDecorator).
    /// Añade la funcionalidad de 'Etiquetas' espaciadas a una tarea.
    /// </summary>
    public class TagDecorator : TaskDecorator
    {
        private readonly string _tags;

        public TagDecorator(ITask task, string tags) : base(task)
        {
            _tags = tags;
        }

        public override string Tags => _tags;

        public override string GetDetails()
        {
            return $"{base.GetDetails()} #{_tags.Replace(" ", " #")}";
        }
    }
}
