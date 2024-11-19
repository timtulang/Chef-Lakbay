using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPointManager : MonoBehaviour
{
    public GameObject kawali;
    [SerializeField]
    private PlayerGrab pg;
    [SerializeField]
    private FoodProcessManager manager;

    private bool isOnKawali; // Flag to track if the HoldPoint is on Kawali

    void OnTriggerStay2D(Collider2D other)
    {
        // Check if HoldPoint is overlapping Kawali
        if (other.gameObject.GetComponent<ItemIdentifier>() != null)
        {
            ItemIdentifier currentItemName;
            string itemName = other.gameObject.GetComponent<ItemIdentifier>().itemName;
            if (pg.CurrentItem != null && pg.CurrentItem.GetComponent<ItemIdentifier>() != null)
            {
                currentItemName = pg.CurrentItem.gameObject.GetComponent<ItemIdentifier>();
            }
            else { currentItemName = null; }
            //Debug.Log(currentItemName == null);
            if (currentItemName == null)
            {

                if (itemName == "Kawali")
                {
                    kawali = other.gameObject;
                    if (manager.ListChecker("Kawali") || pg.CurrentItem == null)
                    {
                        isOnKawali = true; // Set flag to true when on Kawali
                    }
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Remove Kawali reference when HoldPoint is no longer on it
        if (other.gameObject.GetComponent<ItemIdentifier>() != null)
        {
            string itemName = other.gameObject.GetComponent<ItemIdentifier>().itemName;
            if (itemName == "Kawali")
            {
                kawali = null;
                isOnKawali = false; // Reset flag when leaving Kawali

            }
        }
    }

    void PlaceRemoveItem()
    {
        if (kawali != null)
        {
            if (kawali.transform.childCount == 0 && pg.CurrentItem != null)
            {
                pg.CurrentItem.transform.position = kawali.transform.position;
                pg.CurrentItem.transform.rotation = kawali.transform.rotation;
                pg.CurrentItem.transform.SetParent(kawali.transform);
                Rigidbody2D rb = pg.CurrentItem.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero; // Stop any movement
                    rb.angularVelocity = 0f;   // Stop rotation
                    rb.isKinematic = true;     // Disable physics
                    rb.simulated = false;
                }

                pg.CurrentItem.GetComponent<CircleCollider2D>().isTrigger = true; // Disable collider interaction
                pg.CurrentItem.GetComponent<CircleCollider2D>().radius /= 2f;
                pg.CurrentItem = null;
            }
            else
            {
                kawali.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
                kawali.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().radius *= 2f;
                pg.GrabItem(kawali.transform.GetChild(0).gameObject);
            }
        }
    }

    void Update()
    {
        // Check if the HoldPoint is on the Kawali and 'R' is pressed
        if (isOnKawali && Input.GetKeyDown(KeyCode.R))
        {
            PlaceRemoveItem();
        }
    }
}
