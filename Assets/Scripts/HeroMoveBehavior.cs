using Assets.Scripts;
using Assets.Scripts.Math;
using Mirror;
using UnityEngine;

/// <summary>
/// Control the movement of hero.
/// </summary>
public class HeroMoveBehavior : NetworkBehaviour
{
    private new Animator animator;

    /// <summary>
    /// Gets or sets the run animation of hero.
    /// </summary>
    private new Rigidbody2D rigidbody;

    /// <summary>
    /// Gets or sets the speed of the hero.
    /// </summary>
    public double Speed { get; set; } = 7;

    /// <summary>
    /// Called when starting server.
    /// </summary>
    public override void OnStartServer()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.rigidbody.freezeRotation = true;
        this.animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Called when starting client.
    /// </summary>
    public override void OnStartClient()
    {
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.rigidbody.freezeRotation = true;
        this.animator = this.GetComponent<Animator>();
    }

    /// <summary>
    /// Get the rad of rotation from the key.
    /// </summary>
    /// <param name="key">the key down</param>
    /// <returns>the rad of rotation</returns>
    public Vector2 GetRad(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.D: return new Vector2(1, 0);
            case KeyCode.W: return new Vector2(0, 1);
            case KeyCode.A: return new Vector2(-1, 0);
            case KeyCode.S: return new Vector2(0, -1);
            default: return new Vector2(0, 0);
        }
    }

    private void Update()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0)
        {
            this.animator.SetBool("run", true);
        }
        else if (this.GetComponent<Rigidbody2D>().velocity.sqrMagnitude == 0)
        {
            this.animator.SetBool("run", false);
        }

        if (!this.isLocalPlayer)
        {
            return;
        }

        Vector2 e = default(Vector2);
        if (Input.GetKey(KeyCode.A))
        {
            e += this.GetRad(KeyCode.A);
        }

        if (Input.GetKey(KeyCode.W))
        {
            e += this.GetRad(KeyCode.W);
        }

        if (Input.GetKey(KeyCode.S))
        {
            e += this.GetRad(KeyCode.S);
        }

        if (Input.GetKey(KeyCode.D))
        {
            e += this.GetRad(KeyCode.D);
        }

        if (e.sqrMagnitude == 0)
        {
            this.CmdMove(e.GetRad(), 0);
        }
        else
        {
            this.CmdMove(e.GetRad(), this.Speed);
        }
    }

    [Command]
    private void CmdMove(double rad, double speed)
    {
        this.rigidbody.Move(rad, speed);
        this.RpcMove(rad, speed);
    }

    [ClientRpc]
    private void RpcMove(double rad, double speed)
    {
        this.rigidbody.Move(rad, speed);
    }
}
