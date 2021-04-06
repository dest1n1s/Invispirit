using UnityEngine;

namespace Assets.Scripts.Math
{
    static class VectorHandler
    {
        public static double GetRad(this Vector2 v)
        {
            return Mathf.Atan2(v.y, v.x);
        }
        public static Vector2 Rotate(this Vector2 vector, double rad)
        {
            EMatrix2x2 matrix = new EMatrix2x2(new double[4] { System.Math.Cos(rad), -System.Math.Sin(rad), System.Math.Sin(rad), System.Math.Cos(rad) });
            Vector2 ret = matrix * vector;
            //Debug.Log("ret:" + ret.ToString()); ;
            return ret;
        }
    }
}
