using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    class Enemy : AnimatingSprite
    {
        public enum EnemyState
        {
            Idle,
            Moving,
            Attacking,
            Dying
        }
        public enum MoveType
        {
            Swoop,
            Zigzag,
            Charge,
            Teleport
        }
        public enum AttackType
        {
            OneHit,
            Brawl,
            Kamikaze
        }

        protected bool isDead;
        public MoveType moveType { get; }
        public AttackType attackType { get; }
        public EnemyState CurrentState { get; set; }
        public float Attack { get; }
        private float health = default;
        public float AttackRange { get; }
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

        protected AnimationFrame[][][] totalFrames;

        public Enemy(Texture2D image, Vector2 location, Color color, float rotation, SpriteEffects effects, float attack, float attackrange, float health, MoveType movetype, AttackType attacktype, Rectangle hitbox, Vector2 origin, float scale, float depth, AnimationFrame[][][] totalframes, int time, Vector2[][][] Origins = null)
          : base(image, location, color, rotation, effects, hitbox, origin, scale, depth, totalframes[(int)EnemyState.Idle][0], time, Origins == null ? null : Origins[(int)EnemyState.Idle][0])
        {
            moveType = movetype;
            attackType = attacktype;
            CurrentState = EnemyState.Idle;
            isDead = false;
            Attack = attack;
            Health = health;
            AttackRange = attackrange;
            totalFrames = totalframes;
        }

        public void Update(GameTime time)
        {
            if (CurrentState == EnemyState.Idle)
            {
                if (LastFrame)
                {
                    var idleArray = totalFrames[(int)EnemyState.Idle];
                    Frames = idleArray[random.Next(idleArray.Length)];

                }
            }
            Animate(time);
        }
    }
}
