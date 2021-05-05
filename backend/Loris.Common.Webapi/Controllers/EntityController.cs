using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Webapi.Helpers;
using Loris.Common.Webapi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Loris.Common.Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class EntityController<TEntityId, TService> : ControllerBase, IController<TEntityId>
        where TEntityId : class, IEntityIdBase
        where TService : IServiceAsync<TEntityId>
    {
        /*
            C - Create  - POST
            R - Read    - GET
            U - Update  - PUT
            D - Delete  - DELETE         
        */

        protected IDatabase Database = null;

        public EntityController(IDatabase database) 
        {
            Database = database;
        }

        /***********************************************************************************
         * => não foi habilitado, pois é exclusivo pois DbContext é exclusivo de EF
         ***********************************************************************************
        public CrudController(DbContext context)
        {
            var login = User.GetLogin();
            var resourceManager = IotMonitor.Resource.APPDIC.ResourceManager;
            Service = (TService)Activator.CreateInstance(typeof(TService),
                login, context, resourceManager);
        }
        ************************************************************************************/

        #region Métodos protegidos

        protected TService GetService()
        {
            var login = JwtHelper.GetLogin(HttpContext.User);
            return (TService)Activator.CreateInstance(typeof(TService), login, Database);
        }

        #endregion

        // GET api/<Controller>
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntityId>>> Get()
        {
            var list = await GetService().Get();
            return Ok(list);
        }

        // POST api/<Controller>/GetByParameters
        [HttpPost("GetByParameters")]
        [OpenApiOperation("GetByParameters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<ActionResult<PageResult<TEntityId>>> GetByParameters([FromQuery] RequestParameter parameters)
        {
            var pageResult = await GetService().Get(parameters);
            return Ok(pageResult);
        }

        // POST api/<Controller>/GetJsGrid
        [HttpPost("GetJsGrid")]
        [OpenApiOperation("GetJsGrid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetJsGrid([FromBody] RequestParameter param)
        {
            var item = await GetService().Get(param);
            var ret = new
            {
                data = item.Results,
                itemsCount = item.Count
            };

            return Ok(ret);
        }

        // GET api/<Controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TreatedResult), StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<TEntityId>> Get(int id)
        {
            var item = await GetService().GetById(id);
            if (item == null) 
                return NotFound(new TreatedResult(TreatedResultStatus.NoDataFound));
            return Ok(item);
        }

        // POST api/<Controller>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post([FromBody] TEntityId obj)
        {
            var id = await GetService().Add(obj);
            return Ok(id);
        }

        // PUT api/<Controller>/5
        [HttpPut("{anotherUserSecondsToChange}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(TreatedResult), StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult> Put(int anotherUserSecondsToChange, [FromBody] TEntityId obj)
        {
            await GetService().Update(obj, anotherUserSecondsToChange);

            return Ok();
        }

        // DELETE api/<Controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await GetService().Delete(id);

            return Ok();
        }
    }
}
