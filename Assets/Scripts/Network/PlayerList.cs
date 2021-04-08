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
        /// Returns whether there is a player with the provided player ID in the list.
        /// </summary>
        /// <param name="playerId">the player ID to be checked.</param>
        /// <returns>whether there is a player with the provided player ID in the list.</returns>
        public bool ContainsPlayerId(uint playerId)
        {
            return this.playerIdForNetId.Values.Contains(playerId);
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
        /// Adds a player to the player list.
        /// </summary>
        /// <param name="player">the player to be added.</param>
        /// <param name="playerId">the player ID of the player.</param>
        public void AddPlayer(GameObject player, uint playerId)
        {
            this.playerIdForNetId.Add(player.GetComponent<NetworkIdentity>().netId, playerId);
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