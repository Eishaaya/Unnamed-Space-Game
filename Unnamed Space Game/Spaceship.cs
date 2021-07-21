using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{

    class Spaceship : Sprite, IFollowable
    {
        public enum MoveState
        {
            Stalled,
            Forwards,
            Backwards
        };

        public enum TurnState
        {
            None,
            Left,
            Right
        }

        public enum AutoMoveState
        {
            None,
            Back,
            Forwards
        };
        public Vector2 Position { get; private set; }
        float rotationLimit;
        List<Projectile> shots;
        Vector2 speed;
        float engine;
        KeyboardState ks;
        Keys leftKey;
        Keys rightKey;
        Keys upKey;
        Keys downKey;
        Keys shootKey;
        Vector2 momentum;
        LoopedFloat gunIndex;
        public MoveState CurrentState { get; set; }
        public TurnState CurrentTurn { get; set; }
        public AutoMoveState AutoState { get; set; }

        List<Vector2> gunSpots;
        List<Vector2> engineSpots;

        public float Health { get; set; }
        float shotDamage;
        float shotSpeed;
        float shotAcc;
        float shotScale;
        int shotLife;
        Texture2D shotImage;
        Timer reloadTime;
        Timer stallTime;
        List<AnimatingSprite> laserFlashes;
        Texture2D flashImage;
        AnimationFrame[] flashFrames;
        Vector2[] flashOrigins;
        float flashScale;
        int flashTime;
        ParticleEffect exhaust;
        Texture2D exhaustImage;
        float exhaustSpeed;
        int exhaustTime;
        Vector2 exhaustScale;
        Timer exhaustTimer;
        int exhaustChange;

        //ship
        public Spaceship(Texture2D image, Vector2 location, Color color, float Engine, float health, float RotationLimit, float rotation, List<Vector2> GunSpots, List<Vector2> EngineSpots, int reload, int resetTime,
            //shots
            float sDamage, float sSpeed, float sAcc, float sScale, int sLife, Texture2D sImage,
            //flash
            Texture2D fImage, AnimationFrame[] fFrames, Vector2[] fOrigins, int fTime, float fScale,
            //Exhaust
            Texture2D eImage, float eSpeed, int eTime, int eChange, Vector2 eScale, 
            //basic
            SpriteEffects effects, Vector2 origin, float scale, float depth,
            //defaults
            int defaultETime = 0, 
            Keys LeftKey = Keys.A, Keys RightKey = Keys.D, Keys DownKey = Keys.S, Keys UpKey = Keys.W, Keys ShootKey = Keys.Space)

        : base(image, location, color, rotation, effects, origin, scale, depth)
        {
            exhaust = new ParticleEffect();
            gunIndex = new LoopedFloat(0, GunSpots.Count - 1, 0);
            Health = health;

            shotLife = sLife;
            shotDamage = sDamage;
            shotSpeed = sSpeed;
            shotImage = sImage;
            shotAcc = sAcc;
            shotScale = sScale;

            flashFrames = fFrames;
            flashImage = fImage;
            flashScale = fScale;
            flashTime = fTime;
            flashOrigins = fOrigins;
            laserFlashes = new List<AnimatingSprite>();

            exhaustImage = eImage;
            exhaustSpeed = eSpeed;
            exhaustTime = eTime;
            exhaustChange = eChange;
            exhaustScale = eScale;

            gunSpots = GunSpots;
            engineSpots = EngineSpots;
            Position = location;
            shots = new List<Projectile>();
            engine = Engine;
            rotationLimit = RotationLimit;
            leftKey = LeftKey;
            rightKey = RightKey;
            upKey = UpKey;
            downKey = DownKey;
            shootKey = ShootKey;
            CurrentState = MoveState.Stalled;
            momentum = Vector2.Zero;
            reloadTime = new Timer(reload);
            stallTime = new Timer(resetTime);

            if (defaultETime == 0)
            {
                exhaustTimer = new Timer(30);
            }
        }

        public void MakeExhaust()
        {
            foreach (Vector2 engineSpot in engineSpots)
            {
                var tempLocation = Vector2.Transform(engineSpot - Origin, Matrix.CreateRotationZ(rotation)) + Location;
                var newPart = ObjectPool<Particle>.Instance.Borrow<Particle>();
                newPart.SetParticle(exhaustImage, tempLocation, Color.Gold, Color.Red, rotation, effect, new Vector2(exhaustImage.Width / 2, exhaustImage.Height / 2), new Vector2((float) -Math.Sin(rotation) * exhaustSpeed, (float)Math.Cos(rotation) * exhaustSpeed) + new Vector2(0, momentum.Y), exhaustTime, exhaustScale, 1, 1, 0, false, random.Next((int)-exhaustSpeed, (int)exhaustSpeed), (int)exhaustSpeed, exhaustChange);
                exhaust.AddParticle(newPart);
            }
        }

        public void Shoot()
        {
            var newShot = ObjectPool<Projectile>.Instance.Borrow<Laser>();
            var tempLocation = Vector2.Transform(gunSpots[(int)gunIndex] - Origin, Matrix.CreateRotationZ(rotation)) + Location;
            newShot.SetProjectile(shotImage, tempLocation, Color.White, rotation, shotDamage, shotSpeed, shotAcc, shotLife, effect, new Vector2(shotImage.Width / 2, shotImage.Height), shotScale, 1);
            shots.Add(newShot);

            var newFlash = ObjectPool<AnimatingSprite>.Instance.Borrow<AnimatingSprite>();
            newFlash.SetAnimatingSprite(flashImage, tempLocation, Color.White, rotation, effect, Rectangle.Empty, flashOrigins[0], flashScale, Depth, flashFrames, flashTime, flashOrigins);
            laserFlashes.Add(newFlash);
            gunIndex++;
        }

        public void Update(GameTime gametime)
        {
            exhaustTimer.Tick(gametime);
            if (exhaustTimer.Ready())
            {
                MakeExhaust();
            }
            exhaust.Update(gametime);
            reloadTime.Tick(gametime);
            var deadPews = new List<Laser>();
            foreach (Laser pewpew in shots)
            {
                pewpew.move(gametime);
                if (pewpew.LifeTimer.Ready())
                {
                    ObjectPool<Projectile>.Instance.Return<Laser>(pewpew);
                    deadPews.Add(pewpew);
                }
            }
            foreach (Laser pooPew in deadPews)
            {
                shots.Remove(pooPew);
            }

            var DoneFrames = new List<AnimatingSprite>();
            foreach (AnimatingSprite flash in laserFlashes)
            {
                if (flash.currentframe == flash.Frames.Length - 1)
                {
                    DoneFrames.Add(flash);
                }
                else
                {
                    flash.Animate(gametime);
                }
            }
            foreach (AnimatingSprite doneFlash in DoneFrames)
            {
                if (doneFlash.Fade(60))
                {
                    laserFlashes.Remove(doneFlash);
                    ObjectPool<AnimatingSprite>.Instance.Return<AnimatingSprite>(doneFlash);
                }

            }


            ks = Keyboard.GetState();

            if (ks.IsKeyDown(shootKey))
            {
                if (reloadTime.Ready())
                {
                    Shoot();
                }
            }

            if (ks.IsKeyDown(upKey))
            {
                CurrentState = MoveState.Forwards;
            }
            else if (ks.IsKeyDown(downKey))
            {
                CurrentState = MoveState.Backwards;
            }
            else
            {
                CurrentState = MoveState.Stalled;
            }

            if (ks.IsKeyDown(leftKey))
            {
                rotation = MathHelper.Lerp(rotation, -rotationLimit, .05f);
                CurrentTurn = TurnState.Left;
                stallTime.Reset();
            }
            else if (ks.IsKeyDown(rightKey))
            {
                rotation = MathHelper.Lerp(rotation, rotationLimit, .05f);
                CurrentTurn = TurnState.Right;
                stallTime.Reset();
            }
            else
            {
                rotation = MathHelper.Lerp(rotation, 0, .1f);
                CurrentTurn = TurnState.None;
            }


            if (CurrentState == MoveState.Forwards)
            {
                speed = new Vector2(engine * (float)Math.Sin((double)rotation), engine * -(float)Math.Cos((double)rotation));
                stallTime.Reset();
            }
            else if (CurrentState == MoveState.Backwards)
            {
                speed = new Vector2(engine * (float)Math.Sin((double)rotation), engine * (float)Math.Cos((double)rotation));
                stallTime.Reset();
            }
            else if (CurrentState == MoveState.Stalled)
            {
                if (CurrentTurn == TurnState.None && Location != Position)
                {
                    stallTime.Tick(gametime);
                    if (stallTime.Ready(false))
                    {
                        Position = Vector2.Lerp(Position, new Vector2(Position.X, Location.Y), .01f);
                    }
                    speed = Vector2.Zero;
                }
                else
                {
                    speed = new Vector2(engine * (float)Math.Sin((double)rotation), 0);
                }
            }
            momentum = Vector2.Lerp(momentum, speed, .03f);
            Location += momentum;
        }

        public override void Draw(SpriteBatch batch)
        {
            exhaust.Draw(batch);
            base.Draw(batch);
            foreach (Laser pewpew in shots)
            {
                pewpew.Draw(batch);
            }
            foreach (AnimatingSprite sprite in laserFlashes)
            {
                sprite.Draw(batch);
            }
        }
    }
}
