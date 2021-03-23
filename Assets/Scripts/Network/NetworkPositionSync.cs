using UnityEngine;
using Mirror;
using System.Collections.Generic;

/*
	Documentation: https://mirror-networking.com/docs/Guides/NetworkBehaviour.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class NetworkPositionSync : NetworkBehaviour
{
    public float SyncTime = 0.1f;
    private Rigidbody2D rigidbody;
    public override void OnStartServer()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public override void OnStartClient()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    [ServerCallback]
    private void LateUpdate()
    {
        RpcSyncPosition(rigidbody.position);
        RpcSyncRotation(rigidbody.rotation);
    }

    [ClientRpc]
    public void RpcSyncPosition(Vector2 position)
    {
        rigidbody.velocity += (position - rigidbody.position) / SyncTime;
    }
    [ClientRpc]
    public void RpcSyncRotation(float rotation)
    {
        rigidbody.rotation = rotation;
    }
}
