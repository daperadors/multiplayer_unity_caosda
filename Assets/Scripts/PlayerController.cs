using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    //private void Start()
    //{
    //    m_Turn = NetworkManager.singleton.t;
    //}
}
