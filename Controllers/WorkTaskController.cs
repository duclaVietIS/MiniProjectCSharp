using Microsoft.AspNetCore.Mvc;
using MiniProjectCSharp.Models;
using MiniProjectCSharp.Services;
using MiniProjectCSharp.Enums;
using System;
using System.Linq;

namespace MiniProjectCSharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkTaskController : ControllerBase
    {
        private readonly GenericTaskService<WorkTaskItem> _service;
        public WorkTaskController()
        {
            var jsonPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "study_tasks.json");
            _service = new GenericTaskService<WorkTaskItem>(jsonPath);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var tasks = _service.GetAll();
            return Ok(tasks);
        }

        [HttpGet("priority/{level}")]
        public IActionResult FilterByPriority(string level)
        {
            if (Enum.TryParse<PriorityLevel>(level, true, out var priority))
            {
                var result = _service.Filter(t => t.Priority == priority);
                return Ok(result);
            }
            return BadRequest("Invalid priority level");
        }
    }
}
