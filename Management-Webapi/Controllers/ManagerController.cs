using Management_Webapi.Mapping;
using Management_Webapi.Model;
using Management_Webapi.Model.Dto;
using Management_Webapi.Model.Request;
using Management_Webapi.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Management_Webapi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private static List<TaskDto> tasks = new List<TaskDto>();

        [HttpGet]
        public BaseResponse GetTasks()
        {
            var model = new BaseResponse();
            model.data = tasks;
            return model;
        }

        [HttpGet]
        public BaseResponse GetTaskById(string id)
        {
            var model = new BaseResponse();
            var resp = new TaskDto();
            model.SetResponseError(ResponseError.NoError);
            resp = tasks.FirstOrDefault(t => t.Id == id);
            if (resp == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
            }
            model.data = resp;
            return model;
        }

        [HttpPost]
        public BaseResponse CreateTask(TaskReqs reqs)
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
            var TaskNew = reqs.ToModelTaskDto();
            tasks.Add(TaskNew);
            resp = TaskNew.ToTaskViewModel();
            model.data = resp;
            return model;
        }

        [HttpPut]
        public BaseResponse UpdateTask(TaskReqs reqs)
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
            var existingTask = tasks.FirstOrDefault(t => t.Id == reqs.Id);
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

        [HttpDelete]
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
            var TaskDto = tasks.FirstOrDefault(t => t.Id == id);
            if (TaskDto == null)
            {
                model.code = 1;
                model.SetResponseError(ResponseError.NotData);
                model.data = resp;
                return model;
            }
            tasks.Remove(TaskDto);
            resp.IsSuccess = true;
            model.data = resp;
            return model;
        }
    }
}
