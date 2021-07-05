using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MLEM.Extended.Extensions;
using MLEM.Textures;
using MLEM.Ui;
using MLEM.Ui.Elements;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Components
{
    public class ActionBlock: IActionBlock
    {
        public TextureRegion2D Region { get; set; }

        public Action Action { get; set; }
        public Keys Key { get; set; }
    }
}