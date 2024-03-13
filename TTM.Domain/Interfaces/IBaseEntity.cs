namespace TTM.Domain.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
