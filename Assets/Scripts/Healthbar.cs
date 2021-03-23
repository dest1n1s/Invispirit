using Assets.Scripts;
using Assets.Scripts.Network;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : NetworkBehaviour
{
    public GameObject HealthBar;
    public PlayerManager playerManager;
    public Dictionary<uint, Bar> barDictionary = new Dictionary<uint, Bar>();

    public override void OnStartClient()
    {
        foreach(uint netid in playerManager.idDictionary.Keys)
        {
            FuncAddBar(netid);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(isClient)
        {
            foreach (uint netid in playerManager.idDictionary.Keys)
            {
                FuncAddBar(netid);
            }
            foreach (Bar bar in barDictionary.Values)
            {
                bar.Update();
            }
        }
            
    }

    [Server]
    public void AddBar(GameObject player)
    {
        uint netid = player.GetComponent<NetworkIdentity>().netId;
        FuncAddBar(netid);
        RpcAddBar(netid);
    }

    [Server]
    public void RemoveBar(GameObject player)
    {
        uint netid = player.GetComponent<NetworkIdentity>().netId;
        FuncRemoveBar(netid);
        RpcRemoveBar(netid);
    }

    [ClientRpc]
    public void RpcAddBar(uint netid)
    {
        FuncAddBar(netid);
    }

    [ClientRpc]
    public void RpcRemoveBar(uint netid)
    {
        FuncRemoveBar(netid);
    }

    public void FuncAddBar(uint netid)
    {
        if (barDictionary.ContainsKey(netid)) return;
        GameObject healthbar = Instantiate(HealthBar);
        healthbar.transform.SetParent(transform);
        int id = (int)playerManager.idDictionary[netid];
        healthbar.transform.localPosition = new Vector3(0, -4 * id + 4);
        Bar bar = new Bar(healthbar, $"Player{id}");
        barDictionary.Add(netid, bar);
    }

    public void FuncRemoveBar(uint netid)
    {
        if (!barDictionary.ContainsKey(netid)) return;
        Bar bar = barDictionary[netid];
        Destroy(bar.barObject);
        barDictionary.Remove(netid);
    }
}
