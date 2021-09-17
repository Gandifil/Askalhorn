using System;
using System.Collections.Generic;
using System.Linq;
using AmbrosiaGame.Screens;
using Askalhorn.Characters;
using Askalhorn.Common;
using Askalhorn.Elements;
using Askalhorn.UI;
using Microsoft.Xna.Framework;
using MLEM.Extended.Extensions;
using MLEM.Misc;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MLEM.Ui.Style;
using ProgressBar = MLEM.Ui.Elements.ProgressBar;

namespace Askalhorn.Components
{
    public class AbilitiesTabComponent: IGameComponent, IDisposable
    {
        private class Item: IDisposable
        {
            public readonly Panel Box;
            private readonly IAbility ability;
            private readonly Tooltip tooltip;
            private readonly ProgressBar progressBar;
            private readonly Image[] mods;
            
            public Item(IAbility ability)
            {
                this.ability = ability;
                Box = new FixPanel(Anchor.AutoCenter, 0.9f, 0.1f);
                var icon = new Image(Anchor.CenterLeft, new Vector2(-1, 1F), ability.Icon.ToMlem());
                icon.CanBeMoused = true;
                tooltip = new Tooltip(500, ability.ToString(), icon);
                tooltip.MouseOffset = new Vector2(32, -64);
                Box.AddChild(icon);
                
                Box.AddChild(new Paragraph(Anchor.TopRight, 500, ability.Name, true));

                progressBar = new ProgressBar(Anchor.BottomRight, new Vector2(0.8f, 0.5f), Direction2.Right,
                    ability.MaxSkill, ability.Skill);
                Box.AddChild(progressBar);
                var modBox = new Panel(Anchor.TopCenter, new Vector2(0.3f, 0.5f), Vector2.Zero);
                modBox.Texture = null;
                
                mods = new Image[ability.Modifications.Count];
                for (int i = 0; i < mods.Length; i++)
                {
                    var modification = ability.Modifications[i];
                    var image = mods[i] = new Image(Anchor.AutoInlineIgnoreOverflow, new Vector2(-1, 1F), modification.Icon.ToMlem())
                    {
                        Padding = new Padding(3, 3, 3, 3),
                    };
                
                    if (ability.CurrentModification != -1 && ability.CurrentModification != i)
                        image.Color = new StyleProp<Color>(Color.Gray);
                    image.CanBeMoused = true;
                    int x = i;
                    image.OnPressed += element => ability.CurrentModification = x;
                    var tooltip = new Tooltip(500, modification.Description, image);
                    tooltip.MouseOffset = new Vector2(32, -64);
                    modBox.AddChild(image);
                }
                Box.AddChild(modBox);
                ability.OnChange += Update;
            }

            public void Update()
            {
                tooltip.Paragraph.Text = ability.ToString();
                progressBar.CurrentValue = ability.Skill;
                for (int i = 0; i < mods.Length; i++)
                    if (ability.CurrentModification != -1 && ability.CurrentModification != i)
                        mods[i].Color = new StyleProp<Color>(Color.Gray);
                    else
                        mods[i].Color = new StyleProp<Color>(Color.Transparent);
                    
            }

            public void Dispose()
            {
                ability.OnChange -= Update;
            }
        }
        
        private static readonly string NAME = "abilitiesTab";
        private readonly GameProcessScreen screen;
        private List<Item> items;

        public AbilitiesTabComponent(GameProcessScreen screen)
        {
            this.screen = screen;
        }
        
        public void Initialize()
        {
            var box = new FixPanel(Anchor.CenterLeft, 0.45f, 0.9f);

            box.AddChild(new Paragraph(Anchor.AutoCenter, 300, "Способности"));
            items = screen.GameProcess.Player.Abilities.Select(x => new Item(x)).ToList();
            foreach (var item in items)
            {
                box.AddChild(new VerticalSpace(15));
                box.AddChild(item.Box);
            }
            screen.game.UiSystem.Add(NAME, box);
        }

        public void Dispose()
        {
            foreach (var item in items)
                item.Dispose();
            screen.game.UiSystem.Remove(NAME);
        }
        
    }
}