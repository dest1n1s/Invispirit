using UnityEngine;
using Mirror;
using System.Collections.Generic;
using Assets.Scripts.Config;
using System;

/*
	Documentation: https://mirror-networking.com/docs/Guides/NetworkBehaviour.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class NetworkConfig : NetworkBehaviour
{
    private ConfigIni config;
    private SyncDictionary<String, String> configDictionary = new SyncDictionary<string, string>();
    private static readonly String[] keys = { "speed|hero1", "speed|bullet1", "time|keepVisible", "time|emerge", "time|fade" };
    private static readonly String[] values = { "7", "12", "5", "0.5", "1" };
    private static NetworkConfig _instance;
    public static NetworkConfig Instance {
        get
        {
            return _instance;
        }
    }
    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() {
        //config = ConfigIni.Instance;
        int i = 0;
        foreach(String key in keys)
        {
            String[] pre = key.Split('|');
            configDictionary.Add(key, values[i++]);
        }
        _instance = this;
    }
    public override void OnStartClient()
    {
        _instance = this;
    }

    public double ReadSpeed(String key)
    {
        return Double.Parse(configDictionary["speed|" + key]);
    }
    public double ReadTime(String key)
    {
        return Double.Parse(configDictionary["time|" + key]);
    }
}
