using System.ComponentModel;
/// <summary>
/// Mức độ ưu tiên của công việc
/// </summary>
namespace MiniProjectCSharp.Enums
{
    public enum PriorityLevel
    {
        [Description("low")]
        Low,
        [Description("medium")]
        Medium,
        [Description("high")]
        High
    }
}
