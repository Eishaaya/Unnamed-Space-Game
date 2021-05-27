using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    class Enemy : AnimatingSprite
    {
        public enum EnemyState : byte
        {
            Idle,
            Moving,
            Attacking,
            Dying
        }
        bool isDead;
        public EnemyState CurrentState { get; private set; }
        public float Attack { get; }
        private float health = default;
        public float Health
        {
            get => health;

            set
            {
                float newValue = value;
                if (newValue <= 0)
                {
                    isDead = true;
                }
            }
        }

        public Enemy(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, float attack, float health, Rectangle hitbox, Vector2 origin, float scale, float depth, List<List<Rectangle>> frames, int time, List<List<Vector2>> Origins = null)
          : base(image, location, color, rotation, effects, hitbox, origin, scale, depth, frames[(int)EnemyState.Idle], time, Origins == null? null : Origins[(int)EnemyState.Idle])
        {
            CurrentState = EnemyState.Idle;
            isDead = false;
            Attack = attack;
            Health = health;
        }
    }
}
