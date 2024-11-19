using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportButtonHandler : MonoBehaviour
{
    public PlayerTeleport pg;
    // Start is called before the first frame update
    public void OnTeleportButtonPress()
    {
        if (!PauseMenu.isPaused)
            pg.Teleport();
    }
}
