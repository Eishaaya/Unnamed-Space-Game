using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unnamed_Space_Game
{
    public struct Timer
    {
        public TimeSpan wait;
        TimeSpan until;

        public static implicit operator TimeSpan (Timer timer) => timer.wait;
        public static implicit operator Timer (TimeSpan span) => new Timer(span);
        public static implicit operator Timer (int time) => new Timer(time);

        public Timer(TimeSpan until)
        {
            this.until = until;
            this.wait = TimeSpan.Zero;
        }


        public Timer(int length)
            : this(TimeSpan.FromMilliseconds(length)) { }

        public void Tick(GameTime time)
        {
            this.wait += time.ElapsedGameTime;
        }
        
        public int GetMillies()
        {
            return (int)until.TotalMilliseconds;
        }

        public bool Ready(bool reset = true)
        {
            if(wait >= until)
            {
                if (reset)
                {
                    wait = TimeSpan.Zero;
                }
                return true;
            }
            return false;
        }

        public void Reset()
        {
            wait = TimeSpan.Zero;
        }
    }
}
