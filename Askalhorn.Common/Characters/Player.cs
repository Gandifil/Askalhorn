using System.Collections.Generic;
using Askalhorn.Common.Control;
using Askalhorn.Common.Inventory.Items;
using Askalhorn.Common.Mechanics.Abilities;
using Askalhorn.Common.Plot;
using Askalhorn.Common.Plot.Quests;
using Askalhorn.Common.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Askalhorn.Common.Characters
{
    internal class Player: Character, IPlayer
    {
        public Player()
        {
            Name = "Вася";
            Fraction = new NamedFraction("Player");
            Renderer = new TextureRenderer("mage2");
            Controller = new BufferController();
            Abilities.Add(new HealMeditation());
            Bag.Put(new PoisonPoition
            {
                Value = 10,
                TurnCount = 5,
            }, 3);
            Bag.Put(new Dagger());
        }

        IJournal IPlayer.Journal => Journal;
        
        public Journal Journal { get;  } = new (new List<IQuest>());
    }
}