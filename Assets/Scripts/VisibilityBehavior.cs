// <copyright file="VisibilityBehavior.cs" company="ECYSL">
//     Copyright (c) ECYSL. All rights reserved.
// </copyright>

using System.Collections;
using Mirror;
using UnityEngine;

/// <summary>
/// Manage the visibility of the gameobject(usually player) detached.
/// <para>When the gameobject collides other rigidbody, it will emerge.</para>
/// <para>You can invoke the 'CmdEmerge' method on clients to expose this object on server and all clients, or invoke the 'Emerge' method and 'RpcEmerge' method on server to expose this object on server and all clients.</para>
/// </summary>
public class VisibilityBehavior : NetworkBehaviour
{
    private bool show = false;
    private float currentTime = 0f;
    private new Renderer renderer;
    private Renderer[] childRenderer;

    /// <summary>
    /// Gets or sets the instance of VisibilityBehavior.
    /// </summary>
    public static VisibilityBehavior Instance { get; protected set; }

    /// <summary>
    /// Gets or sets 完全显形到开始隐形所需时间.
    /// </summary>
    [field: SerializeField]
    public float MaxTime { get; set; } = 3f;

    /// <summary>
    /// Gets or sets 从完全隐形开始显形到完全显形所需时间.
    /// </summary>
    [field: SerializeField]
    public float EmergeTime { get; set; } = 0.5f;

    /// <summary>
    /// Gets or sets 从完全显形开始隐形到完全隐形所需时间.
    /// </summary>
    [field: SerializeField]
    public float FadeTime { get; set; } = 1f;

    /// <summary>
    /// Call server to expose this object.
    /// </summary>
    [Command]
    public void CmdEmerge()
    {
        this.RpcEmerge();
        this.Emerge();
    }

    /// <summary>
    /// Call all clients to expose this object.
    /// </summary>
    [ClientRpc]
    public void RpcEmerge()
    {
        this.Emerge();
    }

    /// <summary>
    /// Emerge the gameobject this behavior detached.
    /// </summary>
    public void Emerge()
    {
        this.show = true;
        this.currentTime = 0f;
    }

    private void Awake()
    {
        Instance = this.GetComponent<VisibilityBehavior>();
        this.renderer = this.GetComponent<Renderer>();
        this.childRenderer = this.GetComponentsInChildren<Renderer>();

        foreach (Renderer r in this.childRenderer)
        {
            r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0);
        }

        this.StartCoroutine(this.CheckVisibility());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.isLocalPlayer)
        {
            this.CmdEmerge();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.isLocalPlayer)
        {
            this.CmdEmerge();
        }
    }

    private IEnumerator CheckVisibility()
    {
        while (true)
        {
            if (this.show && this.renderer.material.color.a == 1)
            {
                this.currentTime += Time.deltaTime;
                if (this.currentTime >= this.MaxTime)
                {
                    this.currentTime = 0;
                    this.show = false;
                }
            }

            if (this.show && this.renderer.material.color.a < 1)
            {
                this.renderer.material.color = new Color(this.renderer.material.color.r, this.renderer.material.color.g, this.renderer.material.color.b, this.renderer.material.color.a + (Time.deltaTime / this.EmergeTime) > 1 ? 1 : this.renderer.material.color.a + (Time.deltaTime / this.EmergeTime));
            }
            else if (!this.show && this.renderer.material.color.a > 0)
            {
                this.renderer.material.color = new Color(this.renderer.material.color.r, this.renderer.material.color.g, this.renderer.material.color.b, this.renderer.material.color.a - (Time.deltaTime / this.FadeTime) < 0 ? 0 : this.renderer.material.color.a - (Time.deltaTime / this.EmergeTime));
            }

            if (this.childRenderer != null)
            {
                foreach (Renderer r in this.childRenderer)
                {
                    r.material.color = new Color(r.material.color.a, r.material.color.g, r.material.color.b, this.renderer.material.color.a);
                }
            }

            yield return null;
        }
    }
}