using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    // Player prefab
    [SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
    // Spawn locations
    [SerializeField] private Transform[] spawnPoints;

    // Runs when the game starts
    public override void Spawned()
    {
        // Only the server spawns players
        if (Runner.IsServer)
        {
            foreach (var item in Runner.ActivePlayers)
            {
                // Spawn all existing players
                SpawnPlayer(item);
            }
        }
           
    }

    // Spawns a player at an available spawn point
    private void SpawnPlayer(PlayerRef playerRef)
    {
        // Only the server handles spawning
        if (Runner.IsServer)
        {
            // Choose a spawn point
            var index = playerRef % spawnPoints.Length;
            var spawnPoint = spawnPoints[index].transform.position;
            var playerObject = Runner.Spawn(playerNetworkPrefab, spawnPoint, Quaternion.identity, playerRef);
            
            // Link player to Fusion
            Runner.SetPlayerObject(playerRef, playerObject);
        }
    }

    // Removes a player when they leave the game
    private void DespawnPlayer(PlayerRef playerRef)
    {
        // Only the server handles despawning
        if (Runner.IsServer)
        {
            if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
            {
                // Remove the player object
                Runner.Despawn(playerNetworkObject);
            }

            //Reset player object an clear Fusion reference
            Runner.SetPlayerObject(playerRef, null);
        }
    }

    // Called when a new player joins
    public void PlayerJoined(PlayerRef player)
    {
        SpawnPlayer(player);
    }

    // Called when a player leaves
    public void PlayerLeft(PlayerRef player)
    {
        DespawnPlayer(player);
    }
}
