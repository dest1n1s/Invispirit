using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Config
{
    /// <summary>
    /// 已弃用
    /// </summary>
    class ConfigManager
    {
        protected static ConfigIni config;
        public static void Initialize()
        {
            config = ConfigIni.Instance;
        }
        public static double ReadSpeed(String key)
        {
            if (config == null) Debug.Log("Config is Null");
            return Double.Parse(config.ReadIniContent("speed", key));
        }
        public static double ReadTime(String key)
        {
            return Double.Parse(config.ReadIniContent("time", key));
        }
    }
}
