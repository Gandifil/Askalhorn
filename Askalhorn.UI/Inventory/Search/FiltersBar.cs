using System;
using Askalhorn.Common;
using Askalhorn.Inventory;
using Askalhorn.Inventory.Items;
using Askalhorn.Render;
using MLEM.Ui;

namespace Askalhorn.UI.Inventory.Search
{
    public sealed class FiltersBar: FixPanel
    {
        private Filter[] DefaultFilters =
        {   
            new Filter
            {
                Renderer = new TextureRenderer("filters", new(0, 1)),
                TooltipText = "Без фильтра",
                Predicate = _ => true,
            },
            new Filter
            {
                Renderer = new TextureRenderer("filters", new(0, 0)),
                TooltipText = "Оружие",
                Predicate = x=> x.Type == ItemPurpose.Weapon,
            },
            new Filter
            {
                Renderer = new TextureRenderer("filters", new(1, 0)),
                TooltipText = "Броня",
                Predicate = x => 
                    x.Type != ItemPurpose.Weapon &&
                    x.Type != ItemPurpose.Poition &&
                    x.Type != ItemPurpose.Resource,
            },
            new Filter
            {
                Renderer = new TextureRenderer("filters", new(2, 0)),
                TooltipText = "Зелья",
                Predicate = x=> x.Type == ItemPurpose.Poition,
            },
            new Filter
            {
                Renderer = new TextureRenderer("filters", new(1, 1)),
                TooltipText = "Ресурсы",
                Predicate = x=> x.Type == ItemPurpose.Resource,
            },
        };

        public FiltersBar(Anchor anchor, float width, float height, bool scrollOverflow = false) : 
            base(anchor, width, height, scrollOverflow)
        {
            for (int i = 0; i < DefaultFilters.Length; i++)
            {
                var icon = new IconViewer(DefaultFilters[i], Anchor.AutoInline, 0.15f, 1f);
                if (i == _current)
                    icon.ToggleToSelectedState();
                var buffer = i;
                icon.OnPressed += _ => Current = buffer;
                AddChild(icon);
            }
        }

        public event Action OnChanged;

        private int _current = 0;

        public int Current
        {
            get => _current;
            private set
            {
                ((IconViewer)Children[_current]).ToggleToUnselectedState();
                _current = value;
                ((IconViewer)Children[_current]).ToggleToSelectedState();
                OnChanged?.Invoke();
            }
        }

        public Func<IItem, bool> Predicate => DefaultFilters[_current].Predicate;
    }
}