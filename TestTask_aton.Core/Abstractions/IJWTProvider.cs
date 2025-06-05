using TestTask_aton.Core.Models;

namespace TestTask_aton.Core.Abstractions
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}