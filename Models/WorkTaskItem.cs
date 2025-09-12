using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{
    public class WorkTaskItem : BaseTaskItem
    {
        public string Project { get; set; } = string.Empty;
        public string Assignee { get; set; } = string.Empty;
    }
}
