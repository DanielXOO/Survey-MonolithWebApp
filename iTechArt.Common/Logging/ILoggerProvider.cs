namespace iTechArt.Common.Logging
{
    public interface ILoggerProvider
    {
        ILogger CreateLogger(string name);
    }
}