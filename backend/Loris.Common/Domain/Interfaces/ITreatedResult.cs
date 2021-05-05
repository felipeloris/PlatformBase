namespace Loris.Common.Domain.Interfaces
{
    public interface ITreatedResult
    {
        TreatedResultStatus Status { get; set; }

        string Message { get; set; }
    }

    public interface ITreatedResult<T> : ITreatedResult
    {
        T Result { get; set; }
    }
}
