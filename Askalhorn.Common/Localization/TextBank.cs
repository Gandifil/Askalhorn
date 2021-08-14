using System.IO;
using Microsoft.Xna.Framework;

namespace Askalhorn.Common.Localization
{
    public static class TextBank
    {
        public static string Get(string fileName, int index)
        {
            var filePath = $"texts/{fileName}";
            var lines = Storage.Content.Load<TextFile>(filePath);
            return lines.Lines[index];
        }
    }
}