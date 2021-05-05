using System;
using Loris.Common.EF.Repository;
using System.Threading.Tasks;
using Loris.Common.Exceptions;
using Loris.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Loris.Infra.Repositories
{
    public sealed class AuthUserRep : RepositoryAsync<AuthUser, LorisContext>
    {
        public AuthUserRep(IDatabase database) : base(database) 
        { 
        }

        public AuthUserRep(LorisContext context) : base(context) 
        {
        }

        public override async Task<AuthUser> GetById(object id)
        {
            try
            {
                var newId = (int)id;

                var query = DbSet
                    .AsNoTracking()
                    .Include(i => i.AuthUserRole)
                    .ThenInclude(t => t.AuthRole)
                    .ThenInclude(t => t.AuthRoleResource)
                    .ThenInclude(t => t.AuthResource)
                    .Where(x => x.Id == newId);

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetById", ref ex))
                    throw ex;
                throw;
            }
        }

        public async Task<AuthUser> GetUser(string identification, bool includeDependences =  false)
        {
            try
            {
                var identLower = identification.ToLower();

                var query = DbSet
                    .AsNoTracking()
                    .Where(x =>
                        x.Email.ToLower() == identLower || 
                        x.ExtenalId.ToLower() == identLower);

                if (includeDependences)
                {
                    query
                        .Include(i => i.AuthUserRole)
                        .ThenInclude(t => t.AuthRole)
                        .ThenInclude(t => t.AuthRoleResource)
                        .ThenInclude(t => t.AuthResource);
                }

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetUser", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
