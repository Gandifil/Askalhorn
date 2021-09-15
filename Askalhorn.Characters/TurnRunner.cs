using System;
using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Map;

namespace Askalhorn.Characters
{
    public abstract class TurnRunner: ITurnBased
    {
        public static TurnRunner Instance;

        public abstract IReadOnlyCollection<Character> Characters { get; }

        public event Action OnTurned;

        protected void InvokeOnTurned()
        {
            OnTurned?.Invoke();
        }

        public virtual void Turn()
        {
            foreach (var obj in Location.Current.Location.GameObjects)
                obj.Turn();
        }
    }
}