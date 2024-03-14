namespace TTM
{
    public class ProjectDto
    {
        public ProjectDto()
        {
            Duties = new List<DutyDto>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public ICollection<DutyDto> Duties { get; set; }
    }
}
