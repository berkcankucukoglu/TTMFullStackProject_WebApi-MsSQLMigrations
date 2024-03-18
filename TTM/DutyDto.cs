namespace TTM
{
    public class DutyDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? Status { get; set; }
        public float? Hours { get; set; }
        public int? UserId { get; set; }
        public int? ProjectId { get; set; }
    }
}
