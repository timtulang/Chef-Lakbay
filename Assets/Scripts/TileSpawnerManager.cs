using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSpawnerManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap; // Reference to the tilemap
    [SerializeField]
    private List<TileItemMapping> tileMappings; // List of tile-to-item mappings
    [SerializeField]
    private Transform spawnPoint; // Where the item will spawn
    public static int currentObjects = 0;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Example key to interact with tiles
        {
            InteractWithTile();
        }
    }

    public void InteractWithTile()
    {
        // Get the player's current position
        Vector3Int cellPosition = tilemap.WorldToCell(spawnPoint.transform.position);

        // Get the tile at the player's position
        TileBase tile = tilemap.GetTile(cellPosition);
        if (!tilemap.HasTile(cellPosition))
        {
            return;
        }

        if (tile != null && currentObjects < 20)
        {
            currentObjects++;
            Debug.Log(currentObjects);
            // Find the corresponding item prefab for the tile
            foreach (TileItemMapping mapping in tileMappings)
            {
                if (mapping.tile == tile)
                {
                    // Spawn the corresponding item at the spawn point
                    Instantiate(mapping.itemPrefab, spawnPoint.position, Quaternion.identity);
                    return;
                }
            }
        }

    }
}

