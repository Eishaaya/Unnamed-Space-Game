﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Unnamed_Space_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Spaceship ship;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ObjectPool<Projectile>.Instance.Populate(100, () => new Laser(null, Vector2.Zero, Color.White, 0, 0, 0, 0, 0, SpriteEffects.None, Vector2.Zero, 1, 1));
            ObjectPool<AnimatingSprite>.Instance.Populate(10, () => new AnimatingSprite(null, Vector2.Zero, Color.White, 0, SpriteEffects.None, Rectangle.Empty, Vector2.Zero, 1, 1, null, 0));
            ObjectPool<Particle>.Instance.Populate(30, () => new Particle(null, Vector2.Zero, Color.White, 0, SpriteEffects.None, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero));
            ObjectPool<ParticleEffect>.Instance.Populate(3, () => new ParticleEffect());

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ship = new Spaceship(image: Content.Load<Texture2D>("Ship"),
                                 location: new Vector2(500, 500),
                                 color: Color.White,
                                 Engine: 10,
                                 health: 100,
                                 RotationLimit: MathHelper.ToRadians(360),
                                 rotation: 0,
                                 GunSpots: new List<Vector2> { new Vector2(69, 0), new Vector2(81, 0) },
                                 EngineSpots: new List<Vector2> { new Vector2(75.5f, 120) },
                                 reload: 55,
                                 resetTime: 1000,
                                 sDamage: 50,
                                 sSpeed: 20,
                                 sAcc: 1.1f,
                                 sScale: .1f,
                                 sLife: 1000, 
                                 sImage: Content.Load<Texture2D>("Laser"),
                                 fImage: Content.Load<Texture2D>("LaserPewPew"),
                                 fFrames: new List<Rectangle>() {
                new Rectangle(389, 711, 47, 47),
                new Rectangle(374, 560, 78, 54),
                new Rectangle(361, 421, 107, 53),
                new Rectangle(361, 186, 120, 86),
                new Rectangle(1191, 660, 267, 225),
                new Rectangle(1198, 444, 221, 211),

                new Rectangle(1192, 183, 232, 200),

            }, 
                                 fOrigins: new List<Vector2>()
            {
                new Vector2(410 - 389, 739 - 711),
                new Vector2(410 - 374, 600 - 560),
                new Vector2(421 - 361, 462 - 421),
                new Vector2(422 - 361, 255 - 186),
                new Vector2(1317 - 1191, 820 - 660),
                new Vector2(1313 - 1198, 599 - 444),

                new Vector2(1311 - 1192, 348 - 183),

            }, 
                                 fTime: 30,
                                 fScale: .1f,
                                 eImage: Content.Load<Texture2D>("Exhaust"),
                                 eSpeed: 10,
                                 eChange: 70,
                                 eTime: 200,
                                 eScale: new Vector2(.15f), 
                                 effects: SpriteEffects.None,
                                 origin: new Vector2(75.5f, 63),
                                 scale: 1,
                                 depth: 1, 
                                 defaultETime: 1);

            /*
             
            
                                 location: new Vector2(500, 500),
                                 color: Color.White,
                                 Engine: 10,
                                 health: 100,
                                 RotationLimit: MathHelper.ToRadians(45),
                                 rotation: 0,
                                 reload: 55,
                                 resetTime: 1000,

                                 sDamage: 50,
                                 sSpeed: 20,
                                 sAcc: 1.1f,
                                 sScale: .1f,
                                 sLife: 500, 
                                 sImage: Content.Load<Texture2D>("Laser"),
                                 fImage: Content.Load<Texture2D>("LaserPewPew"),
                                 fFrames: new List<Rectangle>() {
                new Rectangle(389, 711, 47, 47),
                new Rectangle(374, 560, 78, 54),
                new Rectangle(361, 421, 107, 53),
                new Rectangle(361, 186, 120, 86),
                new Rectangle(1191, 660, 267, 225),
                new Rectangle(1198, 444, 221, 211),

                new Rectangle(1192, 183, 232, 200),

            }, 
                                 fOrigins: new List<Vector2>()
            {
                new Vector2(410 - 389, 739 - 711),
                new Vector2(410 - 374, 600 - 560),
                new Vector2(421 - 361, 462 - 421),
                new Vector2(422 - 361, 255 - 186),
                new Vector2(1317 - 1191, 820 - 660),
                new Vector2(1313 - 1198, 599 - 444),

                new Vector2(1311 - 1192, 348 - 183),

            }, 
                                 fTime: 30,
                                 fScale: .1f,

                                 eImage: Content.Load<Texture2D>("Exhaust"),
                                 eSpeed: 12,
                                 eTime: 2000,
                                 eScale: new Vector2(.15f), 
                                 scale: 1,
                                 depth: 1);
             
             */

            //var ship1 = SpaceshipBuilder.FromTexturePack("Ship")
            //                            .WithWeapon<Laser>(laserType: Lasers.Red)
            //                            .

        }

        protected override void Update(GameTime gameTime)
        {
            ship.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            ship.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
