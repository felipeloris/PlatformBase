namespace Loris.Common.Domain.Interfaces
{
    public interface IDatabase
    {
        /// <summary>
        /// Nome do servidor onde o banco está instalado
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Porta do servidor onde o banco está instalado
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Nome da base a ser conectada
        /// </summary>
        public string DbName { get; set; }

        /// <summary>
        /// Código do usuário logado
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Senha do usuário logado
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Habilita login pelo windows
        /// </summary>
        public bool IsTrustedSecurity { get; set; }

        public string ConnString { get; }

        /// <summary>
        /// String de conexao no padrao ADO.NET
        /// </summary>
        public string AdoConnString { get; }
    }
}
