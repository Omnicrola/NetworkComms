namespace NetworkClient.Data
{
    public interface IPersonWriteable : IPerson
    {
        new int Id { get; set; }
        new string FirstName { get; set; }
        new string LastName { get; set; }

    }
}