namespace Management_Webapi.Model.Response
{
    public class TaskResp
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }
    }
    public class TaskDeleteResp
    {
        public bool IsSuccess { get; set; }
    }
}
