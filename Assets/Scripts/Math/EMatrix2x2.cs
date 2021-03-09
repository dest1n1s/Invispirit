using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static EVector2 operator *(EMatrix2x2 matrix,EVector2 vector)
        {
            return new EVector2(matrix.Element[0, 0] * vector.X + matrix.Element[0, 1] * vector.Y,
                matrix.Element[1, 0] * vector.X + matrix.Element[1, 1] * vector.Y);
        }
    }
}
