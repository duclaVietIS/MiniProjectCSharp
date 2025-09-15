using System;
using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{

    /// <summary>
    /// Lớp đại diện cho một công việc
    /// </summary>
    public record TaskItem(
        int Id,
        string Title,
        DateTime Deadline,
        DateTime? CompletedAt,
        PriorityLevel Priority
    );
}
