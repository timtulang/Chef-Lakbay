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
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if HoldPoint is overlapping Kawali
        if (other.gameObject.name == "Kawali")
        {
            kawali = other.gameObject;
            if (manager.ListChecker("Kawali"))
            {
                canCook = true;

                Debug.Log("HoldPoint is on top of Kawali!");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Remove Kawali reference when HoldPoint is no longer on it
        if (other.gameObject.name == "Kawali")
        {
            kawali = null;
            Debug.Log("HoldPoint moved away from Kawali.");
        }
    }
    void RemoveItemFromKawali(){
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && canCook && pg.CurrentItem != null)
        {
            //pg.CurrentItem.gameObject.SetActive(false);
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
            kawali.GetComponent<CircleCollider2D>().radius *= 1.25f;
            pg.CurrentItem.GetComponent<CircleCollider2D>().radius /= 1.25f;
            pg.CurrentItem = null;
        }
    }
}
