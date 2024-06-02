namespace Management_Webapi.Model.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }
}
