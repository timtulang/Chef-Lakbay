using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TileSpawnerManager map;
    public FoodProcessManager manager;

    public void OnActionButtonPressed()
    {
        if (map != null && !PauseMenu.isPaused)
        {
            map.InteractWithTile();
            manager.Action();
        }
    }
}
