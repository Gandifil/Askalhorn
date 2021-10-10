using System;
using Askalhorn.Common;

namespace Askalhorn.Text
{
    public class EnumTextPointer<T>: TextPointer where T: Enum
    {
        public EnumTextPointer(T value)
            :base($"enums/{typeof(T).Name}", value.ToString())
        {
        }

        public static string Get(T value, GrammaticalCase? grammaticalCase = null)
        {
            var pointer = new EnumTextPointer<T>(value)
            {
                GrammaticalCase = grammaticalCase,
            };
            return pointer.ToString();
        }
    }
}