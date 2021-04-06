using Assets.Scripts.Math;
using UnityEngine;

namespace Assets.Scripts
{
    //enum Direction
    //{
    //    Up,Down,Left,Right,UpLeft,DownLeft,UpRight,DownRight,Any
    //}
    /// <summary>
    /// 管理刚体移动类
    /// </summary>
    static class TransformManager
    {
        public static void Move(this Rigidbody2D rigidbody, double direction, double speed)
        {
            Vector2 e = new Vector2((float)speed, 0).Rotate(direction);
            //rigidbody.AddForce(e * Time.deltaTime);
            rigidbody.velocity = e;
        }

        //public static void Move(this Transform transform, double direction, double speed)
        //{
        //    Vector3 e = new Vector2((float)speed, 0).Rotate(direction);
        //    //rigidbody.velocity = e.Rotate(direction);
        //    transform.localPosition = transform.localPosition + e * Time.deltaTime * 10;
        //}
    }
}
