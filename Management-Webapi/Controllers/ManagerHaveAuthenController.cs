using Management_Webapi.Model.Dto;
using Management_Webapi.Model.Request;
using Management_Webapi.Service;
using Management_Webapi.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Management_Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerConManagerHaveAuthenControllertroller : ControllerBase
    {
        private readonly IAuthenService _authService;
        private readonly List<TaskDto> _tasks = new List<TaskDto>();

        public ManagerConManagerHaveAuthenControllertroller(IAuthenService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDto model)
        {
            var user = _authService.RegisterUser(model.Username, model.Password);
            return Ok(new { user.Id, user.Username });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserDto model)
        {
            var user = _authService.AuthenticateUser(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid login attempt" });
            }

            // Fake token generation for demonstration purposes
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return Ok(new { token });
        }

        //[HttpPost("CreateTask")]
        //public IActionResult CreateTask([FromBody] TaskReqs reqs)
        //{
        //    // Fake user id for demonstration purposes
        //    var userId = "fakeUserId";
        //    var task = new TaskDto
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        Title = reqs.Title,
        //        Description = reqs.Description,
        //        Priority = reqs.Priority,
        //        DueDate = reqs.DueDate,
        //        UserId = userId
        //    };
        //    _tasks.Add(task);
        //    return Ok(new BaseResponse { data = new TaskResp { Id = task.Id, Title = task.Title, Description = task.Description, Priority = task.Priority, DueDate = task.DueDate } });
        //}

        //[HttpGet("GetTasks")]
        //public IActionResult GetTasks()
        //{
        //    // Fake user id for demonstration purposes
        //    var userId = "fakeUserId";
        //    var tasks = _tasks.Where(t => t.UserId == userId);
        //    return Ok(new BaseResponse { data = tasks });
        //}

        //[HttpGet("GetTaskById/{id}")]
        //public IActionResult GetTaskById(string id)
        //{
        //    // Fake user id for demonstration purposes
        //    var userId = "fakeUserId";
        //    var task = _tasks.FirstOrDefault(t => t.UserId == userId && t.Id == id);
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(new BaseResponse { data = task });
        //}

        //[HttpPut("UpdateTask")]
        //public IActionResult UpdateTask([FromBody] TaskReqs reqs)
        //{
        //    // Fake user id for demonstration purposes
        //    var userId = "fakeUserId";
        //    var task = _tasks.FirstOrDefault(t => t.UserId == userId && t.Id == reqs.Id);
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }

        //    task.Title = reqs.Title;
        //    task.Description = reqs.Description;
        //    task.Priority = reqs.Priority;
        //    task.DueDate = reqs.DueDate;

        //    return Ok(new BaseResponse { data = new TaskResp { Id = task.Id, Title = task.Title, Description = task.Description, Priority = task.Priority, DueDate = task.DueDate } });
        //}

        //[HttpDelete("DeleteTask/{id}")]
        //public IActionResult DeleteTask(string id)
        //{
        //    // Fake user id for demonstration purposes
        //    var userId = "fakeUserId";
        //    var task = _tasks.FirstOrDefault(t => t.UserId == userId && t.Id == id);
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }
        //    _tasks.Remove(task);
        //    return Ok(new BaseResponse { data = new TaskDeleteResp { IsSuccess = true } });
        //}
    }
}

