using Assets.Scripts;
using Assets.Scripts.Math;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveBehavior : NetworkBehaviour
{
    [SyncVar]
    private double speed;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        speed = NetworkConfig.Instance.ReadSpeed("hero1");
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;
    }
    public override void OnStartClient()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        Vector2 e=new Vector2();
        if (Input.GetKey(KeyCode.A))
        {
            e += GetAngle(KeyCode.A);
        }
        if (Input.GetKey(KeyCode.W))
        {
            e += GetAngle(KeyCode.W);
        }
        if (Input.GetKey(KeyCode.S))
        {
            e += GetAngle(KeyCode.S);
        }
        if (Input.GetKey(KeyCode.D))
        {
            e += GetAngle(KeyCode.D);
        }
        if (e.sqrMagnitude==0)
        {
            CmdMove(e.GetRad(), 0);
        }
        else
        {
            CmdMove(e.GetRad(), speed);
            
        }
    }

    [Command]
    public void CmdMove(double rad, double speed)
    {
        rigidbody.Move(rad, speed);
        RpcMove(rad, speed);
    }
    [ClientRpc]
    public void RpcMove(double rad, double speed)
    {
        rigidbody.Move(rad, speed);
        //Debug.Log($"{rad},{speed},{rigidbody.velocity}");
    }
    Vector2 GetAngle(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.D: return new Vector2(1, 0);
            case KeyCode.W: return new Vector2(0, 1);
            case KeyCode.A: return new Vector2(-1, 0);
            case KeyCode.S: return new Vector2(0, -1);
            default:return new Vector2(0, 0);
        }
    }
}
