using AutoMapper;
using Loris.Application.Dtos;
using Loris.Application.Interfaces;
using Loris.Entities;
using Loris.Interfaces.Services;
using Loris.Common;
using Loris.Common.Domain.Entities;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Exceptions;
using Loris.Common.Webapi.Domain.Entities;
using Loris.Common.Webapi.Helpers;
using System;
using System.Threading.Tasks;
using Loris.Resources;

namespace Loris.Application.ApplicationService
{
    public class UserAppService : BaseAppService<IAuthUserService, AuthUser, UserDto>, IUserAppService
    {
        public UserAppService(IAuthUserService service, IMapper mapper, ILoginManager loginManager)
            : base(service, mapper, loginManager)
        {
        }

        private string LoginMessage(LoginStatus status)
        {
            switch (status)
            {
                case LoginStatus.Logged:
                    return BASERES.msg_loggin_sucess;
                case LoginStatus.NotFound:
                    return BASERES.msg_invalid_login;
                case LoginStatus.InvalidPassword:
                    return BASERES.msg_invalid_login;
                case LoginStatus.NotLogged:
                    return BASERES.msg_not_logged;
                case LoginStatus.Blocked:
                    //return COMMON.msg_blocked_access;
                    return BASERES.msg_user_blocked_mult_access;
                case LoginStatus.Disabled:
                    return BASERES.msg_disabled_access;
                case LoginStatus.NotAuthorized:
                    return BASERES.msg_user_not_authorized;
                case LoginStatus.ExpiredPassword:
                    return BASERES.msg_password_expired;
            }
            return null;
        }

        private string ChangePwdMessage(ChangePwdStatus status)
        {
            switch (status)
            {
                case ChangePwdStatus.InvalidUser:
                case ChangePwdStatus.InvalidOldPassword:
                    return BASERES.msg_invalid_user_pwd;
                case ChangePwdStatus.BlockedUser:
                    return BASERES.msg_user_blocked_mult_access;
                    //return BASERES.msg_blocked_access;
                case ChangePwdStatus.InvalidNewPassword:
                    return BASERES.msg_invalid_new_pwd;
                case ChangePwdStatus.InvalidNewPasswordEqualOld:
                    return BASERES.msg_invalid_new_pwd;
                case ChangePwdStatus.PasswordChanged:
                    return BASERES.msg_pwd_changed;
                case ChangePwdStatus.GeneratedKey:
                    return BASERES.msg_pwd_key_generated;
            }
            return null;
        }

        public async Task<UserDto> GetUser(string identification)
        {
            var objTask = await service.GetUser(identification, true);
            var obj = mapper.Map<UserDto>(objTask);

            return obj;
        }

        public async Task<TreatedResult<JwtResult>> Login(string identification, Languages language, string password)
        {
            var result = new TreatedResult<JwtResult>(TreatedResultStatus.NotAuthorized);
            loginManager.SetCulture(language);

            try
            {
                var userLogin = await service.MakeLogin(identification, password, language);

                result.Status = TreatedResultStatus.NotValidate;
                result.Message = LoginMessage(userLogin.LoginStatus);

                // Usuário logado
                if (userLogin.LoginStatus == LoginStatus.Logged)
                {
                    var jwtResult = JwtHelper.GenerateJsonWebToken(userLogin);
                    result.Status = TreatedResultStatus.Success;
                    result.Result = jwtResult;
                }

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

        public async Task<TreatedResult> Logout()
        {
            var result = new TreatedResult<JwtResult>(TreatedResultStatus.Success);
            try
            {
                await service.MakeLogout(loginManager.Login.ExtenalId);
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

        public async Task<TreatedResult> ValidateLogin()
        {
            var result = new TreatedResult(TreatedResultStatus.Success);
            try
            {
                var loginJwt = loginManager.Login;
                var loginOk = await service.ValidateLogin(loginJwt.ExtenalId, loginJwt.SessionId);
                if (!loginOk)
                {
                    result.Status = TreatedResultStatus.NotValidate;
                    result.Message = BASERES.msg_session_login_invalid;
                }

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

        public async Task<TreatedResult<ChangePwdStatus>> ChangePassword(string identification, Languages language, string password, string newPassword)
        {
            var result = new TreatedResult<ChangePwdStatus>(TreatedResultStatus.NotAuthorized);
            loginManager.SetCulture(language);

            try
            {
                var status = await service.ChangePassword(identification, password, newPassword);

                result.Status = TreatedResultStatus.NotValidate;
                result.Message = ChangePwdMessage(status);

                // Usuário logado
                if (status == ChangePwdStatus.PasswordChanged)
                {
                    result.Status = TreatedResultStatus.Success;
                    result.Result = status;
                }

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

        public async Task<TreatedResult<ChangePwdStatus>> GenerateKey(string identification, Languages language)
        {
            var result = new TreatedResult<ChangePwdStatus>(TreatedResultStatus.NotAuthorized);
            loginManager.SetCulture(language);

            try
            {
                var status = await service.GenerateKey(identification);

                result.Status = TreatedResultStatus.NotValidate;
                result.Message = ChangePwdMessage(status);

                if (status == ChangePwdStatus.GeneratedKey)
                {
                    result.Status = TreatedResultStatus.Success;
                    result.Result = status;
                }

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

        public async Task<TreatedResult<ChangePwdStatus>> ChangePasswordWithKey(string identification, Languages language, string token, string newPassword)
        {
            var result = new TreatedResult<ChangePwdStatus>(TreatedResultStatus.NotAuthorized);
            loginManager.SetCulture(language);

            try
            {
                var status = await service.ChangePasswordWithKey(identification, token, newPassword);

                result.Status = TreatedResultStatus.NotValidate;
                result.Message = ChangePwdMessage(status);

                if (status == ChangePwdStatus.PasswordChanged)
                {
                    result.Status = TreatedResultStatus.Success;
                    result.Result = status;
                }

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
