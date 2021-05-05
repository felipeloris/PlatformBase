using Loris.Common.Domain.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Loris.Common.EF.Repository
{
    public interface IRepository<TEntityId> : IDisposable where TEntityId : class, IEntityIdBase
    {
        TEntityId Get(params object[] Keys);

        TEntityId Get(Expression<Func<TEntityId, bool>> where);

        TEntityId Add(TEntityId obj);

        bool Update(TEntityId obj);

        bool Delete(TEntityId obj);

        bool Delete(params object[] Keys);

        bool Delete(Expression<Func<TEntityId, bool>> where);

        IQueryable<TEntityId> Query();

        IQueryable<TEntityId> Query(params Expression<Func<TEntityId, object>>[] includes);

        IQueryable<TEntityId> QueryFast();

        IQueryable<TEntityId> QueryFast(params Expression<Func<TEntityId, object>>[] includes);
    }
}
