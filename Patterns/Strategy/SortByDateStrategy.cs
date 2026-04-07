using System.Collections.Generic;
using System.Linq;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Strategy
{
    /// <summary>
    /// Estrategia Concreta.
    /// Ordena las tareas por Fecha de Creación (Más antiguas primero).
    /// </summary>
    public class SortByDateStrategy : ISortStrategy
    {
        public IEnumerable<ITask> Sort(IEnumerable<ITask> tasks)
        {
            return tasks.OrderBy(t => t.CreatedAt);
        }
    }
}
