// <copyright file="PlayerList.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using Mirror;

    /// <summary>
    /// Maintains the list of players.
    /// </summary>
    /// <para>The class is necessary because <see cref="SyncDictionary{TKey,TValue}"/> is only available for subclasses
    /// of <see cref="NetworkBehaviour"/>.</para>
    public class PlayerList : NetworkBehaviour
    {
        /// <summary>
        /// Gets the dictionary that maps a player's net ID to that player's ID in game (player ID).
        /// </summary>
        public SyncDictionary<uint, uint> PlayerIdForNetId { get; } = new SyncDictionary<uint, uint>();
    }
}