using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FoodProcessManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> choppable;
    [SerializeField]
    private List<GameObject> craftable;
    [SerializeField]
    private List<GameObject> cookable;
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private TileBase plate, choppingBoard, stove;
    [SerializeField]
    private GameObject uiPlate;
    [SerializeField]
    private PlayerGrab pg;
    public TileBase currentTile;
    public GameObject kawali;
    public Transform holdPoint;
    private Vector3Int cellPosition;
    public bool chop = false;
    public bool cook = false;

    public delegate void stoveDocked();
    public static stoveDocked startCookRoutine;
    public delegate void chopDocked();
    public static chopDocked startChopRoutine;
    public void Action()
    {
        tileChecker();

        if (currentTile == plate)
        {
            uiPlate.SetActive(true);
        }

        if (currentTile == stove || currentTile == choppingBoard)
        {
            StationChecker();
            pg.DropItem();
        }
    }

    public void AddToPlate()
    {
        tileChecker();
        StationChecker();
    }
    public void DockItems()
    {
        Vector3 cellPos = tilemap.CellToWorld(cellPosition);

        // Set rotation from the tile in the Tilemap
        Matrix4x4 tileMatrix = tilemap.GetTransformMatrix(cellPosition);
        pg.CurrentItem.transform.rotation = Quaternion.LookRotation(Vector3.forward, tileMatrix.GetColumn(1));
        pg.CurrentItem.transform.position = cellPos + tilemap.tileAnchor;
    }
    public void tileChecker()
    {
        cellPosition = tilemap.WorldToCell(holdPoint.transform.position);
        currentTile = tilemap.GetTile(cellPosition);
    }

    public void StationChecker()
    {
        tileChecker();
        Debug.Log(currentTile != null && pg.CurrentItem != null);
        if (currentTile != null && pg.CurrentItem != null)
        {
            if (pg.CurrentItem.GetComponent<ItemIdentifier>() != null)
            {
                if (currentTile == stove && pg.CurrentItem.GetComponent<ItemIdentifier>().itemName == "Kawali")
                {
                    DockItems();
                    startCookRoutine?.Invoke();
                }
            }
            if (currentTile == choppingBoard && ListChecker("ChoppingBoard"))
            {
                DockItems();
                startChopRoutine?.Invoke();
            }
            if (currentTile == plate && ListChecker("Plate"))
            {
                DockItems();
                PopulateCraftingOptions();
                TileSpawnerManager.currentObjects--;
                Debug.Log(TileSpawnerManager.currentObjects);
            }
        }
    }
    public bool ListChecker(string station)
    {
        if (station == "ChoppingBoard")
        {
            foreach (GameObject obj in choppable)
            {
                if (obj.GetComponent<CraftingItems>().itemName == pg.CurrentItem.GetComponent<CraftingItems>().itemName)
                {
                    return true;
                }
            }
        }
        if (station == "Plate")
        {
            foreach (GameObject obj in craftable)
            {
                if (obj.GetComponent<CraftingItems>().itemName == pg.CurrentItem.GetComponent<CraftingItems>().itemName)
                {
                    return true;
                }
            }
        }
        if (station == "Kawali" && pg.CurrentItem != null)
        {
            GameObject itemOnKawali = pg.CurrentItem;
            foreach (GameObject obj in cookable)
            {
                if (obj.GetComponent<CraftingItems>().itemName == itemOnKawali.GetComponent<CraftingItems>().itemName)
                {
                    return true;
                }
            }
        }
        return false;
    }
    void PopulateCraftingOptions()
    {
        Transform options = uiPlate.transform.Find("OptionItems");

        foreach (Transform grandChild in options)
        {
            if (grandChild.GetComponent<CraftingItems>().itemName == "")
            {
                grandChild.gameObject.SetActive(true);
                grandChild.gameObject.GetComponent<CraftingItems>().itemName = pg.CurrentItem.GetComponent<CraftingItems>().itemName;
                grandChild.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = pg.CurrentItem.GetComponent<SpriteRenderer>().sprite;
                grandChild.gameObject.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;
                grandChild.transform.GetChild(0).gameObject.SetActive(true);
                grandChild.gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                if (pg.CurrentItem.GetComponent<CraftingItems>().itemName != "Sauce")
                    Destroy(pg.CurrentItem);
                return;
            }
        }
    }
}


