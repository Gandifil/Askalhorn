using System.Text;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Localization
{
    public static class StringExtensions
    {
        public static string WithColor(this string line, string color)
        {
            var builder = new StringBuilder();
            builder.Append("<c ");
            builder.Append(color);
            builder.Append(">");
            builder.Append(line);
            builder.Append("</c>");
            return builder.ToString();
        }
    }
}