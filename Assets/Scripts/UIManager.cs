using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button StartServerButton;

    [SerializeField]
    private Button StartHostButton;

    [SerializeField]
    private Button StartClientButton;

    [SerializeField]
    private TextMeshProUGUI PlayersInGameText;

    private void Awake() {
        Cursor.visible = true;
    }

    private void Start() {
        StartHostButton.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartHost()) {
               
            } else {
                
            }
        });
        StartServerButton.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartServer()) {
                
            } else {
                
            }
        });
        StartClientButton.onClick.AddListener(() => {
            if(NetworkManager.Singleton.StartClient()) {
                
            } else {
                
            }
        });
    }

    private void Update() {
        PlayersInGameText.text = $"Players in game: {PlayerManager.Instance.PlayersInGame}";
    }

}
