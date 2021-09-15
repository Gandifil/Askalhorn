﻿using Askalhorn.Common;
using Askalhorn.Map.Impacts;
using Askalhorn.Map.Local;
using Askalhorn.Render;
using Microsoft.Xna.Framework;

namespace Askalhorn.Map.Builds
{
    class GlobalTeleport: GameObject, IBuild
    {
        public IBuild.Types Type => IBuild.Types.Teleport;

        public IRenderer Renderer { get; set; } = new ParticleRenderer(new ParticleRenderer.Settings()
        {
            StartColor = Color.BlueViolet,
            EndColor = Color.Red,
            Capacity = 600,
            Radius = 40,
        });

        public LocationInfo Location { get; set; }

        public uint Place { get; set; }

        public IImpact Impact => new EnterLocationImpact(Location, Place);
    }
}