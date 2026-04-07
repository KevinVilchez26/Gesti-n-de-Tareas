using System;
using System.Collections.Generic;

namespace GestionTareas.Models
{
    /// <summary>
    /// DTO (Data Transfer Object) usado para serialización/deserialización JSON.
    /// Esto facilita guardar tareas decoradas sin problemas de polimorfismo.
    /// </summary>
    public class TaskDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; } = "Simple";
        public string Priority { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new List<string>();
    }
}
