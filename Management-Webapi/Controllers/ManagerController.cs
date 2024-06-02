using Management_Webapi.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Management_Webapi.Controllers
{
    public class ManagerController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class TasksController : ControllerBase
        {
            List<TaskDto> tasks = new List<TaskDto>();

            [HttpGet]
            public ActionResult<IEnumerable<TaskDto>> GetTasks()
            {
                return tasks;
            }

            [HttpGet("{id}")]
            public ActionResult<TaskDto> GetTask(int id)
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return NotFound();
                }
                return task;
            }

            [HttpPost]
            public ActionResult<TaskDto> CreateTask(TaskDto task)
            {
                if (tasks.Any(t => t.Id == task.Id))
                {
                    return BadRequest("Task with this ID already exists");
                }
                tasks.Add(task);
                return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
            }

            [HttpPut("{id}")]
            public ActionResult<TaskDto> UpdateTask(int id, TaskDto task)
            {
                var existingTask = tasks.FirstOrDefault(t => t.Id == id);
                if (existingTask == null)
                {
                    return NotFound();
                }
                if (task.DueDate < DateTime.Now)
                {
                    return BadRequest("Due date must be in the future");
                }
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Priority = task.Priority;
                existingTask.DueDate = task.DueDate;
                return existingTask;
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteTask(int id)
            {
                var task = tasks.FirstOrDefault(t => t.Id == id);
                if (task == null)
                {
                    return NotFound();
                }
                tasks.Remove(task);
                return NoContent();
            }
        }
    }
}
