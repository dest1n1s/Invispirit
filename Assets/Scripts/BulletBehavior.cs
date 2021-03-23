using Assets.Scripts;
using Assets.Scripts.Math;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : NetworkBehaviour
{
    [SyncVar]
    private double speed = 0;
    public int Damage;
    public float DestroyDistance;
    public GameObject Shooter { get; set; }
    private Rigidbody2D rigidbody;
    public Vector2 Direction { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        if (isServer)
            speed = NetworkConfig.Instance.ReadSpeed("bullet1");
        Damage = 20;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if(isServer)
            rigidbody.Move(Direction.GetRad(), speed);
    }   
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(isServer)
        {
            if (collider.gameObject.Equals(Shooter)) return;
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<HealthManager>().Hp -= Damage;
                collider.GetComponent<VisibilityChangeBehavior>().FuncEmerge();
            }
            Destroy(gameObject);
        }
    }
}
