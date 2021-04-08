// <copyright file="HealthManager.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

using Mirror;
using UnityEngine;

/// <summary>
/// Manages the health of a player and updates the player's health bar.
/// </summary>
public class HealthManager : NetworkBehaviour
{
    private HealthBarStack healthBarStackStack;

    /// <summary>
    /// Gets or sets the HP of the player.
    /// </summary>
    [field: SyncVar]
    [field: SerializeField]
    public int Hp { get; set; }

    /// <summary>
    /// Find the <see cref="HealthBarStack"/> object on starting the client.
    /// </summary>
    public override void OnStartClient()
    {
        this.FindBarManager();
    }

    /// <summary>
    /// Find the <see cref="HealthBarStack"/> object on starting the server.
    /// </summary>
    public override void OnStartServer()
    {
        this.FindBarManager();
    }

    /// <summary>
    /// Update the visual effect of the player's health bar.
    /// </summary>
    private void Update()
    {
        this.healthBarStackStack.HealthBarForNetId[this.netId].Hp = this.Hp;
    }

    private void FindBarManager()
    {
        this.healthBarStackStack = GameObject.Find("Canvas").GetComponent<HealthBarStack>();
    }
}