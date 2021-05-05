using AutoMapper;
using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Application.ApplicationService
{
    public class BaseAppService<TInternalService, TEntityId, TDto> : IApplicationService<TDto>
        where TInternalService: IServiceAsync<TEntityId>
        where TEntityId : class, IEntityIdBase
        where TDto : class
    {
        protected TInternalService service;
        protected IMapper mapper;
        protected ILoginManager loginManager;

        public BaseAppService(TInternalService service, IMapper mapper, ILoginManager loginManager)
        {
            this.mapper = mapper;
            this.service = service;
            this.loginManager = loginManager;

            // Validar a sessão do login para todos os métodos de aplicação???
            //if (loginManager.Logged) UserAppService.ValidateLogin => usar algo assim?
        }

        public async Task<TreatedResult<IEnumerable<TDto>>> Get()
        {
            var result = new TreatedResult<IEnumerable<TDto>>(TreatedResultStatus.Success);
            try
            {
                var obj = await service.Get();
                result.Result = mapper.Map<IEnumerable<TDto>>(obj);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }

        public async Task<TreatedResult<PageResult<TDto>>> Get(RequestParameter param)
        {
            var result = new TreatedResult<PageResult<TDto>>(TreatedResultStatus.Success);
            try
            {
                var obj = await service.Get(param);
                result.Result = mapper.Map<PageResult<TDto>>(obj);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }

        public async Task<TreatedResult<TDto>> Get(object id)
        {
            var result = new TreatedResult<TDto>(TreatedResultStatus.Success);
            try
            {
                var obj = await service.GetById(id);
                result.Result = mapper.Map<TDto>(obj);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }

        public async Task<TreatedResult<TDto>> Add(TDto dto)
        {
            var result = new TreatedResult<TDto>(TreatedResultStatus.Success);
            try
            {
                var obj = mapper.Map<TEntityId>(dto);
                var objTask = await service.Add(obj);
                result.Result = mapper.Map<TDto>(objTask);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }

        public async Task<TreatedResult> Update(TDto dto, int anotherUserSecondsToChange)
        {
            var result = new TreatedResult(TreatedResultStatus.Success);
            try
            {
                var obj = mapper.Map<TEntityId>(dto);
                await service.Update(obj, anotherUserSecondsToChange);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }

        public async Task<TreatedResult> Delete(object id)
        {
            // Foi necessário usar TreatedResult pois o Axios no React não recebia a mensagem de BusinessException
            var result = new TreatedResult(TreatedResultStatus.Success);
            try
            {
                await service.Delete(id);

                return result;
            }
            catch (Exception ex)
            {
                if (ex is BusinessException || ex is TreatedErrorException)
                {
                    result.Status = TreatedResultStatus.BusinessError;
                    result.Message = ex.Message;
                    return result;
                }

                throw;
            }
        }
    }
}
