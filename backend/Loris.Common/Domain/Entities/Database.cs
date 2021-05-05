using Loris.Common.Domain.Interfaces;

namespace Loris.Common.Domain.Entities
{
    public class Database : IDatabase
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

        public string ConnString
        {
            get { return $"Server={Server};Port={Port};Database={DbName};User Id={UserId};Password={Password}"; }
        }

        /// <summary>
        /// String de conexao no padrao ADO.NET
        /// </summary>
        public string AdoConnString
        {
            get
            {
                if (IsTrustedSecurity)
                {
                    return $"Data Source={Server};Initial Catalog={DbName};Integrated Security=True;MultipleActiveResultSets=True";
                }

                return $"Data Source={Server};Initial Catalog={DbName};User ID={UserId};Password={Password};MultipleActiveResultSets=True";
            }
        }
    }
}
