using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleConnectionUI : MonoBehaviour
{

    [SerializeField] private Button m_ServerButton;
    [SerializeField] private Button m_ClientButton;
    [SerializeField] private Button m_HostButton;


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
}
