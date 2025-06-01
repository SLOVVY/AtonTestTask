namespace TestTask_aton.Contracts
{
    public record UsersResponse(
        Guid Id,
        string Login,
        string Password,
        string Name,
        int Gender,
        DateTime? BirthDay,
        bool IsAdmin,
        DateTime CreatedAt,
        string CreatedBy,
        DateTime? ModifiedAt,
        string ModifiedBy,
        DateTime? RevokedAt,
        string RevokedBy
    );
}
