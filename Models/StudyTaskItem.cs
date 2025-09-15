using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Models
{
    public class StudyTaskItem : BaseTaskItem
    {
        public string Subject { get; set; } = string.Empty;
        public string Teacher { get; set; } = string.Empty;
        
    }
}
