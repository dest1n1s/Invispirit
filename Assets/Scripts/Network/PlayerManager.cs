using Mirror;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Network
{
    public class PlayerManager : NetworkBehaviour
    {
        public Dictionary<uint, uint> idDictionary = new Dictionary<uint, uint>();
        public static readonly uint MAXID = 100;
        public static Vector2 GetSpawnPosition()
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            return new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));
        }

        [TargetRpc]
        public void TargetSendIdDictionary(NetworkConnection conn,uint[] netid,uint[] id)
        {
            for(int i = 0; i < netid.Length; i++)
            {
                if (idDictionary.ContainsKey(netid[i])) continue;
                idDictionary.Add(netid[i], id[i]);
            }
        }

        [Server]
        public uint AddPlayer(GameObject gameObject)
        {
            uint id;
            for (id = 1; id <= MAXID; id++) if (!idDictionary.ContainsValue(id)) break;
            idDictionary.Add(gameObject.GetComponent<NetworkIdentity>().netId, id);
            RpcAddPlayer(id, gameObject.GetComponent<NetworkIdentity>().netId);
            return id;
        }

        [Server]
        public void RemovePlayer(GameObject gameObject)
        {
            if (!idDictionary.ContainsKey(gameObject.GetComponent<NetworkIdentity>().netId)) return;
            else
            {
                idDictionary.Remove(gameObject.GetComponent<NetworkIdentity>().netId);
                RpcRemovePlayer(gameObject.GetComponent<NetworkIdentity>().netId);
            }
        }

        [ClientRpc]
        public void RpcAddPlayer(uint id, uint netid)
        {

            if (idDictionary.ContainsKey(netid)) return;
            idDictionary.Add(netid, id);
        }

        [ClientRpc]
        public void RpcRemovePlayer(uint netid) 
        {
            if (!idDictionary.ContainsKey(netid)) return;
            idDictionary.Remove(netid);
        }
        
        public uint[] GetKeyArray()
        {
            uint[] arr = new uint[idDictionary.Count];
            int i = 0;
            foreach (uint netid in idDictionary.Keys) arr[i++] = netid;
            return arr;
        }

        public uint[] GetValueArray()
        {
            uint[] arr = new uint[idDictionary.Count];
            int i = 0;
            foreach (uint id in idDictionary.Values) arr[i++] = id;
            return arr;
        }
    }
}
