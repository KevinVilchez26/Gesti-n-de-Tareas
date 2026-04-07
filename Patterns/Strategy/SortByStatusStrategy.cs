using System.Collections.Generic;
using System.Linq;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Strategy
{
    /// <summary>
    /// Estrategia Concreta.
    /// Ordena las tareas por su Estado (Pendientes primero, luego completadas).
    /// </summary>
    public class SortByStatusStrategy : ISortStrategy
    {
        public IEnumerable<ITask> Sort(IEnumerable<ITask> tasks)
        {
            return tasks.OrderBy(t => t.IsCompleted).ThenBy(t => t.Title);
        }
    }
}
