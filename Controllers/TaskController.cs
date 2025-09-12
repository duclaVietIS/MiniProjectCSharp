using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MiniProjectCSharp.Models;
using System.Text.Json;
using MiniProjectCSharp.Services;

using MiniProjectCSharp.Enums;

namespace MiniProjectCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _service;
        public TaskController()
        {
            var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "tasks.json");
            _service = new TaskService(jsonPath);
        }

        private MiniProjectCSharp.Enums.TaskStatus GetStatus(TaskItem t)
        {
            var now = DateTime.Now;
            return t switch
            {
                { CompletedAt: not null } => MiniProjectCSharp.Enums.TaskStatus.Done,
                { Deadline: var d } when d < now => MiniProjectCSharp.Enums.TaskStatus.Overdue,
                { Deadline: var d } when d >= now && d <= now.AddDays(2) => MiniProjectCSharp.Enums.TaskStatus.DueSoon,
                _ => MiniProjectCSharp.Enums.TaskStatus.Pending
            };
        }

        // Lấy tất cả task kèm trạng thái
        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _service.GetAll();
            var result = tasks.Select(t => new { t.Id, t.Title, t.Deadline, t.CompletedAt, t.Priority, Status = GetStatus(t) });
            return Ok(result);
        }

        // Lọc theo trạng thái
        [HttpGet("status/{status}")]
        public IActionResult FilterByStatus(string status)
        {
            if (!Enum.TryParse<MiniProjectCSharp.Enums.TaskStatus>(status, true, out var s)) return BadRequest("Invalid status");
            var result = _service.Filter(t => GetStatus(t) == s)
                .Select(t => new { t.Id, t.Title, t.Deadline, t.CompletedAt, t.Priority, Status = GetStatus(t) });
            return Ok(result);
        }

        // Sắp xếp theo deadline
        [HttpGet("sort")]
        public IActionResult SortByDeadline()
        {
            var result = _service.GetAll()
                .OrderBy(t => t.Deadline)
                .Select(t => new { t.Id, t.Title, t.Deadline, t.CompletedAt, t.Priority, Status = GetStatus(t) });
            return Ok(result);
        }

        // Phân loại theo priority
        [HttpGet("priority/{level}")]
        public IActionResult FilterByPriority(string level)
        {
            if (Enum.TryParse<MiniProjectCSharp.Enums.PriorityLevel>(level, true, out var priority))
            {
                var result = _service.Filter(t => (MiniProjectCSharp.Enums.PriorityLevel)t.Priority == priority)
                .Select(t => new { t.Id, t.Title, t.Deadline, t.CompletedAt, t.Priority, Status = GetStatus(t) });
                return Ok(result);
            }
            return BadRequest("Invalid priority level");
        }

        // Lọc task DueSoon, Overdue
        [HttpGet("urgent")]
        public IActionResult GetUrgent()
        {
            var result = _service.Filter(t => GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.DueSoon || GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.Overdue)
                .OrderBy(t => t.Deadline)
                .Select(t => new { t.Id, t.Title, t.Deadline, t.CompletedAt, t.Priority, Status = GetStatus(t) });
            return Ok(result);
        }

        // Thống kê: Đếm từng trạng thái
        [HttpGet("stats")]
        public IActionResult Stats()
        {
            var tasks = _service.GetAll();
            var total = tasks.Count;
            var done = tasks.Count(t => GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.Done);
            var overdue = tasks.Count(t => GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.Overdue);
            var dueSoon = tasks.Count(t => GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.DueSoon);
            var pending = tasks.Count(t => GetStatus(t) == MiniProjectCSharp.Enums.TaskStatus.Pending);
            return Ok(new { total, done, overdue, dueSoon, pending });
        }
    }
}
