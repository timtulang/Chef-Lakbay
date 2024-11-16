using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButtonHandler : MonoBehaviour
{
    public PlayerGrab player;

    public void OnActionButtonPressed()
    {
        if (player != null)
        {
            player.ActionGrab();
        }
    }
}
