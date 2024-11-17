using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewTileItemMapping", menuName = "ScriptableObjects/TileItemMapping")]
public class TileItemMapping : ScriptableObject
{
    public TileBase tile;       // Reference to the tile
    public GameObject itemPrefab; // Prefab to spawn
}

