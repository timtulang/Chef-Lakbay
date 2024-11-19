using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTeleport : MonoBehaviour
{

    private GameObject currentTeleporter;
    public Button tpBtn;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Teleport();
        }
    }
    public void Teleport()
    {
        if (currentTeleporter != null)
        {
            transform.position = currentTeleporter.GetComponent<TeleporterScript>().GetDestination().position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            tpBtn.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTeleporter = null;
        tpBtn.gameObject.SetActive(false);
    }

    // private void OnTriggerExit(Collider2D collision)
    // {
    //     if (collision.CompareTag("Teleporter"))
    //     {
    //         if (collision.gameObject == currentTeleporter)
    //         {
    //             currentTeleporter = null;
    //         }
    //     }
    // }
}
