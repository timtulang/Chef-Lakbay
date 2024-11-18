using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChoppingScript : MonoBehaviour
{
    public FoodProcessManager manager;
    public List<GameObject> ingredients; // List to store multiple ingredients
    public Tilemap tilemap; // Reference to your Tilemap
    public TileBase chopTile; // Reference to the chop tile
    public List<RawCookedMapping> mappings;

    public float secondsChop;

    void Update()
    {
        foreach (GameObject ingredient in ingredients)
        {
            // Get the position of the ingredient
            Vector3 objectWorldPosition = ingredient.transform.position;

            // Convert world position to tilemap grid position
            Vector3Int gridPosition = tilemap.WorldToCell(objectWorldPosition);

            // Check if the tile at the grid position is a chop tile
            TileBase tileAtPosition = tilemap.GetTile(gridPosition);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (tileAtPosition == chopTile)
                {
                    StartCoroutine(cook(ingredient)); // Start chopping for this specific ingredient
                }
            }
        }
    }

    IEnumerator cook(GameObject ingredient)
    {
        if (ingredient != null)
        {
            yield return new WaitForSeconds(secondsChop);

            Transform rawChild = ingredient.transform; // Cache the raw child

            foreach (RawCookedMapping map in mappings)
            {
                CraftingItems rawItem = rawChild.GetComponent<CraftingItems>();
                if (map.raw.GetComponent<CraftingItems>().itemName == rawItem.itemName)
                {
                    // Instantiate the cooked object from the prefab
                    GameObject cookedObject = Instantiate(map.processed);
                    cookedObject.SetActive(true);

                    // Set it as a child of the ingredient
                    cookedObject.transform.SetParent(ingredient.transform);
                    cookedObject.transform.localPosition = Vector3.zero; // Optional: Reset position

                    // Destroy the raw child
                    Destroy(rawChild.gameObject);

                    break; // Exit the loop after finding a match and chopping
                }
            }
        }
    }
}