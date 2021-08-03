using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    class Toggler : Button
    {
        #region properties
        public Sprite Ball { get; set; }
        public Sprite BottomColor { get; set; }
        public ScalableSprite MovingColor { get; set; }
        public Label Laby { get; set; }
        public bool On { get; set;  }
        public bool Done { get; set; }
        Vector2 setOff;
        #endregion
        public Toggler(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effect, Vector2 origin, float superscale, float depth, Color hovercolor, Color clickedcolor, Sprite Ball, Sprite Bottom, ScalableSprite Moving, SpriteFont font = null, string text = "", float stringH = 50, float offx = 0, float offy = 0, bool On = false)
            : base(image, location, color, rotation, effect, origin, superscale, depth, hovercolor, clickedcolor)
        {
            if (font != null)
            {
                Laby = new Label(font, Color, new Vector2(location.X + image.Width / 2 - (int)font.MeasureString(text).X / 2, location.Y + stringH), text, TimeSpan.Zero);
            }
            this.Ball = Ball;
            BottomColor = Bottom;
            MovingColor = Moving;
            this.On = On;
            if (this.On)
            {
                this.Ball.Location = Location + new Vector2(Image.Width - this.Ball.Image.Width, 0) - setOff;
            }
            else
            {
                this.Ball.Location = Location + setOff;
            }
            setOff = new Vector2(offx, offy);
            this.Ball.Location += setOff;
            MovingColor.scale = new Vector2((this.Ball.Location.X - Location.X) / (Image.Width - this.Ball.Image.Width), MovingColor.scale.Y);
            Done = true;
        }
        public override bool check(Vector2 cursor, bool isclicked)
        {
            Move();
            var tempState = base.check(cursor, isclicked);
            if (!hold)
            {
                if (Done)
                {
                    Done = !tempState;
                    return !Done;
                }
                if (tempState)
                {
                    On = !On;
                    return tempState;
                }
            }
            return false;
        }

        public void Move()
        {
            if (!Done)
            {
                if (!On)
                {
                    toggleRight();
                }
                else
                {
                    toggleLeft();
                }
            }
        }
        void toggleRight()
        {
            Ball.Location = Vector2.Lerp(Ball.Location, Location + new Vector2(Image.Width - Ball.Image.Width, 0) - setOff, .1f);
            MovingColor.scale = new Vector2((Ball.Location.X - Location.X) / (Image.Width - Ball.Image.Width), MovingColor.scale.Y);
            if (Vector2.Distance(Ball.Location, Location + new Vector2(Image.Width - Ball.Image.Width, 0) - setOff) <= .1f)
            {
                Ball.Location = Location + new Vector2(Image.Width - Ball.Image.Width, 0) - setOff;
                On = !On;
                Done = true;
            }
        }
        void toggleLeft()
        {
            Ball.Location = Vector2.Lerp(Ball.Location, Location + setOff, .1f);
            MovingColor.scale = new Vector2((Ball.Location.X - Location.X) / (Image.Width - Ball.Image.Width), MovingColor.scale.Y);
            if (Vector2.Distance(Ball.Location, Location + setOff) <= .1f)
            {
                Ball.Location = Location + setOff;
                On = !On;
                Done = true;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            BottomColor.Draw(batch);
            MovingColor.Draw(batch);
            base.Draw(batch);
            Ball.Draw(batch);
            if (Laby != null)
            {
                Laby.write(batch);
            }
        }
    }
}
