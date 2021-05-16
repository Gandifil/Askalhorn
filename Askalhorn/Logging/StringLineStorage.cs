using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace Askalhorn.Logging
{
    public static class StringLineStorage
    {
        /// <summary>
        /// Saved string lines.
        /// </summary>
        public static IEnumerable<string> Logs => lines;

        private static ConcurrentQueue<string> lines = new ConcurrentQueue<string>();
        private const int LINE_LIMIT = 10;

        /// <summary>
        /// Write line to multithread storage.
        /// </summary>
        /// <param name="line">A message</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// The default is <code>"[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"</code>.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="line"/> is empty or <code>null</code></exception>
        public static void WriteLine(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException(nameof(line));
                
            lines.Enqueue(line);
            if (lines.Count > LINE_LIMIT)
                lines.TryDequeue(out string _);
        }
    }
}