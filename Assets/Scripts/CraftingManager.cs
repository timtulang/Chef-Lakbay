using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingManager : MonoBehaviour
{
    public CraftingItems currentItem;
    public Transform holdPoint;
    public UnityEngine.UI.Image customCursor;
    public Slots[] craftingSlots;
    public CraftingItems[] optionsSlots;
    public List<CraftingItems> itemList;
    public string[] recipes;
    public GameObject[] recipeResults;
    private GameObject dishResult;
    public Slots resultSlot;
    public GameObject uiPanel;
    public Camera mainCamera;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slots nearestSlot = null;
                float shortestDistance = float.MaxValue;
                foreach (Slots slot in craftingSlots)
                {
                    Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mouseWorldPosition.z = 0f;
                    float dist = Vector3.Distance(mouseWorldPosition, slot.transform.position);
                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<UnityEngine.UI.Image>().sprite = currentItem.GetComponent<UnityEngine.UI.Image>().sprite;
                nearestSlot.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;
                nearestSlot.GetComponent<UnityEngine.UI.Image>().color = new Color32(255, 255, 255, 255);
                nearestSlot.item = currentItem;

                Debug.Log(itemList);

                itemList[nearestSlot.index] = currentItem;
                CheckForCreatedRecipes();

                currentItem = null;
            }
        }
    }
    void CheckForCreatedRecipes()
    {
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;

        string currentRecipeString = "";
        foreach (CraftingItems item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }
        Debug.Log(currentRecipeString);
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                dishResult = recipeResults[i];
                resultSlot.GetComponent<UnityEngine.UI.Image>().sprite = recipeResults[i].GetComponent<SpriteRenderer>().sprite;
                resultSlot.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;
                resultSlot.item = recipeResults[i].GetComponent<CraftingItems>();
            }
        }
    }
    public void OnClickSlot(Slots slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }
    public void OnMouseDownItem(CraftingItems item)
    {
        Debug.Log(item);
        if (currentItem == null)
        {
            currentItem = item;
            Debug.Log(currentItem);
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<UnityEngine.UI.Image>().sprite;
            customCursor.gameObject.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;
        }
    }
    public void CloseCraftingUI()
    {
        uiPanel.SetActive(false);
    }
    public void RemoveOption(CraftingItems items)
    {
        if (Time.time - lastClickTime <= doubleClickThreshold)
        {
            items.gameObject.SetActive(false);
            items.GetComponent<UnityEngine.UI.Image>().sprite = null;
            items.GetComponent<CraftingItems>().itemName = "";
        }
        lastClickTime = Time.time;
    }
    public void SpawnResult()
    {
        Instantiate(dishResult, holdPoint.position, Quaternion.identity);
        clearAllItems();
    }
    public void clearAllItems()
    {
        foreach (Slots slot in craftingSlots)
        {
            slot.gameObject.SetActive(false);
            slot.item = null;
            itemList[slot.index] = null;
        }
        foreach (CraftingItems items in optionsSlots)
        {
            items.gameObject.SetActive(false);
            items.GetComponent<UnityEngine.UI.Image>().sprite = null;
            items.GetComponent<CraftingItems>().itemName = "";
        }
        resultSlot.gameObject.SetActive(false);
        resultSlot.item = null;
        dishResult = null;
        currentItem = null;
        customCursor.gameObject.SetActive(false);
        uiPanel.SetActive(false);
    }
}
