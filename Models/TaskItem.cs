using System;

namespace MiniProjectCSharp.Models
{
    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public record TaskItem(
        int Id,
        string Title,
        DateTime Deadline,
        DateTime? CompletedAt,
        PriorityLevel Priority
    );
}
