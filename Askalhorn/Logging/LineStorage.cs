using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace Askalhorn.Logging
{
    public class LineStorage
    {
        /// <summary>
        /// Maximum count of saved strings
        /// </summary>
        private const int MAX_COUNT = 50;

        /// <summary>
        /// Saved strings
        /// </summary>
        public IEnumerable<string> Logs => lines;

        private ConcurrentQueue<string> lines = new ();

        /// <summary>
        /// Write line to storage.
        /// </summary>
        /// <param name="line">A message</param>
        /// <exception cref="ArgumentNullException">When <paramref name="line"/> is empty or <code>null</code></exception>
        public void Write(string line)
        {
            if (string.IsNullOrEmpty(line))
                throw new ArgumentNullException(nameof(line));
                
            lines.Enqueue(line);
            if (lines.Count > MAX_COUNT)
                lines.TryDequeue(out string _);
            
            OnWrited?.Invoke(line);
        }

        public event Action<string> OnWrited;
    }
}