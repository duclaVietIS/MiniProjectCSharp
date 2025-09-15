using System;
using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{
    /// <summary>
    /// Giao diện cho các loại công việc
    /// </summary>
    public interface ITaskItem
    {
        int Id { get; }
        string Title { get; }
        DateTime Deadline { get; }
        DateTime? CompletedAt { get; }
        PriorityLevel Priority { get; }
    }

    /// <summary>
    /// Lớp cơ sở cho các loại công việc khác nhau
    /// </summary>
    public abstract class BaseTaskItem : ITaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public PriorityLevel Priority { get; set; }
    }
}
