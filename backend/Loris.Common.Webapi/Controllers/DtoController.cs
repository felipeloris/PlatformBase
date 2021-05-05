using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Log;
using Loris.Common.Webapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Loris.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class DtoController<TAppService, TDto> : ControllerBase, IController<TDto>
        where TAppService : IApplicationService<TDto>
        where TDto : class
    {
        private static readonly LogManager<DtoController<TAppService, TDto>> Logger = new LogManager<DtoController<TAppService, TDto>>();
        protected TAppService appService;

        public DtoController(TAppService appService)
        {
            //var services = this.HttpContext.RequestServices;
            //Logger = (ILogger)services.GetService(typeof(ILogger));

            this.appService = appService;
        }

        protected ActionResult ConvertTreatedResult<T>(T result) where T : TreatedResult
        {
            // https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status

            switch (result.Status)
            {
                case TreatedResultStatus.SuccessWarning:
                case TreatedResultStatus.Success:
                    return Ok(result);

                case TreatedResultStatus.NotAuthorized:
                    return Unauthorized(result);

                case TreatedResultStatus.NoDataFound:
                    //return NoContent();
                    return NotFound(result);

                case TreatedResultStatus.TimeoutError:
                    // 408 Request Timeout
                    return StatusCode(408, result);

                case TreatedResultStatus.Error:
                case TreatedResultStatus.CriticalError:
                case TreatedResultStatus.InternalServerError:
                    // 500 Internal Server Error
                    return StatusCode(500, result);

                case TreatedResultStatus.BusinessError:
                case TreatedResultStatus.NotValidate:
                    //return ValidationProblem();
                    return BadRequest(result);

                case TreatedResultStatus.GoTo:
                    //return Redirect(result);
                    return Ok(result);
            }

            return Unauthorized();
        }

        // GET: api/<Controller>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TDto>>> Get()
        {
            var result = await appService.Get();

            return ConvertTreatedResult(result);
        }

        // GET api/<Controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TDto>> Get(int id)
        {
            var result = await appService.Get(id);

            return ConvertTreatedResult(result);
        }

        // POST api/<Controller>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TDto obj)
        {
            var result = await appService.Add(obj);

            return ConvertTreatedResult(result);
        }

        // PUT api/<Controller>/0
        [HttpPut("{anotherUserSecondsToChange}")]
        public async Task<ActionResult> Put(int anotherUserSecondsToChange, [FromBody] TDto obj)
        {
            var result = await appService.Update(obj, anotherUserSecondsToChange);

            return ConvertTreatedResult(result);
        }

        // DELETE api/<Controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await appService.Delete(id);
            
            return ConvertTreatedResult(result);
        }

        // POST api/<Controller>/GetByParameters
        [HttpPost("GetByParameters")]
        [OpenApiOperation("GetByParameters")]
        public async Task<ActionResult<PageResult<TDto>>> GetByParameters([FromQuery] RequestParameter parameters)
        {
            var result = await appService.Get(parameters);

            return ConvertTreatedResult(result);
        }
    }
}
