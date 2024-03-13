using TTM.Domain.Interfaces;

namespace TTM.Domain
{
    public class Project : ITaskEntity
    {
        public Project()
        {
            Duties = new List<Duty>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }

        //NavProps
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Duty> Duties { get; set; }

    }
}
