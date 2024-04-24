namespace TTM
{
    public class UserDto
    {
        public UserDto(){}
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Gender? Gender { get; set; }
        public Role? Role { get; set; }
    }
}
