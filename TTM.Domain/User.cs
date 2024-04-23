using TTM.Domain.Interfaces;

namespace TTM.Domain
{
    public class User : IBasePerson
    {
        public User()
        {
            Projects = new List<Project>();
            Duties = new List<Duty>();
            Categories = new List<Category>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender? Gender { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        //NavProps
        public ICollection<Project> Projects { get; set; }
        public ICollection<Duty> Duties { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
