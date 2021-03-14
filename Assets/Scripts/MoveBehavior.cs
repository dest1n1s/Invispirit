using Assets.Scripts;
using Assets.Scripts.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    TransformManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = new HeroTransformManager(GetComponent<Rigidbody2D>());
    }

    // Update is called once per frame
    void Update()
    {
        EComplex e=new EComplex();
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
        if (e == 0)
        {
            manager.Stop();
        }
        else
        {
            manager.Move(e.GetAngle());
        }
    }

    EComplex GetAngle(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.D: return EComplex.GetComplex(1, 0);
            case KeyCode.W: return EComplex.GetComplex(0, 1);
            case KeyCode.A: return EComplex.GetComplex(-1, 0);
            case KeyCode.S: return EComplex.GetComplex(0, -1);
            default:return EComplex.GetComplex(0, 0);
        }
    }
}
