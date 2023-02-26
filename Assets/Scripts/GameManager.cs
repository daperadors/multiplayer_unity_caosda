using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> CurrentTurn = new NetworkVariable<int>(-1);
    private List<NetworkClient> _players = new List<NetworkClient>();
    public NetworkClient actualPlayer;
    private bool gameStarted = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        SceneManager.LoadScene("GameDavid");
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            _players = (List<NetworkClient>)NetworkManager.Singleton.ConnectedClientsList;
            CurrentTurn.Value = 0;
            actualPlayer = _players[CurrentTurn.Value];
        }

    }
    private void Update()
    {
        if (!IsServer || gameStarted) return;

        _players = (List<NetworkClient>)NetworkManager.Singleton.ConnectedClientsList;
    }
    public void EndTurn()
    {
        if (IsServer)
        {
            CurrentTurn.Value++;
            if (CurrentTurn.Value >= _players.Count) CurrentTurn.Value = 0;
            actualPlayer = _players[CurrentTurn.Value];
            gameStarted = true;
            Debug.Log(actualPlayer.ClientId);
        }
    }

}
