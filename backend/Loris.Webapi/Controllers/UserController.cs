using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Webapi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Loris.Webapi.Controllers
{
    public class UserController : DtoController<IUserAppService, UserDto>
    {
        public UserController(IUserAppService appService)
            : base(appService)
        {
        }

        [AllowAnonymous]
        [HttpPost("GetUser")]
        [OpenApiOperation("GetUser")]
        public async Task<ActionResult<UserDto>> GetUser(string identification)
        {
            var userDto = await appService.GetUser(identification);
            
            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [OpenApiOperation("Login")]
        public async Task<ActionResult> Login(string identification, Languages language, string password)
        {
            var result = await appService.Login(identification, language, password);

            return ConvertTreatedResult(result);
        }

        [HttpPost("Logout")]
        [OpenApiOperation("Logout")]
        public async Task<ActionResult> Logout()
        {
            var result = await appService.Logout();

            return ConvertTreatedResult(result);
        }

        [HttpPost("ValidateLogin")]
        [OpenApiOperation("ValidateLogin")]
        public async Task<ActionResult> ValidateLogin()
        {
            var result = await appService.ValidateLogin();

            return ConvertTreatedResult(result);
        }

        [AllowAnonymous]
        [HttpPost("ChangePassword")]
        [OpenApiOperation("ChangePassword")]
        public async Task<ActionResult> ChangePassword(string identification, Languages language, string password, string newPassword)
        {
            var result = await appService.ChangePassword(identification, language, password, newPassword);
            
            return ConvertTreatedResult(result);
        }

        [AllowAnonymous]
        [HttpPost("GenerateKey")]
        [OpenApiOperation("GenerateKey")]
        public async Task<ActionResult> GenerateKey(string identification, Languages language)
        {
            var result = await appService.GenerateKey(identification, language);

            return ConvertTreatedResult(result);
        }

        [AllowAnonymous]
        [HttpPost("ChangePasswordWithToken")]
        [OpenApiOperation("ChangePasswordWithToken")]
        public async Task<ActionResult> ChangePasswordWithToken(string identification, Languages language, string key, string newPassword)
        {
            var result = await appService.ChangePasswordWithKey(identification, language, key, newPassword);

            return ConvertTreatedResult(result);
        }
    }
}
