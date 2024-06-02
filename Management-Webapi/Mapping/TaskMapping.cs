using Management_Webapi.Model.Dto;
using Management_Webapi.Model.Request;
using Management_Webapi.Model.Response;
using System;
using System.Runtime.CompilerServices;

namespace Management_Webapi.Mapping
{
    public static class TaskMapping
    {
        public static TaskResp ToTaskViewModel(this TaskDto dto)
        {
            var resp = new TaskResp();
            resp.Title = dto.Title;
            resp.Description = dto.Description;
            resp.DueDate = dto.DueDate;
            resp.Priority = dto.Priority;
            return resp;
        }
        public static TaskDto ToModelTaskDto(this TaskReqs dto)
        {
            var resp = new TaskDto();
            string guiId = Guid.NewGuid().ToString();
            resp.Id = guiId;
            resp.Title = dto.Title;
            resp.Description = dto.Description;
            resp.DueDate = dto.DueDate;
            resp.Priority = dto.Priority ?? Model.Priority.Low;
            return resp;
        }
        public static TaskDto ToModelTaskUpdated(this TaskDto dto, TaskReqs reqs)
        {
            dto.Title = reqs.Title;
            dto.Description = reqs.Description;
            dto.DueDate = reqs.DueDate;
            dto.Priority = reqs.Priority ?? Model.Priority.Low;
            return dto;
        }
    }
}
