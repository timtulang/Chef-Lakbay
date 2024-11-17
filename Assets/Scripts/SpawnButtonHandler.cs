using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TileSpawnerManager map;

    public void OnActionButtonPressed()
    {
        if (map != null)
        {
            map.InteractWithTile();
        }
    }
}
