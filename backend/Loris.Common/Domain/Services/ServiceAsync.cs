using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loris.Common.Domain.Service
{
    public abstract class ServiceAsync<TEntityId, TRepository> : ServiceBase<TEntityId>, IServiceAsync<TEntityId>
        where TEntityId : class, IEntityIdBase
        where TRepository : class, IRepositoryAsync<TEntityId>
    {
        //protected IRepositoryAsync<TEntity> Repository { get; set; }
        protected TRepository Repository { get; }

        protected IDatabase Database { get; }

        public ServiceAsync(ILogin login, IDatabase database)
        {
            Login = login;
            Database = database;
            Repository = (TRepository)Activator.CreateInstance(typeof(TRepository), database);
        }

        protected override TEntityId InternalGetById(object id)
        {
            return GetById(id).Result;
        }
        
        protected void PreparerEntity(object obj)
        {
            // Valida o objeto
            var lstError = GetErrorSummary(obj);
            if (lstError.Count > 0)
                throw new BusinessException(lstError[0]);

            // Registra o usuário e data na entidade
            SetRegisterControl(obj);
        }

        public virtual async Task<IEnumerable<TEntityId>> Get()
        {
            try
            {
                return await Repository.GetAll();
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "GetAll", ref ex))
                    throw ex;
                throw;
            }
        }

        //public virtual async Task<PageResult<TEntity>> Get(Expression<Func<TEntity, bool>> where)

        public virtual async Task<PageResult<TEntityId>> Get(RequestParameter param)
        {
            try
            {
                if ((param == null) ||
                    (param.Filters.Count == 0) ||
                    (param.Filters.Count(s => string.IsNullOrEmpty(s.Field?.Trim())) > 0) ||
                    (param.Filters.Count(s => string.IsNullOrEmpty(s.Value?.Trim())) > 0))
                    throw new ValidationException(ValidationsExceptionType.InvalidParameter);

                return await Repository.Get(param);
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "Get", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<TEntityId> GetById(object id)
        {
            try
            {
                if ((Convert.ToString(id) ?? "").Length == 0)
                    throw new ValidationException(ValidationsExceptionType.IdMustBeProvided);

                return await Repository.GetById(id);
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "GetById", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task<TEntityId> Add(TEntityId obj)
        {
            try
            {
                PreparerEntity(obj);

                // Salva o objeto
                TEntityId item;
                try
                {
                    item = await Repository.Add(obj);
                }
                catch (DataException ex)
                {
                    if (ex.CheckError.ErrorType == DataErrorTypeEnum.UniqueViolation)
                        throw new ValidationException(ValidationsExceptionType.ViolationUniqueField);
                    throw;
                }
                return item;
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "Add", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task Update(TEntityId obj, int anotherUserSecondsToChange)
        {
            try
            {
                PreparerEntity(obj);

                // Verifica se pode realizar a alteração do registro
                CanChangeDataRecord(obj, anotherUserSecondsToChange);

                // Salva o objeto
                try
                {
                    if (await Repository.Update(obj) == 0)
                        throw new ValidationException(ValidationsExceptionType.FailedToExecuteOperation);
                }
                catch (DataException ex)
                {
                    if (ex.CheckError.ErrorType == DataErrorTypeEnum.UniqueViolation)
                        throw new ValidationException(ValidationsExceptionType.ViolationUniqueField);
                    throw;
                }
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "Update", ref ex))
                    throw ex;
                throw;
            }
        }

        public virtual async Task Delete(object id)
        {
            try
            {
                if ((Convert.ToString(id) ?? "").Length == 0)
                    throw new ValidationException(ValidationsExceptionType.IdMustBeProvided);

                if (await Repository.DeleteById(id) == 0)
                    throw new ValidationException(ValidationsExceptionType.FailedToExecuteOperation);
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "Delete", ref ex))
                    throw ex;
                throw;
            }
        }
    }
}
