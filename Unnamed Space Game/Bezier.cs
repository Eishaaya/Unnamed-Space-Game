using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    struct DualInt
    {
        public int X { get; set; }
        public int Y { get; set; }

        public DualInt(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class Bezier
    {
        static Bezier Instance;
        Dictionary<DualInt, int> cents;
        public static Bezier Get()
        {
            if (Instance == null)
            {
                Instance = new Bezier();
            }
            return Instance;
        }
        Bezier()
        {
            cents = new Dictionary<DualInt, int>();
        }

        public double BezierCalc(double[] points, double time)
        {
            double result = 0;
            for (int i = 0; i < points.Length; i++)
            {
                result += PascalIndex(i, points.Length - 1) * Math.Pow(1 - time, points.Length - 1 - i) * Math.Pow(time, i) * points[i];
            }
            return result;
        }

        public int PascalIndex(int column, int row)
        {
            var key = new DualInt(column, row);
            if (column > row)
            {
                return -1;
            }
            if (cents.ContainsKey(key))
            {
                return cents[key];
            }
            var answer = row.Factorial() / (column.Factorial() * (row - column).Factorial());
            cents.Add(key, answer);
            return answer;            
        }
    }
}
