using Assets.Scripts;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityChangeBehavior : NetworkBehaviour
{
    private static VisibilityChangeBehavior _instance;
    public static VisibilityChangeBehavior Instance
    {
        get
        {
            return _instance;
        }
    }
    private VisibilityManager manager;
    void Awake()
    {
        _instance = GetComponent<VisibilityChangeBehavior>();
    }
    public override void OnStartServer()
    {
        manager = VisibilityManager.GetInstance(GetComponent<Renderer>());
    }
    public override void OnStartClient()
    {
        manager = VisibilityManager.GetInstance(GetComponent<Renderer>());
    }
    void OnCollisionEnter2D(Collision2D OtherObj)
    {
        if (isLocalPlayer) CmdEmerge();
    }
    void OnCollisionStay2D(Collision2D OtherObj)
    {
        if (isLocalPlayer) CmdEmerge();
    }
    [Command(requiresAuthority = false)]
    public void CmdEmerge()
    {
        manager.Emerge();
        RpcEmerge();
    }
    [ClientRpc]
    public void RpcEmerge()
    {
        manager.Emerge();
    }
    public void FuncEmerge()
    {
        manager.Emerge();
        RpcEmerge();
    }
}
