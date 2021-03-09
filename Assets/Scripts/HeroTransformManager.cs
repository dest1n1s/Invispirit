using Assets.Scripts.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Config;

namespace Assets.Scripts
{
    class HeroTransformManager : TransformManager
    {
        public HeroTransformManager(Rigidbody2D rigidbody) : base(rigidbody)
        {
            
        }
        public override void Move(double direction)
        {
            EVector2 e = new EVector2(speed, 0);
            e.Rotate(direction);
            rigidbody.velocity = e;
        }

        protected override void GetSpeed()
        {
            speed = Double.Parse(ConfigManager.ReadSpeed("hero1"));
        }
    }
}
