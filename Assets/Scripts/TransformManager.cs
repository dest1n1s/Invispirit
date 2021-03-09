using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    //enum Direction
    //{
    //    Up,Down,Left,Right,UpLeft,DownLeft,UpRight,DownRight,Any
    //}
    abstract class TransformManager
    {
        //protected static double speed;
        //public static double Speed
        //{
        //    get { return speed; }
        //    set { speed = value; }
        //}
        protected Rigidbody2D rigidbody;
        protected double speed;
        public TransformManager(Rigidbody2D rigidbody)
        {
            this.rigidbody = rigidbody;
            GetSpeed();
        }

        abstract public void Move(double direction);
        public void Stop()
        {
            rigidbody.velocity = new Vector2();
        }
        abstract protected void GetSpeed();
    }
}
