using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using MiniProjectCSharp.Models;

namespace MiniProjectCSharp.Services
{
    public class TaskService
    {
        private readonly string jsonPath;
        public TaskService(string jsonPath)
        {
            this.jsonPath = jsonPath;
        }

        public List<TaskItem> GetAll()
        {
            if (!File.Exists(jsonPath)) return new List<TaskItem>();
            var json = File.ReadAllText(jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<TaskItem>>(json, options) ?? new List<TaskItem>();
        }

        public List<TaskItem> Filter(Predicate<TaskItem> predicate)
        {
            var tasks = GetAll();
            return tasks.FindAll(predicate);
        }
    }
}
