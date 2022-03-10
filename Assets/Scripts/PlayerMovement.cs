using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField]
    private float WalkSpeed = 0.05f;

    private Vector2 defaultPositionRange = new Vector2(-4, 4);

    [SerializeField]
    private NetworkVariable<float> ForwardBackPosition = new NetworkVariable<float>();

    [SerializeField]
    private NetworkVariable<float> LeftRightPosition = new NetworkVariable<float>();


    // Caching
    private float OldForwardBackPosition;
    private float OldLeftRightPosition;

    private void Start() {
        transform.position = new Vector3(Random.Range(defaultPositionRange.x, defaultPositionRange.y), Random.Range(defaultPositionRange.x, defaultPositionRange.y), 0);
    }

    private void Update() {
        if(IsServer) {
            UpdateServer();
        }

        if(IsClient && IsOwner) {
            UpdateClient();
        }
    }

    private void UpdateServer() {
        transform.position = new Vector3(transform.position.x + LeftRightPosition.Value, transform.position.y + ForwardBackPosition.Value, transform.position.z);
    }

    private void UpdateClient() {
        float ForwardBackward = 0;
        float LeftRight = 0;

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            ForwardBackward += WalkSpeed;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            ForwardBackward -= WalkSpeed;
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            LeftRight -= WalkSpeed;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            LeftRight += WalkSpeed;
        }



        if(OldForwardBackPosition != ForwardBackward || OldLeftRightPosition != LeftRight) {
            OldForwardBackPosition = ForwardBackward;
            OldLeftRightPosition = LeftRight;
            UpdateClientPositionServerRpc(ForwardBackward, LeftRight);
        }
    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(float ForwardBackward, float LeftRight) {
        ForwardBackPosition.Value = ForwardBackward;
        LeftRightPosition.Value = LeftRight;
    }

}
