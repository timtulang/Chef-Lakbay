using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingManager : MonoBehaviour
{
    private CraftingItems currentItem;
    public UnityEngine.UI.Image customCursor;
    public Slots[] craftingSlots;
    public CraftingItems[] optionsSlots;
    public List<CraftingItems> itemList;
    public string[] recipes;
    public GameObject[] recipeResults;
    public Slots resultSlot;
    public Canvas canvas;
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
                    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mouseWorldPosition.z = 0;
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
                nearestSlot.item = currentItem;

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
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                resultSlot.gameObject.SetActive(true);
                resultSlot.GetComponent<UnityEngine.UI.Image>().sprite = recipeResults[i].GetComponent<SpriteRenderer>().sprite;
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
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<UnityEngine.UI.Image>().sprite;
            customCursor.gameObject.GetComponent<UnityEngine.UI.Image>().preserveAspect = true;
        }
    }
    public void CloseCraftingUI()
    {
        canvas.gameObject.SetActive(false);
    }
}
