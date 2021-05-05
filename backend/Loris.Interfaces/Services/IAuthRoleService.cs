using Loris.Entities;
using Loris.Common.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Interfaces.Services
{
    public interface IAuthRoleService : IServiceAsync<AuthRole>
    {
        Task<List<AuthRole>> GetUserRoles(int userId);

        Task AddRoleToUser(int roleId, int userId);
        
        Task DeleteUserRole(int roleId, int userId);
    }
}
