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
    }
}