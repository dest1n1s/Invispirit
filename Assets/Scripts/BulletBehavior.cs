// <copyright file="BulletBehavior.cs" company="ECYSL">
//     Copyright (c) ECYSL. All rights reserved.
// </copyright>

using Assets.Scripts;
using Assets.Scripts.Math;
using Mirror;
using UnityEngine;

/// <summary>
/// Control the velocity and damage of the bullet.
/// </summary>
public class BulletBehavior : NetworkBehaviour
{
    private new Rigidbody2D rigidbody;

    /// <summary>
    /// Gets or sets the speed of the bullet.
    /// </summary>
    public double Speed { get; set; } = 12;

    /// <summary>
    /// Gets or sets the damage which should be dealed by the bullet.
    /// </summary>
    public int Damage { get; set; } = 10;

    /// <summary>
    /// Gets or sets the shooter of the bullet.
    /// </summary>
    public GameObject Shooter { get; set; }

    /// <summary>
    /// Gets or sets the direction of the bullet.
    /// </summary>
    public Vector2 Direction { get; set; }

    private void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.rigidbody.velocity = default(Vector2);
    }

    private void Update()
    {
        if (this.isServer)
        {
            this.rigidbody.Move(this.Direction.GetRad(), this.Speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.isServer)
        {
            if (collider.gameObject.Equals(this.Shooter))
            {
                return;
            }

            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<HealthManager>().Hp -= this.Damage;
                collider.GetComponent<VisibilityBehavior>().Emerge();
                collider.GetComponent<VisibilityBehavior>().RpcEmerge();
            }

            Destroy(this.gameObject);
        }
    }
}
