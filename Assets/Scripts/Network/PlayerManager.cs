// <copyright file="PlayerManager.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using System.Linq;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// Manages the connection and disconnection of clients and updates the list of players and the health bar stack
    /// according to it.
    /// </summary>
    public class PlayerManager : NetworkManager
    {
        private const uint MaxNumberOfPlayers = 100;

        [SerializeField]
        private PlayerList playerList;

        [SerializeField]
        private HealthBarStack healthBarStack;

        private SyncDictionary<uint, uint> PlayerIdForNetId => this.playerList.PlayerIdForNetId;

        /// <summary>
        /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // The player will spawn at on a random position.
            var randomSpawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));

            // Instantiate the player object
            var player = Instantiate(this.playerPrefab, randomSpawnPosition, default);

            // Set a unique name for the object for debugging
            player.name = $"Player [connId={conn.connectionId}]";

            // Add the player to the network server
            NetworkServer.AddPlayerForConnection(conn, player);

            // Set the player's player ID as the least possible unique int from 1 to MaxNumberOfPlayers.
            uint playerId;
            for (playerId = 1; playerId <= MaxNumberOfPlayers; playerId++)
            {
                if (!this.PlayerIdForNetId.Values.Contains(playerId))
                {
                    break;
                }
            }

            this.PlayerIdForNetId.Add(player.GetComponent<NetworkIdentity>().netId, playerId);

            // Add the health bar of the player
            // this.healthBarStack = GameObject.Find("Canvas").GetComponent<HealthBarStack>();
            this.healthBarStack.AddHealthBarForPlayer(player);
        }

        /// <summary>
        /// Removes the player from the player list and removes the player's health bar when a client disconnects.
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            var playerObject = conn.identity.gameObject;
            var netId = playerObject.GetComponent<NetworkIdentity>().netId;

            // Removes the player from the player list
            if (this.PlayerIdForNetId.ContainsKey(netId))
            {
                this.PlayerIdForNetId.Remove(netId);
            }

            // Removes the health bar of the player
            this.healthBarStack.RemoveBarForPlayer(playerObject);

            // Calling the base implementation is necessary
            base.OnServerDisconnect(conn);
        }
    }
}