using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private TextMeshProUGUI playerNameText;
    //reference to the camera
    [SerializeField] private GameObject cam;
    // Player movement speed
    [SerializeField] private float moveSpeed = 6;
    // Jump strength
    [SerializeField] private float jumpForce = 1000;

    [Networked(OnChanged = nameof(OnNickNameChanged))] private NetworkString<_16> playerName { get; set; }

    // Stores previous button state
    [Networked] private NetworkButtons buttonsPrev { get; set; }
    // Stores horizontal movement input
    private float horizontal;
    // Reference to the Rigidbody2D for physics movement
    private Rigidbody2D rigid;

    // Defines which buttons the player can press
    private enum PlayerInputButtons
    {
        // No button pressed
        None,
        Jump
    }

    // Called before Fusion processes updates (used for capturing input)
    public void BeforeUpdate()
    {
        // Only process input for the local player
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            const string HORIZONTAL = "Horizontal";
            // Get left/right movement input
            horizontal = Input.GetAxisRaw(HORIZONTAL);
        }
    }

    public override void Spawned()
    {
        // Get the Rigidbody2D component
        rigid = GetComponent<Rigidbody2D>();

        SetLocalObjects();
    }

    private void SetLocalObjects()
    {
        if (Runner.LocalPlayer == Object.HasInputAuthority)
        {
            cam.SetActive(true);

            var nickName = GlobalManagers.Instance.networkRunnerController.LocalPlayerNickname;
            RpcSetNickName(nickName);
        }
    }

    //Sends RPC to the HOST (from a client)
    //"sources" define wich PEER can send the rpc
    //The RpcTargets defines on wich it is executed
    [Rpc(sources: RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RpcSetNickName(NetworkString<_16> nickName)
    {
        playerName = nickName;
    }

    private static void OnNickNameChanged(Changed<PlayerController> changed)
    {
        changed.Behaviour.SetPlayerNickname(changed.Behaviour.playerName);
    }

    private void SetPlayerNickname(NetworkString<_16> nickName)
    {
        playerNameText.text = nickName + " " + Object.InputAuthority.PlayerId;
    }

    // Runs every network tick (Fusion's equivalent of FixedUpdate)
    public override void FixedUpdateNetwork()
    {
        // Get the latest input data from Fusion
        if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out var input))
        {
            // Apply horizontal movement
            rigid.velocity = new Vector2(input.HorizontalInput * moveSpeed, rigid.velocity.y);
            // Check if the player should jump
            CheckJumpInput(input);
        }
    }

    // Checks if the player pressed the jump button and applies force
    private void CheckJumpInput(PlayerData input)
    {
        var pressed = input.networkButtons.GetPressed(buttonsPrev);
        // If Jump button was newly pressed, apply upward force
        if (pressed.WasPressed(buttonsPrev, PlayerInputButtons.Jump))
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }
        // Store the current button state for the next frame
        buttonsPrev = input.networkButtons;
    }

    // Collects the player's input data for Fusion
    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        // Store horizontal movement
        data.HorizontalInput = horizontal;
        // Store jump button state (pressed or not)
        data.networkButtons.Set(PlayerInputButtons.Jump, Input.GetKey(KeyCode.Space));
        // Return input data for Fusion to sync
        return data;
    }

}
