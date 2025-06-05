using System.Runtime.CompilerServices;
using TestTask_aton.Core.Abstractions;

namespace TestTask_aton.Extentions
{
    public static class RepositoryExtension
    {
        public static async Task InitializeAdmin(this IUsersRepository repository)
        {
            await repository.AdminCheckUp();
        }
    }
}
