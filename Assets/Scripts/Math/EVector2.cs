using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Math
{
    class EVector2
    {
        public double X { get; set; }
        public double Y { get; set; }
        public EVector2()
        {
            this.X = 0;
            this.Y = 0;
        }
        public EVector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public static implicit operator Vector2(EVector2 vector)
        {
            return new Vector2((float)vector.X, (float)vector.Y);
        }
        public static implicit operator EVector2(Vector2 vector)
        {
            return new EVector2(vector.x, vector.y);
        }
        public static EVector2 Rotate(EVector2 vector, double rad)
        {
            EMatrix2x2 matrix = new EMatrix2x2(new double[4] { System.Math.Cos(rad), -System.Math.Sin(rad), System.Math.Sin(rad), System.Math.Cos(rad) });
            return matrix * vector;
        }
        public void Rotate(double rad)
        {
            EVector2 e = EVector2.Rotate(this, rad);
            this.X = e.X;
            this.Y = e.Y;
        }
    }
}
