﻿using System;
using System.Collections.Generic;
using Askalhorn.Characters.Abilities;
using Askalhorn.Characters.Control;
using Askalhorn.Combat;
using Askalhorn.Dialogs;
using Askalhorn.Inventory.Items;
using Askalhorn.Plot;
using Askalhorn.Render;

namespace Askalhorn.Characters
{
    public class Player: Character, IPlayer, IHasJournal
    {
        public Player()
        {
            Name = "Вася";
            Fraction = new NamedFraction("Player");
            Renderer = new TextureRenderer("mage2");
            Controller = new BufferController();
            Abilities.Add(new HealMeditation());
            Bag.Put(new ContentItem("dagger01"));
        }

        IJournal IHasReadOnlyJournal.Journal => Journal;
        public Journal Journal { get;  } = new (new List<IQuest>());
        
        public void Make(IMove move)
        {
            var buffer = Controller as BufferController;
            buffer.AddMove(move, this);
        }

        public List<int> HotBindings { get; set; } = new List<int>(new int[10]);
    }
}