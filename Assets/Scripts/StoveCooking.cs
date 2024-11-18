using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StoveCooking : MonoBehaviour
{
    public float secondsCook, secondsOverCook;
    public Transform holdPoint;
    public Tilemap tilemap;
    public TileBase currentTile, stoveTile;
    private Vector3Int cellPos;
    public GameObject kawali;
    public List<RawCookedMapping> rawCookedMappingList;
    private string objectName;
    private string kawaliName = "Kawali";
    public bool isOnStove;
    private bool stillCooking;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Update: " + tileCheck());
            if (tileCheck())
            {
                StartCoroutine(Cook());
            }
        }
    }
    bool tileCheck()
    {
        cellPos = tilemap.WorldToCell(kawali.transform.position);
        currentTile = tilemap.GetTile(cellPos);
        return currentTile == stoveTile; //&& kawali.transform.childCount == 1;
    }


    IEnumerator Cook()
    {
        // Wait for the cooking process
        yield return new WaitForSeconds(secondsCook);
        Debug.Log("Cook: " + tileCheck());
        if (!tileCheck())
        {
            yield break;
        }
        foreach (RawCookedMapping mapping in rawCookedMappingList)
        {
            GameObject rawChild = kawali.transform.GetChild(0).gameObject; // Line 49
            if (mapping.raw.gameObject.GetComponent<CraftingItems>().itemName == rawChild.GetComponent<CraftingItems>().itemName)
            {
                // Destroy the raw child
                Destroy(rawChild.gameObject);
                GameObject cookedObject = Instantiate(mapping.processed);
                cookedObject.SetActive(true);

                // Set it as a child of stove
                cookedObject.transform.SetParent(kawali.transform);
                cookedObject.transform.localPosition = Vector3.zero; // Optional: Reset position
                break;
            }
        }
        // Wait for the cooking process
        yield return new WaitForSeconds(secondsOverCook);
        if (!tileCheck())
        {
            yield break;
        }
        foreach (RawCookedMapping mapping in rawCookedMappingList)
        {
            GameObject cookedChild = kawali.transform.GetChild(0).gameObject; // Cache the raw child
            if (mapping.processed.gameObject.GetComponent<CraftingItems>().itemName == cookedChild.GetComponent<CraftingItems>().itemName) //Line 72
            {
                // Destroy the raw child
                Destroy(cookedChild.gameObject);
                GameObject cookedObject = Instantiate(mapping.over);
                cookedObject.SetActive(true);

                // Set it as a child of stove
                cookedObject.transform.SetParent(kawali.transform);
                cookedObject.transform.localPosition = Vector3.zero; // Optional: Reset position

                break;
            }
        }
    }
}