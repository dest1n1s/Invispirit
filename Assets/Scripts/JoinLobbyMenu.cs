using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;
using Network;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField]
    private NetworkManagerLobby networkManager = null;
    [Header("UI")]
    [SerializeField]
    private GameObject landingPagePanel;
    [SerializeField]
    private TMP_InputField ipAddressInputField = null;
    [SerializeField]
    private Button joinButton = null;

	private void OnEnable()
	{
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

	private void OnDisable()
	{
        NetworkManagerLobby.OnClientConnected += HandleClientConnected;
        NetworkManagerLobby.OnClientDisconnected += HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        string ipAddress = ipAddressInputField.text;
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();
        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        this.joinButton.interactable = true;
        this.gameObject.SetActive(false);
        this.landingPagePanel.SetActive(false);
    }

    private void HandleClientDisconnected()
    {
        this.joinButton.interactable = true;
    }
}
