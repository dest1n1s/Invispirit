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
        

        protected override void GetSpeed()
        {
            speed = Double.Parse(ConfigManager.ReadSpeed("hero1"));
        }
    }
}
