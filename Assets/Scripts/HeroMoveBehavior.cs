using Assets.Scripts;
using Assets.Scripts.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveBehavior : MonoBehaviour
{
    TransformManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = new HeroTransformManager(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            manager.Stop();
        }
        else
        {
            manager.Move(e.GetRad());
        }
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
