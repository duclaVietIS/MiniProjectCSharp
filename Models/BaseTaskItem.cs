using System;
using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{
    public interface ITaskItem
    {
        int Id { get; }
        string Title { get; }
        DateTime Deadline { get; }
        DateTime? CompletedAt { get; }
        PriorityLevel Priority { get; }
    }

    public abstract class BaseTaskItem : ITaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public PriorityLevel Priority { get; set; }
    }
}
