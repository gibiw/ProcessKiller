namespace Infrastructure.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}