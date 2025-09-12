using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiniProjectCSharp.Services
{
    public class GenericTaskService<TTask> where TTask : class
    {
        private readonly string _jsonPath;
        public GenericTaskService(string jsonPath)
        {
            _jsonPath = jsonPath;
        }

        public List<TTask> GetAll()
        {
            if (!File.Exists(_jsonPath)) return new List<TTask>();
            var json = File.ReadAllText(_jsonPath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return JsonSerializer.Deserialize<List<TTask>>(json, options) ?? new List<TTask>();
        }

        public List<TTask> Filter(Predicate<TTask> predicate)
        {
            var tasks = GetAll();
            return tasks.FindAll(predicate);
        }
    }
}
