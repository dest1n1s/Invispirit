using UnityEngine;

namespace Assets.Scripts.Math
{
    class EMatrix2x2
    {
        public double[,] Element { get; set; }
        public EMatrix2x2()
        {
            Element = new double[2, 2];
        }
        public EMatrix2x2(double[,] element)
        {
            Element = element;
        }
        public EMatrix2x2(double[] element) : this(new double[2, 2] { { element[0], element[1] }, { element[2], element[3] } }) { }
        public static Vector2 operator *(EMatrix2x2 matrix, Vector2 vector)
        {
            Vector2 ret = new Vector2((float)(matrix.Element[0, 0] * vector.x + matrix.Element[0, 1] * vector.y),
                (float)(matrix.Element[1, 0] * vector.x + matrix.Element[1, 1] * vector.y));

            return ret;
        }
    }
}
