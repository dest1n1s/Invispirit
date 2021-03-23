using Assets.Scripts;
using Assets.Scripts.Network;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : NetworkBehaviour
{
    public Healthbar barManager;
    
    [SyncVar]
    public int Hp;
    
    public override void OnStartClient()
    {
        barManager = GameObject.Find("Canvas").GetComponent<Healthbar>();
    }

    public override void OnStartServer()
    {
        barManager = GameObject.Find("Canvas").GetComponent<Healthbar>();
    }

    public override void OnStopClient()
    {
        
    }

    void Update()
    {
        //if (isServer) Debug.Log("Server:" + Hp);
        //if (isClient) Debug.Log("Client:" + Hp);
        //if (isLocalPlayer) Debug.Log("LocalPlayer:" + Hp);
        barManager.barDictionary[netId].hp = Hp;
    }
}