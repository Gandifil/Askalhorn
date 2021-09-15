using System;
using System.Collections.Generic;
using Askalhorn.Common;
using Askalhorn.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Render
{
    public class ParticleRenderer: IRenderer
    {
        public class Settings
        {
            public int Capacity { get; set; } = 1000;

            public double TimeSpanSeconds { get; set; } = 1.1;
            public float Radius { get; set; } = 32;
            public Color StartColor { get; set; } = Color.OrangeRed;
            public Color EndColor { get; set; } = Color.Yellow;
        }
        
        private ParticleEffect _particleEffect;
        private Texture2D _particleTexture;


        public ParticleRenderer(Settings settings)
        {
            var GraphicsDevice = Storage.GraphicsDevice;
            _particleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White });

            TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);
            _particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Position = new Vector2(0, -200),
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 
                        settings.Capacity, 
                        TimeSpan.FromSeconds(settings.TimeSpanSeconds),
                        Profile.Ring(settings.Radius, Profile.CircleRadiation.In))
                    {
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(10f, 30f),
                            Quantity = 15,
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(1.0f, 1.0f),
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = settings.StartColor.ToHsl(),
                                        EndValue = settings.EndColor.ToHsl()
                                    },
                                    new ScaleInterpolator { StartValue = new Vector2(3,3), EndValue = new Vector2(1,1) }
                                }
                            },
                            new RotationModifier {RotationRate = -2.1f},
                            //new RectangleContainerModifier {Width = 800, Height = 480},
                            //new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
                        }
                    }
                }
            };
            
            
        }
        
        public void Dispose()
        {
            _particleEffect?.Dispose();
            _particleTexture?.Dispose();
        }
        
        public void Update(GameTime gameTime)
        {
            _particleEffect.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch batch, Vector2 center)
        {
            _particleEffect.Position = center + Vectors.ToCenter;
            batch.Draw(_particleEffect);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_particleEffect);
        }
    }
}