// <copyright file="GunBehavior.cs" company="ECYSL">
//     Copyright (c) ECYSL. All rights reserved.
// </copyright>

using Mirror;
using UnityEngine;

/// <summary>
/// Control the gun of the player.
/// </summary>
public class GunBehavior : NetworkBehaviour
{
    private Vector3 originalScale;
    private Vector2 gunDirection;

    /// <summary>
    /// Gets or sets the first gun of the player.
    /// </summary>
    [field:SerializeField]
    public GameObject Gun { get; set; }

    /// <summary>
    /// Gets or sets the rotation axis of gun1.
    /// </summary>
    [field: SerializeField]
    public GameObject Axis { get; set; }

    /// <summary>
    /// Gets or sets the bullet prefab.
    /// </summary>
    [field: SerializeField]
    public GameObject Bullet { get; set; }

    /// <summary>
    /// Gets or sets the muzzle.
    /// </summary>
    [field: SerializeField]
    public GameObject Muzzle { get; set; }

    /// <summary>
    /// Called when server starts.
    /// </summary>
    public override void OnStartServer()
    {
        this.originalScale = this.transform.localScale;
    }

    /// <summary>
    /// Called when client starts.
    /// </summary>
    public override void OnStartClient()
    {
        this.originalScale = this.transform.localScale;
    }

    private void Update()
    {
        if (!this.isLocalPlayer)
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        this.gunDirection = (mousePos - this.Muzzle.transform.position).normalized;

        if (this.gunDirection.sqrMagnitude == 0)
        {
            this.gunDirection = new Vector2(0, 1);
        }

        float angle = Mathf.Atan2(this.gunDirection.y, this.gunDirection.x) * Mathf.Rad2Deg;

        if (mousePos.x - this.transform.position.x > 0)
        {
            this.transform.localScale = new Vector3(-this.originalScale.x, this.originalScale.y, this.originalScale.z);
            this.Gun.transform.RotateAround(this.Axis.transform.position, new Vector3(0, 0, 1), angle - this.Gun.transform.rotation.eulerAngles.z);
        }
        else
        {
            this.transform.localScale = new Vector3(this.originalScale.x, this.originalScale.y, this.originalScale.z);
            this.Gun.transform.RotateAround(this.Axis.transform.position, new Vector3(0, 0, 1), (Mathf.PI * Mathf.Rad2Deg) + angle - this.Gun.transform.rotation.eulerAngles.z);
        }

        this.CmdUpdateTransform(this.transform.position, this.transform.rotation, this.transform.localScale);

        if (Input.GetMouseButtonDown(0))
        {
            this.GetComponent<VisibilityBehavior>().CmdEmerge();
            this.CmdFire(mousePos, this.gameObject);
        }
    }

    [Command]
    private void CmdFire(Vector3 mousePos, GameObject gameObject)
    {
        this.gunDirection = (mousePos - this.Muzzle.transform.position).normalized;
        if (this.gunDirection.sqrMagnitude == 0)
        {
            this.gunDirection = new Vector2(0, 1);
        }

        GameObject newBullet = Instantiate(this.Bullet, this.Muzzle.transform.position, Quaternion.Euler(this.transform.eulerAngles));
        newBullet.GetComponent<BulletBehavior>().Direction = this.gunDirection;
        newBullet.GetComponent<BulletBehavior>().Shooter = this.gameObject;
        NetworkServer.Spawn(newBullet, gameObject);
    }

    [Command]
    private void CmdUpdateTransform(Vector3 position, Quaternion rotation, Vector3 localscale)
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        this.transform.localScale = localscale;
    }
}
