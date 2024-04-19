namespace TTM
{
    public class UserDto
    {
        public UserDto()
        {
            Projects = new List<ProjectDto>();
            Duties = new List<DutyDto>();
        }
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender? Gender { get; set; }
        public string? Token { get; set; }
        public ICollection<ProjectDto>? Projects { get; set; }
        public ICollection<DutyDto>? Duties { get; set; }
    }
}
