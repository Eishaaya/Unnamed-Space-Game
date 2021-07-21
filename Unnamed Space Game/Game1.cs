using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Unnamed_Space_Game
{
    public class Game1 : Game
    {
        public static Vector2 bounds = new Vector2(1920, 1080);
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Spaceship ship;
        Texture2D test;
        Enemy testAnimatingSprite;
        Camera camera;
        TextureFrame[][][] frames = new TextureFrame[5][][];
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            graphics.GraphicsProfile = GraphicsProfile.HiDef;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)bounds.X;
            graphics.PreferredBackBufferHeight = (int)bounds.Y;
            graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            Random randall = new Random();

            var stupid = randall.Previous(0, int.MaxValue);

            ObjectPool<Projectile>.Instance.Populate(100, () => new Laser(null, Vector2.Zero, Color.White, 0, 0, 0, 0, 0, SpriteEffects.None, Vector2.Zero, 1, 1));
            ObjectPool<AnimatingSprite>.Instance.Populate(10, () => new AnimatingSprite(null, Vector2.Zero, Color.White, 0, SpriteEffects.None, Rectangle.Empty, Vector2.Zero, 1, 1, null, 0));
            ObjectPool<Particle>.Instance.Populate(30, () => new Particle(null, Vector2.Zero, Color.White, 0, SpriteEffects.None, Vector2.Zero, Vector2.Zero, 0, Vector2.Zero));
            ObjectPool<ParticleEffect>.Instance.Populate(3, () => new ParticleEffect());

            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region shipConstructor
            ship = new Spaceship(image: Content.Load<Texture2D>("Ship"),
                                 location: new Vector2(500, 500),
                                 color: Color.White,
                                 Engine: 10,
                                 health: 100,
                                 RotationLimit: MathHelper.ToRadians(45),
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
                                 fFrames: new RectangleFrame[]
                                 {
                                    new Rectangle(389, 711, 47, 47),
                                    new Rectangle(374, 560, 78, 54),
                                    new Rectangle(361, 421, 107, 53),
                                    new Rectangle(361, 186, 120, 86),
                                    new Rectangle(1191, 660, 267, 225),
                                    new Rectangle(1198, 444, 221, 211),

                                    new Rectangle(1192, 183, 232, 200),

                                 },
                                 fOrigins: new Vector2[]
                                 {
                                    new Vector2(410 - 389, 739 - 711),
                                    new Vector2(410 - 374, 600 - 560),
                                    new Vector2(421 - 361, 462 - 421),
                                    new Vector2(422 - 361, 255 - 186),
                                    new Vector2(1317 - 1191, 820 - 660),

                                    new Vector2(1311 - 1192, 348 - 183),
                                    new Vector2(1313 - 1198, 599 - 444),

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
            #endregion

            var swayFrames = LoadFromFolder("Small Pistachio Alien", "IdleSway");
            var blinkFrames = LoadFromFolder("Small Pistachio Alien", "Blink");

            frames[0] = new TextureFrame[2][];
            frames[(int)Enemy.EnemyState.Idle][0] = swayFrames;
            frames[(int)Enemy.EnemyState.Idle][1] = blinkFrames;


            testAnimatingSprite = new Enemy(null, new Vector2(500, 500), Color.White, 0, SpriteEffects.None, 0, 0, 1, Enemy.MoveType.Swoop, Enemy.AttackType.OneHit, Rectangle.Empty, Vector2.Zero, 1, 1, frames, 35);

            camera = new Camera(ship, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), GraphicsDevice);


            #region Stan's Pain

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
            #endregion
        }

        TextureFrame[] LoadFromFolder(params string[] folderPath)
        {
            var currentDir = Directory.GetCurrentDirectory();

            var path = Path.Combine(currentDir, "Content");
            for (int i = 0; i < folderPath.Length; i++)
            {
                path = Path.Combine(path, folderPath[i]);
            }

            //var IdleFolderPath = Path.Combine(topLevelPathToSmallPistachioAlien, innerFolderPath);

            Dictionary<int, string> map = new Dictionary<int, string>();
            string[] temp = Directory.GetFiles(path);
            foreach (var x in temp)
            {
                var fileName = Path.GetFileName(x);
                fileName = fileName.Substring(0, fileName.Length - 4);

                //Split by space, grab last item (that's your number) parse that then add to map
                string[] split = fileName.Split(' ');
                int key = int.Parse(split[split.Length - 1]);

                string returnPath = "";
                for (int i = 0; i < folderPath.Length; i++)
                {
                    returnPath = Path.Combine(returnPath, folderPath[i]);
                }

                returnPath = Path.Combine(returnPath, fileName);

                map.Add(key, returnPath);
            }

            TextureFrame[] frames = new TextureFrame[map.Count];
            for (int i = 0; i < frames.Length; i++)
            {
                frames[i] = Content.Load<Texture2D>(map[i]);
            }

            return frames;
        }

        protected override void Update(GameTime gameTime)
        {
            testAnimatingSprite.Update(gameTime);
            ship.Update(gameTime);
            camera.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.FromNonPremultiplied(20, 20, 20, 255));
            spriteBatch.Begin(effect: camera.Effect);
            ship.Draw(spriteBatch);
            testAnimatingSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
