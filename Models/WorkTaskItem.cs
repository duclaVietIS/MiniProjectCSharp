using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{
    /// <summary>
    /// Lớp đại diện cho một công việc liên quan đến công việc
    /// </summary>
    public class WorkTaskItem : BaseTaskItem
    {
        public string Project { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
    }
}
