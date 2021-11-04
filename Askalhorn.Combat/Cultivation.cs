using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Askalhorn.Utils;
using Newtonsoft.Json;

namespace Askalhorn.Combat
{
    public class Cultivation: ICultivation
    {
        public ILinearParameter<int> Level => _level;

        private readonly LinearParameter<int> _level;
        
        public ILimitedValue<IObservedParameter<int>> Energy  => _energy;

        private readonly ObservedLimitedValue<int> _energy;
        
        [JsonConstructor]
        public Cultivation(LinearParameter<int> level, ObservedLimitedValue<int> energy)
        {
            _level = level;
            _energy = energy;
        }

        public Cultivation(int level = 1)
        {
            _level = new LinearParameter<int>
            {
                Base = new ObservedParameter<int>(level),
            };

            _energy = new ObservedLimitedValue<int>(0)
            {
                Max = new ObservedParameter<int>(CalculateNewMax(level))
            };
        }
        
        public void AddEnergy(int bonus)
        {
            if (bonus + _energy.Current.Value >= Energy.Max.Value)
            {
                var nextBonus = bonus + _energy.Current.Value - Energy.Max.Value;
                _level.Base.Value++;
                _energy.Max.Value = CalculateNewMax(_level.Base.Value);
                _energy.Current.Value = 0;
                AddEnergy(nextBonus);
            }
            else
                _energy.Current.Value += bonus;
        }

        private static int CalculateNewMax(int level)
        {
            return level * 100;
        }
    }
}