using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform holdPoint; // The position where the player holds the item
    private GameObject currentItem; // The item the player is currently holding
    [SerializeField] private Animator anim;

    private string INTERACT_ANIMATION = "actionBtnPressed";

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //For testing purposes only
        {
            ActionGrab();
        }
    }
    public void ActionGrab()
    {
        StartCoroutine(ActionGrabDelay());
    }
    IEnumerator ActionGrabDelay()
    {
        anim.SetBool(INTERACT_ANIMATION, true);
        if (currentItem != null)
        {
            DropItem();
        }
        else
        {
            TryGrabItem();
        }
        yield return new WaitForSeconds(0.1f);
        anim.SetBool(INTERACT_ANIMATION, false);
    }

    void TryGrabItem()
    {
        // Define layer mask to detect grabbable items only
        LayerMask grabbableLayer = LayerMask.GetMask("Items");

        // Detect grabbable items within range
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, 0.5f, grabbableLayer);

        // Check if there's a grabbable item
        foreach (Collider2D item in items)
        {
            if (item.CompareTag("Item"))
            {
                GrabItem(item.gameObject);
                return; // Stop after grabbing the first valid item
            }
            Debug.Log("Detected Item: " + item.name);
        }
    }

    public void GrabItem(GameObject item)
    {
        currentItem = item;

        // Snap the item to the hold point
        currentItem.transform.position = holdPoint.position;
        currentItem.transform.rotation = holdPoint.rotation;

        // Parent the item to the player to keep it moving with them
        currentItem.transform.SetParent(holdPoint);

        // Disable physics so the item doesn't move independently
        Rigidbody2D rb = currentItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop any movement
            rb.angularVelocity = 0f;   // Stop rotation
            rb.isKinematic = true;     // Disable physics
            rb.simulated = false;
        }
    }

    void DropItem()
    {
        // Unparent the item
        currentItem.transform.SetParent(null);

        // Re-enable physics on the item
        Rigidbody2D rb = currentItem.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = false; // Enable physics
            rb.simulated = true;    // Resume Rigidbody2D simulation
        }

        // Change the item's layer back to Default or Grabbable
        //currentItem.layer = LayerMask.NameToLayer("Item");

        currentItem = null; // Clear reference
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f); // Match OverlapCircle radius
    }
}