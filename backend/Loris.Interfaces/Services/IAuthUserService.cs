using Loris.Entities;
using Loris.Common;
using Loris.Common.Domain.Interfaces;
using System.Threading.Tasks;

namespace Loris.Interfaces.Services
{
    public interface IAuthUserService : IServiceAsync<AuthUser>
    {
        Task<AuthUser> GetUser(string identification, bool includeDependences);

        Task<ILogin> MakeLogin(string identification, string password, Languages language);

        Task MakeLogout(string identification);

        Task<bool> ValidateLogin(string identification, string sessionId);

        Task<ChangePwdStatus> ChangePassword(string identification, string oldPassword, string newPassword);

        Task<ChangePwdStatus> GenerateKey(string identification);

        Task<ChangePwdStatus> ChangePasswordWithKey(string identification, string key, string newPassword);
    }
}
