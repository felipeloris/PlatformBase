using Loris.Application.Dtos;
using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Webapi.Domain.Entities;
using System.Threading.Tasks;

namespace Loris.Application.Interfaces
{
    public interface IUserAppService : IApplicationService<UserDto>
    {
        Task<UserDto> GetUser(string identification);

        Task<TreatedResult<JwtResult>> Login(string identification, Languages language, string password);

        Task<TreatedResult> Logout();

        Task<TreatedResult> ValidateLogin();

        Task<TreatedResult<ChangePwdStatus>> ChangePassword(string identification, Languages language, string password, string newPassword);

        Task<TreatedResult<ChangePwdStatus>> GenerateKey(string identification, Languages language);

        Task<TreatedResult<ChangePwdStatus>> ChangePasswordWithKey(string identification, Languages language, string token, string newPassword);
    }
}
