using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Config
{
    class ConfigManager
    {
        protected static ConfigIni config;
        public static void Initialize()
        {
            config = ConfigIni.Instance;
        }
        public static String ReadSpeed(String key)
        {
            return config.ReadIniContent("speed", key);
        }
    }
}
