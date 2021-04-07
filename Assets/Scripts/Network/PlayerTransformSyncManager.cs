// <copyright file="PlayerTransformSyncManager.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using Mirror;
    using UnityEngine;

    /// <summary>
    /// Manages the synchronization of the transforms of a player.
    /// </summary>
    public class PlayerTransformSyncManager : NetworkBehaviour
    {
        /// <summary>
        /// The time (0.1s) it takes for the object to travel from its original position (the value on the client)
        /// to its position it should be (the value on the server).
        /// </summary>
        [SerializeField]
        private float syncDuration = 0.1f;

        private new Rigidbody2D rigidbody; // overrides a deprecated field

        /// <summary>
        /// Find the rigid body object when starting the server.
        /// </summary>
        public override void OnStartServer()
        {
            this.rigidbody = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Finds the rigid body object when starting a client.
        /// </summary>
        public override void OnStartClient()
        {
            this.rigidbody = this.GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Synchronizes the position and rotation on the client.
        /// </summary>
        [ServerCallback]
        private void LateUpdate()
        {
            this.SyncPositionOnClientTo(this.rigidbody.position);
            this.SyncRotationOnClientTo(this.rigidbody.rotation);
        }

        [ClientRpc]
        private void SyncPositionOnClientTo(Vector2 position)
        {
            this.rigidbody.velocity += (position - this.rigidbody.position) / this.syncDuration;
        }

        [ClientRpc]
        private void SyncRotationOnClientTo(float rotation)
        {
            this.rigidbody.rotation = rotation;
        }
    }
}