using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleConnectionUI : NetworkBehaviour
{

    [SerializeField] private Button m_ServerButton;
    [SerializeField] private Button m_ClientButton;
    [SerializeField] private Button m_HostButton;
    [SerializeField] private TextMeshProUGUI m_textPlayers;

    private NetworkVariable<int> players = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);
    public override void OnNetworkSpawn()
    {
        players.OnValueChanged += (int previousValue, int newValue) =>
        {
            m_textPlayers.text = players.Value + "";
        };
    }
    void Awake()
    {

        m_ServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        m_ClientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        m_HostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
        });
    }

    private void Update()
    {
        if (!IsOwner) return;
        players.Value = NetworkManager.Singleton.ConnectedClients.Count;
        

    }
}
