// <copyright file="HealthBarStack.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Mirror;
using Network;
using UnityEngine;

/// <summary>
/// The stack of all players' health bars.
/// </summary>
public class HealthBarStack : NetworkBehaviour
{
    [SerializeField]
    private GameObject healthBarPrefab;
    [SerializeField]
    private PlayerList playerList;

    /// <summary>
    /// Gets the dictionary from player ID's to their health bars.
    /// </summary>
    public Dictionary<uint, HealthBar> HealthBarForNetId { get; } = new Dictionary<uint, HealthBar>();

    /// <summary>
    ///  Adds the health bars of all players on starting a client.
    /// </summary>
    public override void OnStartClient()
    {
        foreach (var playerNetId in this.playerList.NetIds())
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
        this.AddHealthBarForNetId(playerNetId); // add on the server
        this.AddHealthBarForNetIdOnClient(playerNetId);
    }

    /// <summary>
    /// Removes the health bar for the player.
    /// </summary>
    /// <param name="player">the player whose health bar is to be added.</param>
    [Server]
    public void RemoveHealthBarForPlayer(GameObject player)
    {
        var playerNetId = player.GetComponent<NetworkIdentity>().netId;
        this.RemoveHealthBarForNetId(playerNetId); // add on the server
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

        // Add the health bars for new players. Those who already exist will be skipped.
        foreach (var playerNetId in this.playerList.NetIds())
        {
            this.AddHealthBarForNetId(playerNetId);
        }

        foreach (var playerHealthBar in this.HealthBarForNetId.Values)
        {
            playerHealthBar.Update();
        }
    }

    [ClientRpc]
    private void AddHealthBarForNetIdOnClient(uint playerNetId)
    {
        this.AddHealthBarForNetId(playerNetId);
    }

    [ClientRpc]
    private void RemoveHealthBarForNetIdOnClient(uint playerNetId)
    {
        this.RemoveHealthBarForNetId(playerNetId);
    }

    /// <summary>
    /// Adds the health bar of the player with the given net ID.
    /// </summary>
    /// <para>
    /// If the net ID does not exist or the health bar is already added, nothing happens. This method will be called
    /// separately on the client and the server.
    /// </para>
    /// <param name="playerNetId">the net ID of the player.</param>
    private void AddHealthBarForNetId(uint playerNetId)
    {
        // Do nothing if the net ID cannot be found.
        // This additional check is necessary because `playerList`s on the server and client might not be synced yet.
        if (!this.playerList.ContainsNetId(playerNetId))
        {
            return;
        }

        // Do nothing if the health bar is already added
        if (this.HealthBarForNetId.ContainsKey(playerNetId))
        {
            return;
        }

        var playerId = this.playerList.PlayerIdForNetId(playerNetId);
        var healthBarPosition = new Vector3(0, (-4 * playerId) + 4);
        var healthBar = new HealthBar(this.healthBarPrefab, $"Player{playerId}", this.transform, healthBarPosition);
        this.HealthBarForNetId.Add(playerNetId, healthBar);
    }

    /// <summary>
    /// Removes the health bar of the player with the given net ID. If the net ID does not exist, nothing happens.
    /// </summary>
    /// <para>
    /// If the net ID does not exist, nothing happens. This method will be called separately on the client and the
    /// server.
    /// </para>
    /// <param name="playerNetId">the net ID of the player.</param>
    private void RemoveHealthBarForNetId(uint playerNetId)
    {
        // Do nothing if the net ID cannot be found
        if (!this.HealthBarForNetId.ContainsKey(playerNetId))
        {
            return;
        }

        var healthBar = this.HealthBarForNetId[playerNetId];
        Destroy(healthBar.BarObject);
        this.HealthBarForNetId.Remove(playerNetId);
    }
}