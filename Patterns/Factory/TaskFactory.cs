using System;
using GestionTareas.Models;
using GestionTareas.Patterns.Decorator;

namespace GestionTareas.Patterns.Factory
{
    /// <summary>
    /// (2) PATRÓN FACTORY METHOD - Creador de Tareas.
    /// Centraliza y encapsula la lógica de instanciación de las jerarquías de ITask.
    /// </summary>
    public static class TaskFactory
    {
        /// <summary>
        /// Crea una nueva tarea aplicando decoradores opcionales según los parámetros.
        /// </summary>
        public static ITask CreateTask(string title, string description, string type = "Simple", string priority = "", string tags = "")
        {
            ITask baseTask = new SimpleTask(title, description);

            if (!string.IsNullOrEmpty(priority))
            {
                baseTask = new PriorityDecorator(baseTask, priority);
            }

            if (!string.IsNullOrEmpty(tags))
            {
                baseTask = new TagDecorator(baseTask, tags);
            }

            return baseTask;
        }

        /// <summary>
        /// Reconstruye una tarea a partir de un DTO cargado (útil para la persistencia).
        /// </summary>
        public static ITask ReconstructTask(TaskDto dto)
        {
            ITask baseTask = new SimpleTask(dto.Id, dto.Title, dto.Description, dto.IsCompleted, dto.CreatedAt);

            if (!string.IsNullOrEmpty(dto.Priority))
            {
                baseTask = new PriorityDecorator(baseTask, dto.Priority);
            }

            if (dto.Tags != null && dto.Tags.Count > 0)
            {
                baseTask = new TagDecorator(baseTask, string.Join(" ", dto.Tags));
            }

            return baseTask;
        }
    }
}
