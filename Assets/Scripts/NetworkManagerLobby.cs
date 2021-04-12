// <copyright file="NetworkManagerLobby.cs" company="ECYSL">
// Copyright (c) ECYSL. All rights reserved.
// </copyright>

namespace Network
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Mirror;
    using UnityEngine;
    using UnityEngine.SceneManagement;

	public class NetworkManagerLobby : NetworkManager
    {
		[Scene] [SerializeField] private string menuScene = string.Empty;
		[Header("Room")]
		[SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;
		[SerializeField]
		private PlayerList playerList;
		[SerializeField]
		private HealthBarStack healthBarStack;
		public static event Action OnClientConnected;
		public static event Action OnClientDisconnected;

		public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();



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
			if (SceneManager.GetActiveScene().name == menuScene)
			{
				// Generate randomly the spawn position
				var randomSpawnPosition = new Vector3(UnityEngine.Random.Range(-8.0f, 8.0f), UnityEngine.Random.Range(-8.0f, 8.0f));

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
		}

		/// <summary>
		/// Removes the player from the player list and removes the player's health bar when a client disconnects.
		/// </summary>
		/// <param name="conn">Connection from client.</param>
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			var player = Instantiate(this.playerPrefab);
			player = conn.identity.gameObject;
			this.playerList.RemovePlayerWithNetId(player);
			this.healthBarStack.RemoveHealthBarForPlayer(player);

			// Calling the base implementation is necessary
			base.OnServerDisconnect(conn);
		}

		public override void OnStartClient()
		{
			var spawnablePrefabs = Resources.LoadAll<GameObject>("spawnablePrefabs");
			foreach (var prefab in spawnablePrefabs)
			{
				ClientScene.RegisterPrefab(prefab);
			}
		}

		public override void OnClientConnect(NetworkConnection conn)
		{
			base.OnClientConnect(conn);
			OnClientConnected?.Invoke();
		}

		public override void OnClientDisconnect(NetworkConnection conn)
		{
			base.OnClientDisconnect(conn);
			OnClientDisconnected?.Invoke();
		}

		public override void OnServerConnect(NetworkConnection conn)
		{
			if (numPlayers >= maxConnections)
			{
				conn.Disconnect();
				return;
			}
			if (SceneManager.GetActiveScene().name != menuScene)
			{
                conn.Disconnect();
                return;
			}
        }
    }
}