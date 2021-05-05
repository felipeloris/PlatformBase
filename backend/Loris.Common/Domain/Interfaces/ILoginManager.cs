namespace Loris.Common.Domain.Interfaces
{
    public interface ILoginManager
    {
        bool Logged { get; }

        ILogin Login { get; }

        void SetCulture(Languages languageCode);
    }
}
