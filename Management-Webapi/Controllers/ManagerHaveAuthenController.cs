using Management_Webapi.Mapping;
using Management_Webapi.Model;
using Management_Webapi.Model.Dto;
using Management_Webapi.Model.Request;
using Management_Webapi.Model.Response;
using Management_Webapi.Service;
using Management_Webapi.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Management_Webapi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ManagerHaveAuthenController : ControllerBase
    {
        private readonly IAuthenService _authService;
        private static List<TaskDto> _tasks = new List<TaskDto>();

        public ManagerHaveAuthenController(IAuthenService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public BaseResponse Register([FromBody] AuthenReqs reqs)
        {
            var model = new BaseResponse();
            var resp = new AuthenResp();
            if (!reqs.IsValidateReq())
            {
                model.code = 1;
                model.SetResponseError(ResponseError.InvalidReqs);
                model.data = resp;
                return model;
            }
            model.SetResponseError(ResponseError.NoError);
            var user = _authService.RegisterUser(reqs.UserName, reqs.Password);
            if(user == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
                model.data = resp;
                return model;
            }
            var fakeToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            resp.IsSuccess = true;
            resp.Token = fakeToken;
            model.data = resp;
            return model;
        }

        [HttpPost("Login")]
        public BaseResponse Login([FromBody] AuthenReqs reqs)
        {
            var model = new BaseResponse();
            var resp = new AuthenResp();
            if (!reqs.IsValidateReq())
            {
                model.code = 1;
                model.SetResponseError(ResponseError.InvalidReqs);
                model.data = resp;
                return model;
            }
            model.SetResponseError(ResponseError.NoError);
            var user = _authService.AuthenticateUser(reqs.UserName, reqs.Password);
            if (user == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.Unauthorized);
                model.data = resp;
                return model;
            }
            var fakeToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            resp.IsSuccess = true;
            resp.Token = fakeToken;
            model.data = resp;
            return model;
        }

        [HttpPost("CreateTask")]
        public BaseResponse CreateTask([FromBody] TaskReqs reqs)
        {
            var model = new BaseResponse();
            var resp = new TaskResp();
            model.SetResponseError(ResponseError.NoError);
            if (!reqs.IsValidateReq())
            {
                model.code = 1;
                model.SetResponseError(ResponseError.InvalidReqs);
                model.data = resp;
                return model;
            }
            var fakeUserId = "guiId123";
            var TaskNew = reqs.ToModelTaskDto(fakeUserId);
            _tasks.Add(TaskNew);
            resp = TaskNew.ToTaskViewModel();
            model.data = resp;
            return model;
        }

        [HttpGet("GetTasks")]
        public BaseResponse GetTasks(int pageIndex, int pageSize, Sort sort, Filter filter, Priority priority)
        {
            var model = new BaseResponse();
            var fakeUserId = "guiId123";
            var tasks = _tasks.Where(t => t.UserId == fakeUserId);
            if(filter == Filter.Priority)
            {
                var temp = tasks.Where(e => e.Priority == priority);
                tasks = temp;
            }
            if (sort == Sort.Priority)
            {
                tasks.OrderByDescending(t => t.DueDate);
            }
            var paginatedTasks = tasks
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            model.data = paginatedTasks;
            return model;
        }

        [HttpGet("GetTaskById")]
        public BaseResponse GetTaskById(string id)
        {
            var model = new BaseResponse();
            var resp = new TaskDto();
            model.SetResponseError(ResponseError.NoError);
            var fakeUserId = "fakeUserId";
            resp = _tasks.FirstOrDefault(t => t.UserId == fakeUserId && t.Id == id);
            if (resp == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
            }
            model.data = resp;
            return model;
        }

        [HttpPut("UpdateTask")]
        public BaseResponse UpdateTask([FromBody] TaskReqs reqs)
        {
            var model = new BaseResponse();
            var resp = new TaskResp();
            model.SetResponseError(ResponseError.NoError);
            if (!reqs.IsValidateReq(true))
            {
                model.code = 1;
                model.SetResponseError(ResponseError.InvalidReqs);
                model.data = resp;
                return model;
            }
            var fakeUserId = "fakeUserId";
            var existingTask = _tasks.FirstOrDefault(t => t.UserId == fakeUserId && t.Id == reqs.Id);
            if (existingTask == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
                model.data = resp;
                return model;
            }
            var TaskUpdated = existingTask.ToModelTaskUpdated(reqs);
            resp = TaskUpdated.ToTaskViewModel();
            model.data = resp;
            return model;
        }

        [HttpDelete("DeleteTask/{id}")]
        public BaseResponse DeleteTask(string id)
        {
            var model = new BaseResponse();
            var resp = new TaskDeleteResp();
            if (string.IsNullOrEmpty(id))
            {
                model.code = 1;
                model.SetResponseError(ResponseError.InvalidReqs);
                model.data = resp;
                return model;
            }
            var fakeUserId = "fakeUserId";
            var TaskDto = _tasks.FirstOrDefault(t => t.UserId == fakeUserId && t.Id == id);
            if (TaskDto == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
                model.data = resp;
                return model;
            }
            _tasks.Remove(TaskDto);
            resp.IsSuccess = true;
            model.data = resp;
            return model;
        }
    }
}

