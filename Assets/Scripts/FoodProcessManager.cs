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
    private Canvas canvasPlate;
    [SerializeField]
    private PlayerGrab pg;
    private TileBase currentTile;
    private bool _itemDocked = false;
    public Transform holdPoint;
    private Vector3Int cellPosition;
    public bool ItemDocked
    {
        get { return _itemDocked; }
        set { _itemDocked = value; }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            tileChecker();
            EnablePlate();
        }
    }

    public void EnablePlate()
    {
        if (currentTile == plate)
        {
            canvasPlate.gameObject.SetActive(true);
        }
    }
    public void DockItems()
    {
        Vector3 cellPos = tilemap.CellToWorld(cellPosition);

        // Set rotation from the tile in the Tilemap
        Matrix4x4 tileMatrix = tilemap.GetTransformMatrix(cellPosition);
        pg.CurrentItem.transform.rotation = Quaternion.LookRotation(Vector3.forward, tileMatrix.GetColumn(1));
        pg.CurrentItem.transform.position = cellPos + tilemap.tileAnchor;


        pg.CurrentItem.GetComponent<CircleCollider2D>().radius *= 1.5f;
        Debug.Log(currentTile);
        ItemDocked = true;
    }
    void tileChecker()
    {
        cellPosition = tilemap.WorldToCell(holdPoint.transform.position);
        currentTile = tilemap.GetTile(cellPosition);
    }

    public void StationChecker()
    {
        tileChecker();
        Debug.Log(currentTile + ": " + pg.CurrentItem.GetComponent<CraftingItems>().itemName);
        if (currentTile != null)
        {
            if (currentTile == stove && pg.CurrentItem.name == "Kawali")
            {
                DockItems();
            }
            if (currentTile == choppingBoard && ListChecker("ChoppingBoard"))
            {
                DockItems();
            }
            if (currentTile == plate && ListChecker("Plate"))
            {
                DockItems();
                PopulateCraftingOptions();
                Destroy(pg.CurrentItem);
                ItemDocked = true;
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
            foreach (GameObject obj in cookable)
            {
                if (obj.GetComponent<CraftingItems>().itemName == pg.CurrentItem.GetComponent<CraftingItems>().itemName)
                {
                    return true;
                }
            }
        }
        return false;
    }
    void PopulateCraftingOptions()
    {
        Transform options = canvasPlate.transform.Find("OptionItems");

        foreach (Transform grandChild in options)
        {
            if (grandChild.GetComponent<CraftingItems>().itemName == "")
            {
                Debug.Log("Grandchild name: " + grandChild.name);
                grandChild.gameObject.SetActive(true);
                grandChild.gameObject.GetComponent<CraftingItems>().itemName = pg.CurrentItem.GetComponent<CraftingItems>().itemName;
                grandChild.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = pg.CurrentItem.GetComponent<SpriteRenderer>().sprite;
                return;
            }
        }
    }
}


