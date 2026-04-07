using System;
using System.Collections.Generic;
using System.Linq;
using GestionTareas.Models;
using GestionTareas.Patterns.Factory;
using GestionTareas.Patterns.Observer;
using GestionTareas.Services;
using GestionTareas.Patterns.Strategy;

namespace GestionTareas.Patterns.Singleton
{
    /// <summary>
    /// (1) PATRÓN SINGLETON - Gestor Central de Tareas.
    /// Garantiza una única instancia en toda la aplicación para manejar la colección de tareas.
    /// También actúa como el Sujeto (Subject) en el patrón Observer.
    /// </summary>
    public sealed class TaskManager
    {
        private static TaskManager? _instance = null;
        private static readonly object _padlock = new object();

        private readonly List<ITask> _tasks;
        private readonly List<ITaskObserver> _observers;

        // Constructor privado para evitar instanciación externa
        private TaskManager()
        {
            _tasks = new List<ITask>();
            _observers = new List<ITaskObserver>();
            LoadTasks(); // Cargar estado inicial desde JSON
        }

        /// <summary>
        /// Acceso global a la única instancia (Thread-safe).
        /// </summary>
        public static TaskManager Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new TaskManager();
                    }
                    return _instance;
                }
            }
        }

        // --- MÉTODOS DE OBSERVER ---
        
        public void Attach(ITaskObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(ITaskObserver observer)
        {
            _observers.Remove(observer);
        }

        private void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        // --- MÉTODOS DE NEGOCIO ---

        public void AddTask(ITask task)
        {
            _tasks.Add(task);
            Notify($"Nueva tarea agregada: '{task.Title}'");
            SaveTasks();
        }

        public void CompleteTask(string taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null && !task.IsCompleted)
            {
                task.Complete();
                Notify($"Tarea marcada como completada: '{task.Title}'");
                SaveTasks();
            }
        }

        public void RemoveTask(string taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _tasks.Remove(task);
                Notify($"Tarea eliminada: '{task.Title}'");
                SaveTasks();
            }
        }

        /// <summary>
        /// Usa el patrón Strategy de forma dinámica para retornar la lista ordenada
        /// </summary>
        public IEnumerable<ITask> GetTasks(ISortStrategy sortStrategy)
        {
            return sortStrategy.Sort(_tasks);
        }
        
        public IEnumerable<ITask> GetRawTasks() => _tasks;

        // --- PERSISTENCIA ---

        private void SaveTasks()
        {
            var dtos = _tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                CreatedAt = t.CreatedAt,
                Type = t.Type,
                Priority = t.Priority,
                Tags = !string.IsNullOrEmpty(t.Tags) ? t.Tags.Split(' ').ToList() : new List<string>()
            });
            StorageService.SaveTasks(dtos);
        }

        private void LoadTasks()
        {
            var dtos = StorageService.LoadTasks();
            _tasks.Clear();
            foreach (var dto in dtos)
            {
                _tasks.Add(GestionTareas.Patterns.Factory.TaskFactory.ReconstructTask(dto));
            }
        }
    }
}
