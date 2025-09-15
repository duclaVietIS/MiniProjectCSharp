using System.ComponentModel;
/// <summary>
/// Trạng thái của công việc
/// </summary>
namespace MiniProjectCSharp.Enums
{
    public enum TaskStatus
    {
        [Description("done")]
        Done,
        [Description("overdue")]
        Overdue,
        [Description("due soon")]
        DueSoon,
        [Description("待处理")]
        Pending
    }
}
