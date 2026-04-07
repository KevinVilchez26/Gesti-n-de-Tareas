using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Strategy
{
    /// <summary>
    /// Estrategia Concreta.
    /// Ordena las tareas por Prioridad (Alta => Media => Baja).
    /// </summary>
    public class SortByPriorityStrategy : ISortStrategy
    {
        public IEnumerable<ITask> Sort(IEnumerable<ITask> tasks)
        {
            return tasks.OrderBy(t => GetPriorityWeight(t.Priority));
        }

        private int GetPriorityWeight(string priority)
        {
            if (string.IsNullOrEmpty(priority)) return 3; // Sin prioridad
            
            var lowerPrio = priority.ToLower();
            if (lowerPrio.Contains("alta")) return 0;
            if (lowerPrio.Contains("media")) return 1;
            if (lowerPrio.Contains("baja")) return 2;
            
            return 4; // Prioridades custom
        }
    }
}
