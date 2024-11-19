using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChoppingScript : MonoBehaviour
{
    public float secondsChop;
    public PlayerGrab pg;
    public Tilemap tilemap;
    public TileBase currentTile, boardTile;
    private Vector3Int cellPos;
    public GameObject ingredient;
    public List<RawCookedMapping> choppableMappings;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && pg.CurrentItem != null)
        {
            ingredient = pg.CurrentItem;
            if (tileCheck())
            {
                StartCoroutine(Chop(ingredient));
            }
        }
    }
    bool tileCheck()
    {
        cellPos = tilemap.WorldToCell(ingredient.transform.position);
        currentTile = tilemap.GetTile(cellPos);
        return currentTile == boardTile && ingredient != null;
    }


    IEnumerator Chop(GameObject ing)
    {
        // Wait for the cooking process
        yield return new WaitForSeconds(secondsChop);
        if (!tileCheck())
        {
            yield break;
        }
        foreach (RawCookedMapping mapping in choppableMappings)
        {
            GameObject rawIng = ing;
            if (mapping.raw.gameObject.GetComponent<CraftingItems>().itemName == rawIng.GetComponent<CraftingItems>().itemName)
            {
                // Destroy the raw child
                Destroy(rawIng.gameObject);
                GameObject cookedObject = Instantiate(mapping.processed);
                cookedObject.SetActive(true);
                cookedObject.GetComponent<CircleCollider2D>().isTrigger = true;
                cookedObject.GetComponent<CircleCollider2D>().radius *= 2f;

                cookedObject.transform.localPosition = cellPos + tilemap.tileAnchor;
                break;
            }
        }
    }
}