using Loris.Common.EF.Repository;
using Loris.Entities;
using Loris.Infra.Context;
using Loris.Common.Domain.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Loris.Common.Exceptions;

namespace Loris.Infra.Repositories
{
    public sealed class CourierTemplateRep : RepositoryAsync<CourierTemplate, LorisContext>
    {
        public CourierTemplateRep(IDatabase database) : base(database) 
        { 
        }

        public CourierTemplateRep(LorisContext context) : base(context) 
        { 
        }

        public async Task<CourierTemplate> GetByExternalId(string externalId)
        {
            try
            {
                var query = DbSet
                    .AsNoTracking()
                    .Where(x =>
                        x.ExternalId.ToLower() == externalId.ToLower()
                     );

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                if (DataExceptionHandler.HandleException(GetType(), "GetTemplate", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
