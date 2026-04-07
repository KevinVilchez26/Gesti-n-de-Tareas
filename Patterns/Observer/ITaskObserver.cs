using GestionTareas.Models;

namespace GestionTareas.Patterns.Observer
{
    /// <summary>
    /// (3) PATRÓN OBSERVER - Interfaz Observador.
    /// Define el contrato para objetos que quieren ser notificados
    /// ante cambios de estado en el sujeto (TaskManager).
    /// </summary>
    public interface ITaskObserver
    {
        void Update(string message);
    }
}
