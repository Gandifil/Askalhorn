using System.Collections.Generic;
using Askalhorn.Common.Control;
using Askalhorn.Common.Mechanics.Abilities;
using Askalhorn.Common.Plot.Quests;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    internal class Player: Character, IPlayer
    {
        public Player()
        {
            Name = "Вася";
            Texture = Storage.Content.Load<Texture2D>("images/mage");
            Controller = new BufferController();
            Abilities.Add(new HealMeditation());
        }

        public IJournal Journal { get; set; } = new Journal(new List<IQuest>
        {
            new Quest()
            {
                Name = "fdssdf",
                Description = "fffffffffffffffff",
            }
        });
    }
}