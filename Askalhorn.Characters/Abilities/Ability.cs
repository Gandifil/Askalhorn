using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Characters.Abilities
{
    public abstract class Ability: IAbility
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract TextureRegion2D Texture { get; }
        public abstract SoundEffect CastSound { get; }
        public abstract IAbility.TargetType Type { get; }
        public abstract int Radius { get; }
        
        public abstract int CoolDown { get; }

        public int CoolDownTimer { get; private set; } = 0;
        public abstract int MagicCost { get; }
        
        public uint Skill { get; set; } = 0;
        
        public abstract List<IAbility.Modification> Modifications { get; }

        private int _currentModification = -1;

        public int CurrentModification
        {
            get => _currentModification;
            set
            {
                //if (_currentModification == -1)
                {
                    _currentModification = value;
                    OnChanged?.Invoke();
                }
            }
        }
        public event Action OnChanged;

        public abstract uint MaxSkill { get; }

        void IAbility.Use(Character character, Character target)
        {
            CoolDownTimer = CoolDown;
            if (Skill < MaxSkill)
                Skill++;

            OnChanged?.Invoke();
            
            character.MP.Current.Value -= MagicCost;
            Use(character, target);
        }

        public bool IsReady => CoolDownTimer == 0;

        public abstract void Use(Character character, Character target);

        public void Turn()
        {
            if (CoolDownTimer > 0)
                CoolDownTimer -= 1;
        }

        private static string[] TARGET_TYPES_TEXT = {"На себя", "На одну цель"};

        public string TooltipText
        {
            get
            {
                var builder = new StringBuilder();
                builder.AppendLine(Name);
                builder.AppendLine($"Тип: {TARGET_TYPES_TEXT[(int)Type]}");
                builder.AppendLine($"Затраты магии: {MagicCost}");
                builder.AppendLine($"Откат: {CoolDown}");
            
                if (IsReady)
                    builder.AppendLine($"<c Green>Умение готово к использованию</c>");
                else
                    builder.AppendLine($"<c Red>Умение будет готово через {CoolDownTimer} ходов</c>");
            
            
                builder.AppendLine(Description);
            
                return builder.ToString();
            }
        }
    }
}