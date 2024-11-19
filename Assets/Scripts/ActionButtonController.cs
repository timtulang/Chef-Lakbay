using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonHandler : MonoBehaviour
{
    public PlayerGrab player;
    public FoodProcessManager manager;

    public void OnActionButtonPressed()
    {
        if (player != null && !PauseMenu.isPaused)
        {
            manager.AddToPlate();
            player.ActionGrab();
        }
    }
}
