using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteMeMonogameTest
{
    public class MoveComponent
    {
        public enum MoveState
        {
            Stalled,
            Forwards,
            Backwards
        };
        public MoveState CurrentState { get; set; }
        public Vector2 Momentum { get; }
        public Vector2 Force { get; set; }
        float speed;
        float lerpSpeed;

        public void setSpeed (float rotation)
        {
            if (CurrentState == MoveState.Forwards)
            {
                Force = new Vector2(speed * (float)Math.Sin((double)rotation), speed * -(float)Math.Cos((double)rotation));
            }
            else if (CurrentState == MoveState.Backwards)
            {
                Force = new Vector2(speed * (float)Math.Sin((double)rotation), speed * (float)Math.Cos((double)rotation));
            }
        }
        public void Update()
        {
            Vector2.LerpPrecise(Momentum, Force, lerpSpeed);
        }
    }
}
