using System;

namespace Askalhorn.Common
{
    public interface ITurnBased
    {
        public event Action OnTurned;
        void Turn();
    }
}