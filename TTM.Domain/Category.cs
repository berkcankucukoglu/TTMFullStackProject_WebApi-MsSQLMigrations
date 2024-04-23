using TTM.Domain.Interfaces;

namespace TTM.Domain
{
    public class Category : IBaseEntity
    {
        public Category()
        {
            Projects = new List<Project>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //NavProps
        public int? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Project> Projects { get; set; }

    }
}
