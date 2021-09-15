﻿using System;
using System.Collections.Generic;
using Askalhorn.Characters.Abilities;
using Askalhorn.Characters.Control;
using Askalhorn.Dialogs;
using Askalhorn.Inventory.Items;
using Askalhorn.Plot;
using Askalhorn.Render;

namespace Askalhorn.Characters
{
    public class Player: Character, IPlayer
    {
        public Player()
        {
            Name = "Вася";
            Fraction = new NamedFraction("Player");
            Renderer = new TextureRenderer("mage2");
            Controller = new BufferController();
            Abilities.Add(new HealMeditation());
            // Bag.Put(new PoisonPoition
            // {
            //     Value = 10,
            //     TurnCount = 5,
            // }, 3);
            Bag.Put(new Dagger());
        }

        IJournal IPlayer.Journal => Journal;
        public void Make(IMove move)
        {
            var buffer = Controller as BufferController;
            buffer.AddMove(move, this);
        }
        
        public Journal Journal { get;  } = new (new List<IQuest>());
    }
}