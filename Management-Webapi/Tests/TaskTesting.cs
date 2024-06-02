using Management_Webapi.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Management_Webapi.Controllers;
using Management_Webapi.Model.Dto;
using Management_Webapi.Model.Request;
using Management_Webapi.Model.Response;

namespace Management_Webapi.Tests
{
    public class TaskTesting
    {
        [Fact]
        public void CanCreateTask()
        {
            var controller = new ManagerController();
            var task = new TaskReqs
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Test Task",
                Description = "Test Description",
                Priority = Priority.High,
                DueDate = DateTime.Now.AddDays(1)
            };
            var result = controller.CreateTask(task);
            Assert.NotNull(result);
            Assert.IsType<BaseResponse>(result);
            var response = (BaseResponse)result;
            Assert.NotNull(response.data);
            Assert.IsType<TaskResp>(response.data);
        }

        [Fact]
        public void CanGetTaskById()
        {
            // Khởi tạo controller
            var controller = new ManagerController();
            // Tạo một task mới
            string taskId = Guid.NewGuid().ToString();
            var task = new TaskReqs
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                Priority = Priority.High,
                DueDate = DateTime.Now.AddDays(1)
            };
            var createTask = controller.CreateTask(task);
            var lstTask = controller.GetTasks();
            // Kiểm tra danh sách không rỗng
            Assert.NotNull(lstTask);
            Assert.True(lstTask.data is IEnumerable<TaskDto> tasks && tasks.Any());
            var firstTask = ((IEnumerable<TaskDto>)lstTask.data).First();
            var result = controller.GetTaskById(firstTask.Id);
            Assert.NotNull(result);
            Assert.IsType<BaseResponse>(result);
            var response = (BaseResponse)result;
            Assert.NotNull(response.data);
            Assert.IsType<TaskDto>(response.data);
            Assert.Equal(task.Title, response.data.Title);
            Assert.Equal(task.Description, response.data.Description);
            Assert.Equal(task.Priority, response.data.Priority);
            Assert.Equal(task.DueDate, response.data.DueDate);
        }


        [Fact]
        public void CanUpdateTask()
        {
            var controller = new ManagerController();
            string taskId = Guid.NewGuid().ToString();
            var task = new TaskReqs
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                Priority = Priority.High,
                DueDate = DateTime.Now.AddDays(1)
            };
            var createTask = controller.CreateTask(task);

            var lstTask = controller.GetTasks();
            Assert.NotNull(lstTask);
            Assert.True(lstTask.data is IEnumerable<TaskDto> tasks && tasks.Any());
            var firstTask = ((IEnumerable<TaskDto>)lstTask.data).First();
            var updatedTask = new TaskReqs
            {
                Id = firstTask.Id,
                Title = "Updated Task",
                Description = "Updated Description",
                Priority = Priority.Medium,
                DueDate = DateTime.Now.AddDays(2)
            };
            var updateResult = controller.UpdateTask(updatedTask);
            Assert.NotNull(updateResult);
            Assert.IsType<BaseResponse>(updateResult);
            var updateResponse = (BaseResponse)updateResult;
            Assert.NotNull(updateResponse.data);
            Assert.IsType<TaskResp>(updateResponse.data);
            var responseTask = (TaskResp)updateResponse.data;
            Assert.Equal(updatedTask.Title, responseTask.Title);
            Assert.Equal(updatedTask.Description, responseTask.Description);
            Assert.Equal(updatedTask.Priority, responseTask.Priority);
            Assert.Equal(updatedTask.DueDate, responseTask.DueDate);
        }


        [Fact]
        public void CanDeleteTask()
        {
            var controller = new ManagerController();
            string taskId = Guid.NewGuid().ToString();
            var task = new TaskReqs
            {
                Id = taskId,
                Title = "Test Task",
                Description = "Test Description",
                Priority = Priority.High,
                DueDate = DateTime.Now.AddDays(1)
            };
            var createTask = controller.CreateTask(task);
            var lstTask = controller.GetTasks();
            Assert.NotNull(lstTask);
            Assert.True(lstTask.data is IEnumerable<TaskDto> tasks && tasks.Any());
            var firstTask = ((IEnumerable<TaskDto>)lstTask.data).First();   
            var result = controller.DeleteTask(firstTask.Id);
            Assert.NotNull(result);
            Assert.IsType<BaseResponse>(result);
            var deleteResponse = (BaseResponse)result;
            Assert.NotNull(deleteResponse.data);
            Assert.IsType<TaskDeleteResp>(deleteResponse.data);
            var responseTask = (TaskDeleteResp)deleteResponse.data;
            Assert.True(responseTask.IsSuccess);
        }

    }
}

