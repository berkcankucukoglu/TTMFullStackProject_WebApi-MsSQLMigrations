namespace TTM.Domain.Interfaces
{
    public interface IBasePerson : IIdentity
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}
