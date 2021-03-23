using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Network
{
    public class Player
    {
        public GameObject playerObject { get; set; }
        public uint id;
        public Player(GameObject playerObject,uint id)
        {
            this.playerObject = playerObject;
            this.id = id;
        }
    }
}
