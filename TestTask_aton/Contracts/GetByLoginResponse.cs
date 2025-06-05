namespace TestTask_aton.Contracts
{
    public record GetByLoginResponse
    (
        string Name,
        int Gender,
        DateTime? BirthDay,
        string IsActive
    );
}
