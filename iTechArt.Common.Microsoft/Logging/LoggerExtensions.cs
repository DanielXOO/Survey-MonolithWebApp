using Microsoft.Extensions.DependencyInjection;
using iTechArt.Common.Logging;

using ILoggingBuilder = Microsoft.Extensions.Logging.ILoggingBuilder;

namespace iTechArt.Common.Microsoft.Logging
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            builder.Services.AddSingleton<ILoggerProvider, LoggerProvider>();
            builder.Services.AddSingleton(sp =>
            {
                var loggerProvider = sp.GetRequiredService<ILoggerProvider>();

                return loggerProvider.CreateLogger("");
            });

            return builder;
        }
    }
}