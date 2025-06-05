namespace TestTask_aton.Contracts
{
    public record UsersRequestUpdate
    (
        string Login,
        string Password,
        string Name = "",
        int Gender = -1,
        DateTime? BirthDay = null
    );
}
