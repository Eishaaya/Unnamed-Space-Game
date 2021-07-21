using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    static class Extensions
    {
        public static Random random = new Random();
        public static int Previous(this Random random, int min, int max)
        {
            switch (random.Next(0, 2))
            {
                case 0:
                    return random.Next(int.MinValue, min);
                default:
                    return random.Next(max, int.MaxValue);
            }

        }

        #region behaviors

        public static void Idle(this Enemy enemy)
        {

        }

        public static void Move(this Enemy enemy, Enemy.MoveType type, int[] moveNumber)
        {
            var bounds = Game1.bounds;

            if (enemy.CurrentState != Enemy.EnemyState.Attacking && enemy.CurrentState != Enemy.EnemyState.Dying)
            {
                enemy.CurrentState = Enemy.EnemyState.Moving;
            }


            switch (type)
            {
                case Enemy.MoveType.Swoop:

                    break;
                case Enemy.MoveType.Teleport:
                    break;
                case Enemy.MoveType.Zigzag:
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
