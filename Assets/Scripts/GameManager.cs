using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class GameManager : NetworkBehaviour
{
    public List<NetworkObject> jugadores = new List<NetworkObject>();
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
        print("Client connected");
    }
}
