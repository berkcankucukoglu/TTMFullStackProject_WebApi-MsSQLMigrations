namespace TTM
{
    public class CategoryDto
    {
        public CategoryDto()
        {
            Projects = new List<ProjectDto>();
        }
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ProjectDto> Projects { get; set; }
    }
}
