using UnityEngine;
using Unity.Netcode;
using DilmerGames.Core.Singletons;

public class PlayerManager : Singleton<PlayerManager> {
   
    private NetworkVariable<int> playersInGame = new NetworkVariable<int>();

    public int PlayersInGame {
        get {
            return playersInGame.Value;
        }
    }

    private void Start() {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) => {
            if(IsServer) {
                print($"{id} just connected...");
                playersInGame.Value++;
            }
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) => {
            if(IsServer) {
                print($"{id} just disconnected...");
                playersInGame.Value--;
            }
        };
    }
}
