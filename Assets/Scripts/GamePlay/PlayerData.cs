using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

//INetwork Input send player input data across the network
public struct PlayerData : INetworkInput
{
    // Stores horizontal movement input (-1 = Left, 1 = Right)
    public float HorizontalInput;
    //NetworkButtons tracks button presses (like Jump, Shoot, or Dash)
    public NetworkButtons networkButtons;
}
