using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Unnamed_Space_Game
{
    class My
    {
        public int name;
    }
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
                    return random.Next(max + 1, int.MaxValue);
            }
        }

        public static int Factorial(this int number)
        {
            int result = number;
            while (number > 1)
            {
                number--;
                result *= number;
            }
            return number;
        }

        public static float AddTill(this float number, float endCondition, float amount)
        {
            return number + (float)(int)((endCondition - number) / amount + .99f) * amount;
        }

        public static float LoopCalc(this float current, float target, float max, float min = 0)
        {
            current %= max - min;
            target %= max - min;
            current.AddTill(min, max - min);
            target.AddTill(min, max - min);

            float result;

            if (current < target)
            {
                result = target - current;
                var alt = current + max - min - target;
                result = result < alt ? result : alt * -1;
            }
            else
            {
                result = current - target;
                var alt = current - max + min - target;
                result = result < alt * 1 ? result * -1 : alt;
            }

            return result;
        }

        #region behaviors

        public static void Idle(this Enemy enemy)
        {
            
        }

        public static void Move(this Enemy enemy, Vector2 targetLocation, Enemy.MoveType type, params int[] moveNumber)
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
                case Enemy.MoveType.Charge:
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
