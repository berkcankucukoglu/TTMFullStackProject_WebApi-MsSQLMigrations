namespace TTM.Domain.Interfaces
{
    public interface IBaseEntity : IIdentity
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
