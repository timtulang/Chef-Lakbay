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
    private bool canCook;
    private bool isOnKawali; // Flag to track if the HoldPoint is on Kawali

    void OnTriggerStay2D(Collider2D other)
    {
        // Check if HoldPoint is overlapping Kawali
        if (other.gameObject.GetComponent<ItemIdentifier>() != null)
        {
            string itemName = other.gameObject.GetComponent<ItemIdentifier>().itemName;
            if (itemName == "Kawali")
            {
                kawali = other.gameObject;
                if (manager.ListChecker("Kawali"))
                {
                    isOnKawali = true; // Set flag to true when on Kawali
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
        // Only proceed if Kawali is assigned and it's empty, or if there's a current item
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
                pg.CurrentItem.GetComponent<CircleCollider2D>().radius *= 2;
                pg.CurrentItem = null;
            }
            else if (kawali.transform.childCount > 0)
            {
                canCook = false;
                pg.GrabItem(kawali.transform.GetChild(0).gameObject);
                pg.CurrentItem.GetComponent<CircleCollider2D>().isTrigger = false; // Re-enable collider interaction
                pg.CurrentItem.GetComponent<CircleCollider2D>().radius /= 2;
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
