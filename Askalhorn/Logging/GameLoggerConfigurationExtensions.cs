using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Formatting.Display;

namespace Askalhorn.Logging
{
    public static class GameLoggerConfigurationExtensions
    {
        const string DefaultConsoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
        
        /// <summary>
        /// Writes log events to <see cref="Askalhorn.Logging.StringLineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public static LoggerConfiguration GameLog(
            this LoggerSinkConfiguration sinkConfiguration,
            string outputTemplate = DefaultConsoleOutputTemplate)
        {
            if (sinkConfiguration is null) throw new ArgumentNullException(nameof(sinkConfiguration));
            
            var formatter = new MessageTemplateTextFormatter(outputTemplate);
            return sinkConfiguration.Sink(new GameLogSink(formatter));
        }
    }
}