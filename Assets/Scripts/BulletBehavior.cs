using Assets.Scripts;
using Assets.Scripts.Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    //public float Speed;
    public int Damage;
    public float DestroyDistance;

    private Rigidbody2D rb2d;
    private Vector3 startPos;
    private TransformManager manager;
    public Vector2 Direction { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2();
        startPos = transform.position;
        manager = new BulletTransformManager(rb2d);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Direction);
        manager.Move(Direction.GetRad());
        float distance = (transform.position - startPos).magnitude;
        if (distance > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
