using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GestionTareas.Models;

namespace GestionTareas.Services
{
    /// <summary>
    /// Servicio básico para persistir las tareas en formato JSON.
    /// No es un patrón GoF específico, pero es requerido por los atributos del proyecto.
    /// </summary>
    public static class StorageService
    {
        private const string FilePath = "tasks.json";

        public static void SaveTasks(IEnumerable<TaskDto> tasks)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(tasks, options);
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar las tareas: {ex.Message}");
            }
        }

        public static List<TaskDto> LoadTasks()
        {
            if (!File.Exists(FilePath))
                return new List<TaskDto>();

            try
            {
                string json = File.ReadAllText(FilePath);
                var tsks = JsonSerializer.Deserialize<List<TaskDto>>(json);
                return tsks ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar las tareas: {ex.Message}");
                return new List<TaskDto>();
            }
        }
    }
}
