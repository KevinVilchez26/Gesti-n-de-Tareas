using System;

namespace GestionTareas.Models
{
    /// <summary>
    /// Componente principal del patrón Decorator.
    /// Define el contrato base para tareas y sus decoradores.
    /// </summary>
    public interface ITask
    {
        string Id { get; }
        string Title { get; set; }
        string Description { get; set; }
        bool IsCompleted { get; }
        DateTime CreatedAt { get; }
        
        string Priority { get; } // Agregado para facilitar extracción al DTO
        string Tags { get; } // Agregado para facilitar extracción al DTO
        string Type { get; } 

        void Complete();
        string GetDetails();
    }
}
