using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject npcPrefab; // The NPC prefab to spawn

    [SerializeField]
    private GameObject spawnPoint; // Spawn point (Point A)

    [SerializeField]
    private GameObject destinationPointB; // Destination point (Point B)

    private bool hasSpawned = false; // Tracks whether an NPC has been spawned

    void Start()
    {
        if (!hasSpawned)
        {
            SpawnNpc();
        }
    }

    private void SpawnNpc()
    {
        // Instantiate the NPC at the spawn point
        GameObject newNpc = Instantiate(npcPrefab, spawnPoint.transform.position, Quaternion.identity);

        // Configure the NPC's movement points
        NpcMovement npcMovement = newNpc.GetComponent<NpcMovement>();
        if (npcMovement != null)
        {
            npcMovement.pointA = spawnPoint;
            npcMovement.pointB = destinationPointB;
        }

        // Mark as spawned to prevent additional spawns
        hasSpawned = true;
    }
}
