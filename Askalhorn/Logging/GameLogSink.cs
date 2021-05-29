﻿using System;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Askalhorn.Logging
{
    public class GameLogSink: ILogEventSink
    {
        const int DefaultWriteBufferCapacity = 100;
        
        readonly ITextFormatter formater;

        /// <summary>
        /// Writes log events to <see cref="Askalhorn.Logging.StringLineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public GameLogSink(ITextFormatter formater)
        {
            if (formater is null)
                throw new ArgumentNullException(nameof(formater));
                
            this.formater = formater;
        }
        
        /// <summary>
        /// Writes log events to <see cref="Askalhorn.Logging.StringLineStorage"/>.
        /// </summary>
        /// <param name="sinkConfiguration">Logger sink configuration.</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <returns>Configuration object allowing method chaining.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="sinkConfiguration"/> is <code>null</code></exception>
        public void Emit(LogEvent logEvent)
        {
            if (logEvent.Level == LogEventLevel.Information)
            {
                var buffer = new StringWriter(new StringBuilder(DefaultWriteBufferCapacity));
                formater.Format(logEvent, buffer);
            
                box.AddChild(new Paragraph(Anchor.AutoLeft, box.Area.Width - 50, buffer.ToString()));
                box.AddChild(new VerticalSpace(10));
                box.ScrollBar.ForceUpdateArea();
                box.ScrollBar.CurrentValue = box.ScrollBar.MaxValue -1;
            }
        }

        private static Panel box;

        public static Element Create()
        {
            box = new Panel(
                Anchor.BottomLeft, 
                new Vector2(0.3f, 0.1f), 
                new Vector2(0, 30), false, true, new Point(15, 20));
            return box;
        }
    }
}