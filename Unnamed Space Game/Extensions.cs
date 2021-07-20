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

        public static void Move (this Enemy enemy, Enemy.MoveType type, int moveNumber)
        {

        }

        #endregion
    }
}
