using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : NetworkBehaviour
{
    public List<Player> players = new List<Player>();
    public NetworkVariable<int> currentPlayer = new NetworkVariable<int>(0);


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        // Obtener los jugadores
        Player[] playerList = FindObjectsOfType<Player>();
        players.AddRange(playerList);
        SceneManager.LoadScene("GameDavid");
    }

    // Cambiar el turno
    public void NextPlayer()
    {
        currentPlayer.Value = (currentPlayer.Value + 1) % players.Count;
        ChangeTurnOnClientsServerRpc();
    }

    // Comando para cambiar el turno
    [ServerRpc]
    void ChangeTurnOnClientsServerRpc()
    {
        ChangeTurnClientRpc();
    }

    // RPC para cambiar el turno en los clientes
    [ClientRpc]
    void ChangeTurnClientRpc()
    {
        foreach (var player in players)
        {
            player.isMyTurn = player.playerID == currentPlayer.Value;
        }
    }
}
public class Player : UnityEngine.Object
{
    public bool isMyTurn;
    public int playerID;

    public Player(int id)
    {
        playerID = id;
        isMyTurn = false;
    }
}
