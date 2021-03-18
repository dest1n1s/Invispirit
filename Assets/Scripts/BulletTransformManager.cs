using Assets.Scripts.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class BulletTransformManager : TransformManager
    {
        public BulletTransformManager(Rigidbody2D rigidbody) : base(rigidbody) { }
        protected override void GetSpeed()
        {
            speed = Double.Parse(ConfigManager.ReadSpeed("bullet1"));
        }
    }
}
