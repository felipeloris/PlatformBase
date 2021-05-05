namespace Loris.Common.Domain.Interfaces
{
    /// <summary>
    /// Interface usada para realizar a validação do usuário e/ou senha
    /// (permite estender a autenticação de usuários para outros métodos
    /// além de validação por banco de dados)
    /// </summary>
    public interface IUserAuthentication
    {
        bool ValidateUser(ILogin login);

        bool ValidatePassword(ILoginPwd login);

        void ChangePassword(ILoginPwd login, string newPassword);
    }
}
