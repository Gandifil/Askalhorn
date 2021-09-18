using System;
using System.Linq;
using AmbrosiaGame.Screens;
using Microsoft.Xna.Framework;
using MLEM.Ui;
using MLEM.Ui.Elements;

namespace Askalhorn.Components
{
    public class SwitchComponent: IGameComponent
    {
        private const string SWITCH_ELEMENT_NAME = "SWITCH_ELEMENT";
        private readonly UiSystem _uiSystem;

        public SwitchComponent(UiSystem uiSystem)
        {
            _uiSystem = uiSystem;
        }

        public void SwitchTo<T>(Func<T> genNewElement) where T : Element
        {
            var oldElement = _uiSystem.Get(SWITCH_ELEMENT_NAME)?.Element;
            if (oldElement is not null)
                _uiSystem.Remove(SWITCH_ELEMENT_NAME);
            
            if (oldElement is null || oldElement is not T)
                _uiSystem.Add(SWITCH_ELEMENT_NAME, genNewElement());
        }

        public void Initialize()
        {
        }
    }
}