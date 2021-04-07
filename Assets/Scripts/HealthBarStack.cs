// <copyright file="HealthBarStack.cs" company="Invispirit">
// Copyright (c) Invispirit. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Assets.Scripts.Network;
using Mirror;
using UnityEngine;

/// <summary>
/// The stack of all players' health bars.
/// </summary>
public class HealthBarStack : NetworkBehaviour
{
    [SerializeField]
    private GameObject healthBarPrefab;
    [SerializeField]
    private PlayerManager playerManager;

    /// <summary>
    /// Gets the dictionary from player ID's to their health bars.
    /// </summary>
    public Dictionary<uint, HealthBar> HealthBarForPlayerId { get; } = new Dictionary<uint, HealthBar>();

    /// <summary>
    ///  Add the health bars of all players on starting a client.
    /// </summary>
    public override void OnStartClient()
    {
        foreach (var playerNetId in this.playerManager.idDictionary.Keys)
        {
            this.AddHealthBarForNetId(playerNetId);
        }
    }

    /// <summary>
    /// Adds the health bar for the player.
    /// </summary>
    /// <param name="player">the player whose health bar is to be added.</param>
    [Server]
    public void AddHealthBarForPlayer(GameObject player)
    {
        var playerNetId = player.GetComponent<NetworkIdentity>().netId;
        this.AddHealthBarForNetId(playerNetId);
        this.AddHealthBarForPlayerIdOnClient(playerNetId);
    }

    /// <summary>
    /// Removes the health bar for the player.
    /// </summary>
    /// <param name="player">the player whose health bar is to be added.</param>
    [Server]
    public void RemoveBarForPlayer(GameObject player)
    {
        var playerNetId = player.GetComponent<NetworkIdentity>().netId;
        this.RemoveBarForNetId(playerNetId);
        this.RemoveHealthBarForNetIdOnClient(playerNetId);
    }

    /// <summary>
    /// When on the server, update the health bars for all new players.
    /// </summary>
    private void Update()
    {
        if (!this.isClient)
        {
            return;
        }

        foreach (var playerNetId in this.playerManager.idDictionary.Keys)
        {
            this.AddHealthBarForNetId(playerNetId);
        }

        foreach (var playerHealthBar in this.HealthBarForPlayerId.Values)
        {
            playerHealthBar.Update();
        }
    }

    [ClientRpc]
    private void AddHealthBarForPlayerIdOnClient(uint playerNetId)
    {
        this.AddHealthBarForNetId(playerNetId);
    }

    [ClientRpc]
    private void RemoveHealthBarForNetIdOnClient(uint playerNetId)
    {
        this.RemoveBarForNetId(playerNetId);
    }

    /// <summary>
    /// Adds the health bar of the player with the given net ID.
    /// </summary>
    /// <para>
    /// If the net ID does not exist, nothing happens. This method will be called separately on the client and the
    /// server.
    /// </para>
    /// <param name="playerNetId">the net ID of the player.</param>
    private void AddHealthBarForNetId(uint playerNetId)
    {
        // Do nothing if the net ID cannot be found
        if (this.HealthBarForPlayerId.ContainsKey(playerNetId))
        {
            return;
        }

        var playerId = this.playerManager.idDictionary[playerNetId];
        var healthBarPosition = new Vector3(0, (-4 * playerId) + 4);
        var healthBar = new HealthBar(this.healthBarPrefab, $"Player{playerId}", this.transform, healthBarPosition);
        this.HealthBarForPlayerId.Add(playerNetId, healthBar);
    }

    /// <summary>
    /// Removes the health bar of the player with the given net ID. If the net ID does not exist, nothing happens.
    /// </summary>
    /// <para>
    /// If the net ID does not exist, nothing happens. This method will be called separately on the client and the
    /// server.
    /// </para>
    /// <param name="playerNetId">the net ID of the player.</param>
    private void RemoveBarForNetId(uint playerNetId)
    {
        // Do nothing if the net ID cannot be found
        if (!this.HealthBarForPlayerId.ContainsKey(playerNetId))
        {
            return;
        }

        HealthBar healthBar = this.HealthBarForPlayerId[playerNetId];
        Destroy(healthBar.BarObject);
        this.HealthBarForPlayerId.Remove(playerNetId);
    }
}