namespace TTM.Domain.Interfaces
{
    public interface ITaskEntity : IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        string? Description { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        bool Status { get; set; }
    }
}
