namespace TestTask_aton.Contracts
{
    public record UsersRequestCreate
    (
        string Login,
        string Password,
        string Name,
        int Gender,
        DateTime? BirthDay,
        bool IsAdmin
    );
}
