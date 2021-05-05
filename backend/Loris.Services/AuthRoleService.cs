using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Interfaces.Services;
using Loris.Common;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;
using Loris.Common.Exceptions;
using Loris.Common.Extensions;
using Loris.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Services
{
    public class AuthRoleService : ServiceAsync<AuthRole, AuthRoleRep>, IAuthRoleService
    {
        public AuthRoleService(ILogin login, IDatabase database)
            : base(login, database)
        {
        }

        public async Task<List<AuthRole>> GetUserRoles(int userId)
        {
            return await Repository.GetUserRoles(userId);
        }

        public async Task AddRoleToUser(int userId, int roleId)
        {
            var obj = new AuthUserRole()
            {
                AuthUserId = userId,
                AuthRoleId = roleId
            };
            obj.SetRegisterControl(null, Login);

            if (await Repository.AddRoleToUser(obj) == 0)
                throw new BusinessException(BASERES.msg_record_insert);
        }

        public async Task DeleteUserRole(int userId, int roleId)
        {
            var obj = new AuthUserRole()
            {
                AuthUserId = userId,
                AuthRoleId = roleId
            };

            if (await Repository.DeleteUserRole(obj) == 0)
                throw new BusinessException(BASERES.msg_record_delete);
        }

        public async Task AddResourceToRole(int roleId, int resourceId, AccessPermission permissions)
        {
            var obj = new AuthRoleResource()
            {
                AuthRoleId = roleId,
                AuthResourceId = resourceId,
                Permissions = permissions
            };
            obj.SetRegisterControl(null, Login);

            if (await Repository.AddResourceToRole(obj) == 0)
                throw new BusinessException(BASERES.msg_record_insert);
        }

        public async Task DeleteRoleResource(int roleId, int resourceId)
        {
            var obj = new AuthRoleResource()
            {
                AuthRoleId = roleId,
                AuthResourceId = resourceId,
            };

            if (await Repository.DeleteRoleResource(obj) == 0)
                throw new BusinessException(BASERES.msg_record_delete);
        }
    }
}
