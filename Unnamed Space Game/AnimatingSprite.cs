using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnamed_Space_Game
{
    public class AnimatingSprite : Sprite
    {
        public struct Animation
        {
            List<Rectangle> frames;

            public Animation(List<Rectangle> list)
            {
                frames = list;
            }
        }
        public Rectangle[] Frames { get; set; }
        Vector2[] origins;
        public TimeSpan frametime {get; set;}
        TimeSpan tick;
        public int currentframe;

        public bool LastFrame 
        {
            get => currentframe == Frames.Length - 1;
        }

        public AnimatingSprite (Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Rectangle hitbox, Vector2 origin, float scale, float depth, Rectangle[] frames, int time, Vector2[] Origins = null)
            :base(image, location, color, rotation, effects, origin, scale, depth)
        {
            Frames = frames;
            frametime = new TimeSpan(0, 0, 0, 0, time);
            tick = TimeSpan.Zero;
            origins = Origins;
            currentframe = 0;


            if (origins != null && origins.Length < frames.Length)
            {
                origins = null;
            }

        }
        public void SetAnimatingSprite(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, Rectangle hitbox, Vector2 origin, float scale, float depth, Rectangle[] frames, int time, Vector2[] Origins = null)
        {
            Frames = frames;
            frametime = new TimeSpan(0, 0, 0, 0, time);
            tick = TimeSpan.Zero;
            origins = Origins;
            currentframe = 0;


            if (origins != null && origins.Length < frames.Length)
            {
                origins = null;
            }

            Location = location;
            Color = color;
            Origin = origin;
            Scale = scale;
            Depth = depth;
            effect = effects;
            this.rotation = rotation;
            originalColor = color;
            oldScale = Scale;
            oldRotation = rotation;
            random = new Random();
            Image = image;

            offset = Vector2.Zero;
            moved = false;
            bigger = false;
            sizeSet = float.NaN;
            degreeSet = float.NaN;
            spotSet = new Vector2(float.NaN, float.NaN);

        }
        public void Animate(GameTime gametime)
        {
            tick += gametime.ElapsedGameTime;
            if(tick >= frametime)
            {
                currentframe++;
                tick = TimeSpan.Zero;
            }
            if(currentframe >= Frames.Length)
            {
                currentframe = 0;
            }
        }   
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(Image, Location, Frames[currentframe], Color, rotation, origins == null? Origin: origins[currentframe], Scale, effect, Depth);
        }
    }
}