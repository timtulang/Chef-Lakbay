using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerGrab : MonoBehaviour
{
    public Transform holdPoint; // The position where the player holds the item
    private GameObject currentItem; // The item the player is currently holding
    [SerializeField] private Tilemap map; // The
    [SerializeField] private Animator anim;
    [SerializeField]
    private List<GameObject> choppable;
    private Vector3Int cellPosition;
    private TileBase tile;
    private bool itemDocked = false;

    private string INTERACT_ANIMATION = "actionBtnPressed";

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
            if (StationChecker())
            {
                Vector3 cellPos = map.CellToWorld(cellPosition);

                // Set rotation from the tile in the Tilemap
                Matrix4x4 tileMatrix = map.GetTransformMatrix(cellPosition);
                currentItem.transform.rotation = Quaternion.LookRotation(Vector3.forward, tileMatrix.GetColumn(1));
                currentItem.transform.position = cellPos + map.tileAnchor;


                currentItem.GetComponent<CircleCollider2D>().radius *= 1.5f;
                itemDocked = true;
            }
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
        }
    }

    public void GrabItem(GameObject item)
    {
        Debug.Log(holdPoint.transform.position);
        currentItem = item;
        if (itemDocked)
            currentItem.GetComponent<CircleCollider2D>().radius /= 1.5f;

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

        itemDocked = false;
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

        currentItem = null; // Clear reference
    }
    bool StationChecker()
    {
        cellPosition = map.WorldToCell(holdPoint.transform.position); // Get the cell position
        tile = map.GetTile(cellPosition); // Get the tile at that position
        //Rigidbody2D rb = currentItem.GetComponent<Rigidbody2D>();

        if (tile != null)
        {
            if (tile.name == "Stove" && currentItem.name == "Kawali")
            {
                return true;
            }
            if (tile.name == "Plate" && ListChecker())
            {
                return true;
            }
        }
        return false;
    }
    bool ListChecker()
    {
        foreach (GameObject obj in choppable)
        {
            if (obj.name == currentItem.name)
            {
                return true;
            }
        }
        return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f); // Match OverlapCircle radius
    }
}