using System;
namespace Management_Webapi.Model.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }
    }
}
