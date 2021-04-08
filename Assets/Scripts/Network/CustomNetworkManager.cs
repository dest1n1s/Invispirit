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

        /// <summary>
        /// Adds a new connected player to tbe player list and adds the player's health bar.
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // Add the player to the player list
            var player = this.playerList.CreateAndAddPlayer(this.playerPrefab);

            // Add the player to the network server
            NetworkServer.AddPlayerForConnection(conn, player);

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