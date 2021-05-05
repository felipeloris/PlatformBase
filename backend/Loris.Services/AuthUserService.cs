using Loris.Common.Exceptions;
using Loris.Entities;
using Loris.Infra.Repositories;
using Loris.Common.Domain.Interfaces;
using Loris.Common.Domain.Service;
using Loris.Common.Log;
using Loris.Common.Helpers;
using System;
using System.Threading.Tasks;
using Loris.Common;
using Loris.Interfaces.Services;
using System.Collections.Generic;
using Loris.Resources;
using Loris.Common.Tools;

namespace Loris.Services
{
    public class AuthUserService : ServiceAsync<AuthUser, AuthUserRep>, IAuthUserService
    {
        private static readonly LogManager<AuthUserService> Logger = new LogManager<AuthUserService>();
        private const byte WrondPwdAttempts = 5;
        private const byte MinimumLengthOfPwd = 6;
        private const byte MaximumLengthOfPwd = 15;

        private IUserAuthentication userAuthentication;

        public AuthUserService(IDatabase database, IUserAuthentication userAuthentication)
            : base(null, database)
        {
            this.userAuthentication = userAuthentication;
        }

        public AuthUserService(ILogin login, IDatabase database, IUserAuthentication userAuthentication)
            : base(login, database)
        {
            this.userAuthentication = userAuthentication;
        }

        #region Métodos privados

        private bool ValidateUser(ILogin login)
        {
            return (login == null) || userAuthentication.ValidateUser(login);
        }

        private async Task<bool> ValidatePassword(AuthUser dbUser, string password)
        {
            var pwdOk = false;

            if (dbUser == null)
                return false;

            // Chama o método externo (injetado) para validação da senha
            if (!string.IsNullOrEmpty(password?.Trim()))
            {
                dbUser.Password = password;
                pwdOk = userAuthentication.ValidatePassword(dbUser);
            }

            // Senha errada
            if (!pwdOk)
            {
                dbUser.LoginStatus = LoginStatus.InvalidPassword;
                dbUser.WrondPwdAttempts = (short)(dbUser.WrondPwdAttempts + 1);
                if (dbUser.WrondPwdAttempts >= WrondPwdAttempts)
                {
                    dbUser.DtBlocked = DateTime.Now;
                    dbUser.LoginStatus = LoginStatus.Blocked;
                }

                await base.Update(dbUser, 0);
            }

            return pwdOk;
        }

        private void ResetLoginControl(ILoginControl control)
        {
            control.DtBlocked = null;
            control.DtExpiredPwd = null; // ToDo - definir estrutura/regra para expirar a senha
            control.KeyChangePwd = null;
            control.WrondPwdAttempts = 0;
        }

        #endregion

        public override async Task<AuthUser> Add(AuthUser obj)
        {
            if (!string.IsNullOrEmpty(obj.Password?.Trim()))
                obj.EncryptedPassword = LoginHelper.EncryptPassword(obj.Password.Trim());
            obj.LoginStatus = LoginStatus.ResetPassword;
            obj.WrondPwdAttempts = 0;
            return await base.Add(obj);
        }

        public override async Task Update(AuthUser obj, int anotherUserSecondsToChange)
        {
            var dbUser = await GetUser(obj.ExtenalId, false);
            if (dbUser == null)
                throw new ValidationException(
                    ValidationsExceptionType.FailedToExecuteOperation,
                    "Record not found! " + SerializeObject.ToJson(obj));

            dbUser.PersonId = obj.PersonId;
            dbUser.Nickname = obj.Nickname;
            dbUser.Email = obj.Email;
            dbUser.Language = obj.Language;
            dbUser.Note = obj.Note;

            if (!string.IsNullOrEmpty(obj.Password?.Trim()))
                dbUser.EncryptedPassword = LoginHelper.EncryptPassword(obj.Password.Trim());

            await base.Update(dbUser, anotherUserSecondsToChange);
        }

        /// <summary>
        /// Busca pelo ID do usuário ou e-mail
        /// </summary>
        /// <param name="identification">Id do usuário ou e-mail</param>
        /// <returns></returns>
        public async Task<AuthUser> GetUser(string identification, bool includeDependences)
        {
            if (string.IsNullOrEmpty(identification?.Trim()))
                return null;

            // Busca o usuário no repositório
            var user = await Repository.GetUser(identification, includeDependences);

            return user;
        }

        public async Task<ILogin> MakeLogin(string identification, string password, Languages language)
        {
            try
            {
                Logger.LogDebugStart("MakeLogin");

                // Busca o usuário no repositório
                var dbUser = await GetUser(identification, false);

                // Valida o usuário
                if (ValidateUser(dbUser))
                    return new AuthUser() { LoginStatus = LoginStatus.NotFound };

                // Atualiza o idioma
                dbUser.Language = language;

                // Usuário bloqueado, desabilitado ou com senha expirada
                if ((dbUser.LoginStatus == LoginStatus.Blocked) ||
                    (dbUser.LoginStatus == LoginStatus.Disabled) ||
                    (dbUser.LoginStatus == LoginStatus.ExpiredPassword))
                    return dbUser;

                // Senha expirada
                if ((dbUser.DtExpiredPwd != null) && (dbUser.DtExpiredPwd < DateTime.Now))
                {
                    dbUser.LoginStatus = LoginStatus.ExpiredPassword;

                    await base.Update(dbUser, 0);

                    return dbUser;
                }

                // *************************************
                // * Valida a senha
                // *************************************
                var pwdOk = ValidatePassword(dbUser, password).Result;

                // Senha errada
                if (!pwdOk)
                {
                    dbUser.LoginStatus = LoginStatus.InvalidPassword;
                    return dbUser;
                }

                // Senha fornecida correta, retorna o status 'logado' e atualiza a base
                dbUser.LoginStatus = LoginStatus.Logged;
                dbUser.LoginAt = DateTime.Now;
                dbUser.SessionId = Guid.NewGuid().ToString();
                dbUser.WrondPwdAttempts = 0;
                dbUser.KeyChangePwd = "";
                if (language > 0)
                    dbUser.Language = language;

                await base.Update(dbUser, 0);

                return dbUser;
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "MakeLogin", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("MakeLogin");
            }
        }

        public async Task MakeLogout(string identification)
        {
            try
            {
                Logger.LogDebugStart("MakeLogout");

                // Busca o usuário no repositório
                var dbUser = await GetUser(identification, false);

                // Atualiza o sistema        
                if (dbUser.LoginStatus == LoginStatus.Logged)
                {
                    dbUser.LoginStatus = LoginStatus.NotLogged;
                    dbUser.LogoutAt = DateTime.Now;
                    dbUser.SessionId = null;
                    await base.Update(dbUser, 0);
                }
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "MakeLogout", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("Login");
            }
        }

        public async Task<bool> ValidateLogin(string identification, string sessionId)
        {
            try
            {
                Logger.LogDebugStart("ValidateLogin");

                // Busca o usuário no repositório
                var dbUser = await GetUser(identification, false);
                if (dbUser == null)
                    return false;

                if (dbUser.LoginStatus != LoginStatus.Logged)
                    return false;

                return dbUser.SessionId.Equals(sessionId);
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "ValidateLogin", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("ValidateLogin");
            }
        }

        public async Task<ChangePwdStatus> ChangePassword(string identification, string oldPassword, string newPassword)
        {
            try
            {
                Logger.LogDebugStart("ChangePassword");

                // Busca o usuário no repositório
                var dbUser = await GetUser(identification, false);
                if (ValidateUser(dbUser))
                    return ChangePwdStatus.InvalidUser;
                if (dbUser.LoginStatus == LoginStatus.Blocked)
                    return ChangePwdStatus.BlockedUser;
                if (dbUser.LoginStatus == LoginStatus.NotAuthorized)
                    return ChangePwdStatus.InvalidUser;
                dbUser.Password = oldPassword;

                // ToDo - colocar numa estrutura de validação de regra de senha injetada!!!
                // Valida o tamanho e a nova senha
                if (string.IsNullOrEmpty(newPassword?.Trim())
                    || newPassword.Length < MinimumLengthOfPwd
                    || newPassword.Length > MaximumLengthOfPwd)
                    return ChangePwdStatus.InvalidNewPassword;

                // Valida se a senha antiga é válida
                var pwdOk = ValidatePassword(dbUser, oldPassword).Result;
                if (!pwdOk)
                    return ChangePwdStatus.InvalidOldPassword;

                // Valida se a nova senha é igual a anterior
                var encriptNewPwd = LoginHelper.EncryptPassword(newPassword);
                if (dbUser.EncryptedPassword.Equals(encriptNewPwd))
                    return ChangePwdStatus.InvalidNewPassword;

                // Atualiza a senha no sistema externo de autenticação
                userAuthentication.ChangePassword(dbUser, newPassword);

                // Grava a nova senha 
                ResetLoginControl(dbUser);
                dbUser.EncryptedPassword = LoginHelper.EncryptPassword(newPassword);
                await Repository.Update(dbUser);

                // Retorna o status de senha trocada
                return ChangePwdStatus.PasswordChanged;
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "ChangePassword", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("ChangePassword");
            }
        }

        public async Task<ChangePwdStatus> GenerateKey(string identification)
        {
            try
            {
                Logger.LogDebugStart("GenerateKey");

                // Retorna o usuário
                var dbUser = await GetUser(identification, false);
                if (ValidateUser(dbUser))
                    return ChangePwdStatus.InvalidUser;
                if (dbUser.LoginStatus == LoginStatus.Blocked)
                    return ChangePwdStatus.BlockedUser;
                if (dbUser.LoginStatus == LoginStatus.NotAuthorized)
                    return ChangePwdStatus.InvalidUser;

                // Gera o token e grava no BD
                var sGuidToken = Guid.NewGuid().ToString();
                dbUser.KeyChangePwd = sGuidToken;
                await Repository.Update(dbUser);

                // Envia a chave por e - mail
                var cMsgService = new CourierMessageService(base.Login, Database);
                var cTemplService = new CourierTemplateService(null, Database);
                var sendTokenTemplate = cTemplService.GetByExternalId(CourierTemplateService.SEND_TOKEN).Result;

                if (sendTokenTemplate == null)
                    throw new BusinessException(string.Format(BASERES.msg_template_not_found, CourierTemplateService.SEND_TOKEN));

                var message = new CourierMessage()
                {
                    Action = CourierAction.ToSend,
                    Generator = InternalSystem.PlatformBase,
                    DtInclusion = DateTime.Now,
                    CourierTemplateId = sendTokenTemplate.Id,
                    Title = sendTokenTemplate.Title,
                    Message = string.Format(sendTokenTemplate.Template, sGuidToken),
                    From = sendTokenTemplate.SystemSenderIdent,
                    To = new List<CourierTo>()
                    {
                        new CourierTo(){
                            SystemUserIdent = dbUser.Email,
                            Status = CourierStatus.ToProcess,
                            System = CourierSystem.Email,
                        }
                    },
                };
                await cMsgService.Add(message);

                return ChangePwdStatus.GeneratedKey;
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "GenerateKey", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("GenerateKey");
            }
        }

        public async Task<ChangePwdStatus> ChangePasswordWithKey(string identification, string key, string newPassword)
        {
            try
            {
                Logger.LogDebugStart("ChangePasswordWithKey");

                // Retorna o usuário
                var dbUser = await GetUser(identification, false);
                if (ValidateUser(dbUser))
                    return ChangePwdStatus.InvalidUser;
                if (dbUser.LoginStatus == LoginStatus.Blocked)
                    return ChangePwdStatus.BlockedUser;
                if (dbUser.LoginStatus == LoginStatus.NotAuthorized)
                    return ChangePwdStatus.InvalidUser;

                // Verifica se o token fornecido é igual ao gravado no BD
                if (!dbUser.KeyChangePwd.Equals(key))
                    return ChangePwdStatus.InvalidToken;

                // Valida se a nova senha é igual a anterior
                var encriptNewPwd = LoginHelper.EncryptPassword(newPassword);
                if (dbUser.EncryptedPassword.Equals(encriptNewPwd))
                    return ChangePwdStatus.InvalidNewPassword;

                // Atualiza a senha no sistema externo de autenticação
                userAuthentication.ChangePassword(dbUser, newPassword);

                // Grava a nova senha 
                ResetLoginControl(dbUser);
                dbUser.EncryptedPassword = LoginHelper.EncryptPassword(newPassword);
                dbUser.WrondPwdAttempts = 0;
                dbUser.KeyChangePwd = "";
                await Repository.Update(dbUser);

                // Retorna o status de senha trocada
                return ChangePwdStatus.PasswordChanged;
            }
            catch (Exception ex)
            {
                if (BusinessExceptionHandler.HandleException(GetType(), "ChangePasswordWithKey", ref ex))
                    throw ex;
                throw;
            }
            finally
            {
                Logger.LogDebugFinish("ChangePasswordWithKey");
            }
        }
    }
}
