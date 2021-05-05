using System;
using System.Collections.Generic;
using Loris.Common.EF.Repository;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Loris.Common.Exceptions;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;

namespace Loris.Infra.Repositories
{
    public sealed class AuthRoleRep : RepositoryAsync<AuthRole, LorisContext>
    {
        public AuthRoleRep(IDatabase database) : base(database) 
        { 
        }

        public AuthRoleRep(LorisContext context) : base(context) 
        {
        }

        public async Task<List<AuthRole>> GetUserRoles(int userId)
        {
            try
            {
                var query =
                    from roles in DbSet
                    join userRoles in dbContext.Set<AuthUserRole>()
                        on roles.Id equals userRoles.AuthRoleId
                    where userRoles.AuthUserId == userId
                    select roles;

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetUserRoles", ref ex))
                    throw ex;
                throw;
            }
        }

        public async Task<int> AddRoleToUser(AuthUserRole obj)
        {
            try
            {
                var r = await dbContext.Set<AuthUserRole>().AddAsync(obj);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "AddRoleToUser", ref ex))
                    throw ex;
                throw;
            }
        }

        public async Task<int> DeleteUserRole(AuthUserRole obj)
        {
            try
            {
                dbContext.Set<AuthUserRole>().Remove(obj);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "DeleteUserRole", ref ex))
                    throw ex;
                throw;
            }
        }

        public async Task<int> AddResourceToRole(AuthRoleResource obj)
        {
            try
            {
                var r = await dbContext.Set<AuthRoleResource>().AddAsync(obj);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "AuthRoleResource", ref ex))
                    throw ex;
                throw;
            }
        }

        public async Task<int> DeleteRoleResource(AuthRoleResource obj)
        {
            try
            {
                dbContext.Set<AuthRoleResource>().Remove(obj);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "DeleteRoleResource", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
