// <copyright file="PlayerList.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using System.Collections.Generic;
    using System.Linq;
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// The list of all connected players.
    /// </summary>
    public class PlayerList : NetworkBehaviour
    {
        private readonly SyncDictionary<uint, uint> playerIdForNetId = new SyncDictionary<uint, uint>();

        /// <summary>
        /// Returns the net ID's of the all players connected.
        /// </summary>
        /// <returns>the net ID's of the all players connected.</returns>
        public IEnumerable<uint> NetIds()
        {
            return this.playerIdForNetId.Keys;
        }

        /// <summary>
        /// Returns whether there is a player with the provide net ID in the list.
        /// </summary>
        /// <param name="playerNetId">the net ID to be checked.</param>
        /// <returns>whether there is a player with the provide net ID in the list.</returns>
        public bool ContainsNetId(uint playerNetId)
        {
            return this.playerIdForNetId.ContainsKey(playerNetId);
        }

        /// <summary>
        /// Returns the player ID of the player with the provided net ID.
        /// </summary>
        /// <param name="playerNetId">the net ID to be searched.</param>
        /// <returns>the player ID of the player with the provided net ID.</returns>
        public uint PlayerIdForNetId(uint playerNetId)
        {
            return this.playerIdForNetId[playerNetId];
        }

        /// <summary>
        /// Creates a new player object from a prefab, adds the player to the player list, and returns the player
        /// object.
        /// </summary>
        /// <param name="playerPrefab">the prefab from which the player is to be created.</param>
        /// <returns>the player object created from the method.</returns>
        public GameObject CreateAndAddPlayer(GameObject playerPrefab)
        {
            var randomSpawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));
            var player = Instantiate(playerPrefab, randomSpawnPosition, default);

            // Set the player's player ID as the least possible unique int from 1
            uint playerId = 1;
            while (!this.playerIdForNetId.Values.Contains(playerId))
            {
                ++playerId;
            }

            // Set a unique object name for the player. This name is shown only in the debugger.
            player.name = "Player " + playerId;

            this.playerIdForNetId.Add(player.GetComponent<NetworkIdentity>().netId, playerId);
            return player;
        }

        /// <summary>
        /// Removes a player from the list.
        /// </summary>
        /// <param name="player">the player to be removed.</param>
        public void RemovePlayerWithNetId(GameObject player)
        {
            var playerNetId = player.GetComponent<NetworkIdentity>().netId;
            if (this.playerIdForNetId.ContainsKey(playerNetId))
            {
                this.playerIdForNetId.Remove(playerNetId);
            }
        }
    }
}