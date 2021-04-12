// <copyright file="CustomNetworkManager.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// Manages the connection and disconnection of clients and updates the list of players and the health bar stack
    /// according to it.
    /// <para>
    /// A player will have two ID's. The player ID starts from 1 and is displayed in the UI. The net ID is the ID
    /// assigned by <see cref="Mirror"/>.
    /// </para>
    /// </summary>
    public class CustomNetworkManager : NetworkManager
    {
        [SerializeField]
        private PlayerList playerList;
        [SerializeField]
        private HealthBarStack healthBarStack;

        private void Update()
        {
            if (NetworkClient.isConnected)
            {
                this.playerList = GameObject.Find("PlayerList").GetComponent<PlayerList>();
                this.healthBarStack = GameObject.Find("Canvas").GetComponent<HealthBarStack>();
                return;
            }
        }

        /// <summary>
        /// Adds a new connected player to tbe player list and adds the player's health bar.
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // Generate randomly the spawn position
            var randomSpawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));

            // Instantiate the player object.
            var player = Instantiate(this.playerPrefab, randomSpawnPosition, default);

            // Set the player's player ID as the least possible unique int from 1
            uint playerId = 1;
            while (this.playerList.ContainsPlayerId(playerId))
            {
                ++playerId;
            }

            // Set a unique object name for the player. This name is shown only in the debugger.
            player.name = "Player " + playerId;

            NetworkServer.AddPlayerForConnection(conn, player);
            this.playerList.AddPlayer(player, playerId);
            this.healthBarStack.AddHealthBarForPlayer(player);
        }

        /// <summary>
        /// Removes the player from the player list and removes the player's health bar when a client disconnects.
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            var player = conn.identity.gameObject;
            this.playerList.RemovePlayerWithNetId(player);
            this.healthBarStack.RemoveHealthBarForPlayer(player);

            // Calling the base implementation is necessary
            base.OnServerDisconnect(conn);
        }
    }
}