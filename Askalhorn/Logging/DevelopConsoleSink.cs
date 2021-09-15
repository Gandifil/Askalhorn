using System;
using System.IO;
using System.Text;
using Askalhorn.Elements;
using Askalhorn.Text;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Askalhorn.Logging
{
    public class DevelopConsoleSink: ILogEventSink
    {
        const int DefaultWriteBufferCapacity = 100;
        
        readonly ITextFormatter formater;

        /// <summary>
        /// Writes log events to <see cref="LineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public DevelopConsoleSink(ITextFormatter formater)
        {
            if (formater is null)
                throw new ArgumentNullException(nameof(formater));
                
            this.formater = formater;
        }
        
        /// <summary>
        /// Writes log events to <see cref="LineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public void Emit(LogEvent logEvent)
        {
            var buffer = new StringWriter(new StringBuilder(DefaultWriteBufferCapacity));
            formater.Format(logEvent, buffer);
            var line = buffer.ToString();
            switch (logEvent.Level)
            {
                case LogEventLevel.Debug:
                case LogEventLevel.Verbose:
                    line = line.WithColor("LightGreen");
                    break;
                case LogEventLevel.Warning:
                    line = line.WithColor("Orange");
                    break;
                case LogEventLevel.Error:
                case LogEventLevel.Fatal:
                    line = line.WithColor("DarkRed");
                    break;
            }
            DebugConsole.LineStorage.Write(line);
        }
    }
}