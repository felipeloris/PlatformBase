using Loris.Common.Domain.Interfaces;
using Loris.Common.EF.Helpers;
using Loris.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;

namespace Loris.Common.EF.Repository
{
    public class RepositoryBase<TEntityId, TContext> : IDisposable
        where TEntityId : class, IEntityIdBase
        where TContext : DbContext
    {
        protected readonly DbContext dbContext;
        protected DbSet<TEntityId> DbSet { get; private set; }

        public RepositoryBase()
        {
            dbContext = Activator.CreateInstance<TContext>();
            Create();
        }

        public RepositoryBase(IDatabase database)
        {
            dbContext = EfCoreHelper.GetContext<TContext>(database.ConnString);
            Create();
        }

        public RepositoryBase(TContext context)
        {
            dbContext = context;
            Create();
        }

        private void Create()
        {
            DbSet = dbContext.Set<TEntityId>();
            DataExceptionHandler.DataCheckError = new PostgresqlCheckError();
        }

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
