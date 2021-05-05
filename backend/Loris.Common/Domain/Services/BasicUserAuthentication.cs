using Loris.Common.Domain.Interfaces;
using Loris.Common.Helpers;

namespace Loris.Common.Domain.Services
{
    public sealed class BasicUserAuthentication  : IUserAuthentication
    {
        public bool ValidateUser(ILogin login)
        {
            // Usuário não foi encontrado
            return login == null;
        }

        /// <summary>
        /// Valida a senha no banco de dados
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ValidatePassword(ILoginPwd login)
        {
            // Criptografa a senha informada no login para comparação
            var encryptedPassword = LoginHelper.EncryptPassword(login.Password);

            // Verifica se a senha informada é a mesma do sistema
            var pwdOk = login.EncryptedPassword.Equals(encryptedPassword);

            return pwdOk;
        }

        public void ChangePassword(ILoginPwd login, string newPassword)
        {
        }
    }
}
