using System.ComponentModel;
using System.Reflection;

namespace Management_Webapi.Model.Request
{
    public class TaskReqs
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public Priority? Priority { get; set; }

        public DateTime DueDate { get; set; }
    }
    public static class ExtTaskReqs
    {
        public static bool IsValidateReq(this TaskReqs reqs,bool isUpdate = false)
        {
            // rule validate
            if(reqs == null) { return false; }
            if(reqs.Title == null || reqs.Priority == null) { return false; }
            if(reqs.DueDate <= DateTime.Now) { return false; }
            if(isUpdate && string.IsNullOrEmpty(reqs.Id)) { return false; }
            return true;
        }
    }
}
