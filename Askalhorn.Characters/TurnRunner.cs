﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var list = Location.Current.Location.GameObjects.ToList();
            
            // on Turn() objects can dispose themself;
            for (int i = list.Count - 1; i >= 0; i--)
                list[i].Turn();
        }
    }
}