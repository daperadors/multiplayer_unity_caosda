
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : NetworkBehaviour
{
    public Button startButton;
    [SerializeField] private TextMeshProUGUI m_textPlayers;

    private NetworkVariable<int> players = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        players.OnValueChanged += (int previousValue, int newValue) =>
        {
            m_textPlayers.text = players.Value + "";
        };
        if (IsOwner)
        {
            startButton.onClick.AddListener(() =>
            {
                if (!IsOwner) return;
                if (GameManager.m_Instance._players.Count > 0) ;
                NetworkManager.Singleton.SceneManager.LoadScene("GameDavid", LoadSceneMode.Single);
            });
        }
        else startButton.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!IsOwner) return;
        players.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
