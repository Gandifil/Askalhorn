using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Modifiers.Containers;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Particles.Profiles;
using MonoGame.Extended.TextureAtlases;

namespace Askalhorn.Render
{
    public class ParticleRenderer: IDisposable, IUpdate
    {
        private ParticleEffect _particleEffect;
        private Texture2D _particleTexture;

        public ParticleRenderer(GraphicsDevice GraphicsDevice)
        {
            _particleTexture = new Texture2D(GraphicsDevice, 1, 1);
            _particleTexture.SetData(new[] { Color.White });

            TextureRegion2D textureRegion = new TextureRegion2D(_particleTexture);
            _particleEffect = new ParticleEffect(autoTrigger: false)
            {
                Position = new Vector2(0, -200),
                Emitters = new List<ParticleEmitter>
                {
                    new ParticleEmitter(textureRegion, 2000, TimeSpan.FromSeconds(1.5),
                        Profile.Ring(50, Profile.CircleRadiation.In))
                    {
                        Parameters = new ParticleReleaseParameters
                        {
                            Speed = new Range<float>(0f, 20f),
                            Quantity = 15,
                            Rotation = new Range<float>(-1f, 1f),
                            Scale = new Range<float>(1.0f, 1.0f)
                        },
                        Modifiers =
                        {
                            new AgeModifier
                            {
                                Interpolators =
                                {
                                    new ColorInterpolator
                                    {
                                        StartValue = Color.OrangeRed.ToHsl(),
                                        EndValue = Color.LightBlue.ToHsl()
                                    },
                                    new ScaleInterpolator { StartValue = new Vector2(3,3), EndValue = new Vector2(1,1) }
                                }
                            },
                            new RotationModifier {RotationRate = -2.1f},
                            //new RectangleContainerModifier {Width = 800, Height = 480},
                            new LinearGravityModifier {Direction = -Vector2.UnitY, Strength = 30f},
                            new VortexModifier()
                            {
                                Mass = 10f,
                                MaxSpeed = 1f,
                                Position = new Vector2(100,0)
                            },
                            new VortexModifier()
                            {
                                Mass = 10f,
                                MaxSpeed = 1f,
                                Position = new Vector2(100,50)
                            },
                            new VortexModifier()
                            {
                                Mass = 10f,
                                MaxSpeed = 1f,
                                Position = new Vector2(90,100)
                            }
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

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(_particleEffect);
        }
    }
}