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
        double timeMultiplier;
        double time;
        double[] points;
        public double Location { get; set; }

        public Bezier(double timeMulti, double[] points, double location = 0)
        {
            Location = location;
            this.time = 0;
            timeMultiplier = timeMulti;
            this.points = points;
        }

        public void Update(GameTime gameTime)
        {
            if (time >= 1)
            {
                return;
            }
            time += (gameTime.ElapsedGameTime.TotalMilliseconds) / timeMultiplier / 1000;
            Location = BezierFuncs.Get().BezierCalc(points, time);
        }

        public void Update(double thyme)
        {    
            time = thyme;
            if (thyme >= 1)
            {
                time = 1;
            }
            Location = BezierFuncs.Get().BezierCalc(points, time);
        }
    }


    class BezierFuncs
    {
        static BezierFuncs Instance;
        Dictionary<DualInt, int> cents;
        public static BezierFuncs Get()
        {
            if (Instance == null)
            {
                Instance = new BezierFuncs();
            }
            return Instance;
        }
        BezierFuncs()
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
            var rowFact = row.Factorial();
            var colFact = column.Factorial();
            var bothFact = (row - column).Factorial();

            var answer = rowFact / (colFact * bothFact);
            cents.Add(key, answer);
            return answer;            
        }
    }
}
