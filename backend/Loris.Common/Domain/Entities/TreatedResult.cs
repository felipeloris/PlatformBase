using Loris.Common.Domain.Interfaces;

namespace Loris.Common.Domain.Entities
{
    /// <summary>
    /// Padrão de retorno
    /// </summary>
    public class TreatedResult : ITreatedResult
    {
        /// <summary>
        /// Status da Operação
        /// </summary>
        public TreatedResultStatus Status { get; set; } = TreatedResultStatus.Success;

        /// <summary>
        /// Mensagem de resultado da operação no caso de ter acontecido falha
        /// </summary>
        public string Message { get; set; } = "OK";

        public TreatedResult(TreatedResultStatus status)
        {
            Status = status;
            Message = string.Empty;
        }

        public TreatedResult(TreatedResultStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public bool Error => (
            Status == TreatedResultStatus.Error ||
            Status == TreatedResultStatus.CriticalError);
    }

    /// <summary>
    /// Padrão genêrico de retorno
    /// </summary>
    /// <typeparam name="T">Tipo da Resposta</typeparam>
    public class TreatedResult<T> : TreatedResult, ITreatedResult<T>
    {
        public TreatedResult(TreatedResultStatus status) : base(status)
        {
            Result = default(T);
        }


        public TreatedResult(TreatedResultStatus status, string message) : base(status, message)
        {
            Result = default(T);
        }

        public TreatedResult(TreatedResultStatus status, string message, T result) : this(status, message)
        {
            Result = result;
        }

        public TreatedResult(TreatedResultStatus status, T result) : this(status)
        {
            Result = result;
        }

        /// <summary>
        /// Conteudo da resposta
        /// </summary>
        public T Result { get; set; }
    }
}
