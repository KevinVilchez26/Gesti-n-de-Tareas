using System.Collections.Generic;
using GestionTareas.Models;

namespace GestionTareas.Patterns.Strategy
{
    /// <summary>
    /// (4) PATRÓN STRATEGY - Interfaz Estrategia.
    /// Define el contrato para distintos algoritmos de ordenamiento de tareas.
    /// Esto permite intercambiar la lógica de ordenado en tiempo de ejecución.
    /// </summary>
    public interface ISortStrategy
    {
        IEnumerable<ITask> Sort(IEnumerable<ITask> tasks);
    }
}
