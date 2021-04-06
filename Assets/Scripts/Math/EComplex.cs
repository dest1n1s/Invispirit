using System;

namespace Assets.Scripts.Math
{
    class EComplex
    {
        private double real;//实部
        private double image;//虚部
        /// <summary>
        /// 获取或设置实部
        /// </summary>
        public double Real
        {
            get { return real; }
            set { real = value; }
        }
        /// <summary>
        /// 获取或者设置虚部
        /// </summary>
        public double Image
        {
            get { return image; }
            set { image = value; }
        }
        public EComplex(double real, double image)
        {
            this.real = real;
            this.image = image;
        }
        public EComplex(double real)
        {
            this.real = real;
            this.image = 0;
        }
        public EComplex() : this(0) { }
        /// <summary>
        /// 取共轭复数
        /// </summary>
        public EComplex Conjugate()
        {
            return new EComplex(this.real, -this.image);
        }
        public static EComplex operator +(EComplex C, EComplex c)
        {
            return new EComplex(c.real + C.real, C.image + c.image);
        }
        public static EComplex operator +(double d, EComplex c)
        {
            return new EComplex(d) + c;
        }
        public static EComplex operator +(EComplex c, double d)
        {
            return new EComplex(d) + c;
        }
        public static EComplex operator -(EComplex C, EComplex c)
        {
            return new EComplex(C.real - c.real, C.image - c.Image);
        }
        public static EComplex operator -(double d, EComplex c)
        {
            return new EComplex(d) - c;
        }
        public static EComplex operator -(EComplex c, double d)
        {
            return c - new EComplex(d);
        }
        public static bool operator ==(EComplex C, EComplex c)
        {
            return (C.real == c.real && C.image == c.image);
        }
        public static bool operator ==(double d, EComplex c)
        {
            return (new EComplex(d)) == c;
        }
        public static bool operator ==(EComplex c, double d)
        {
            return c == new EComplex(d);
        }
        public static bool operator !=(EComplex C, EComplex c)
        {
            return (C.real != c.real || C.image != c.image);
        }
        public static bool operator !=(double d, EComplex c)
        {
            return new EComplex(d) != c;
        }
        public static bool operator !=(EComplex c, double d)
        {
            return c != new EComplex(d);
        }
        public static EComplex operator *(EComplex c, EComplex C)
        {
            //(a+b*i)*(c+d*i)=(ac-bd)+(ad+bc)*i
            return new EComplex(c.real * C.real - c.image * C.image, c.real * C.image + c.image * C.real);
        }
        public static EComplex operator *(double d, EComplex c)
        {
            return new EComplex(d) * c;
        }
        public static EComplex operator *(EComplex c, double d)
        {
            return c * new EComplex(d);
        }
        public static EComplex operator /(EComplex C, EComplex c)
        {
            if (c.real == 0 && c.image == 0)
            {
                throw new Exception("除数的虚部和实部不能同时为零(除数不能为零)");
            }
            double real = (C.real * c.real + c.image * C.image) / (c.real * c.real + c.image + c.image);
            double image = (C.image * c.real - c.image * C.real) / (c.real * c.real + c.image + c.image);
            return new EComplex(real, image);
        }
        public static EComplex operator /(double d, EComplex c)
        {
            return new EComplex(d) / c;
        }
        public static EComplex operator /(EComplex c, double d)
        {
            return c / new EComplex(d);
        }
        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public double Mod()
        {
            return System.Math.Sqrt(this.real * this.real + this.image * this.image);
        }
        /// <summary>
        /// 判断复数是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is EComplex)
            {
                EComplex com = (EComplex)obj;
                return (com.real == this.real && com.image == this.image);
            }
            if (obj is double || obj is int || obj is float)
            {
                EComplex com = new EComplex((double)obj);
                return com.real == this.real;
            }
            return false;
        }
        /// <summary>
        /// 计算复数相位角
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static double GetAngle(EComplex c)
        {
            return c.GetAngle();
        }
        public double GetAngle()
        {
            return System.Math.Atan2(this.image, this.real);
        }
        public override string ToString()
        {
            return string.Format("{0}+{1}i", this.real, this.image);
        }
        public static EComplex GetComplex(double real)
        {
            return new EComplex(real);
        }
        public static EComplex GetComplex(double real, double image)
        {
            return new EComplex(real, image);
        }

        public override int GetHashCode()
        {
            return real.GetHashCode() ^ image.GetHashCode();
        }
    }
}
