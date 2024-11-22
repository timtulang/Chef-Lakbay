using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingItems : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    public void OnPointerClick(PointerEventData eventData)
    {
        CraftingItems items = GetComponent<CraftingItems>();
        if (items == null) return;

        if (Time.time - lastClickTime <= doubleClickThreshold)
        {
            // Handle double-click
            Debug.Log($"Double-click detected on: {items.itemName}");
            RemoveOption(items);
        }
        lastClickTime = Time.time;
    }

    private void RemoveOption(CraftingItems items)
    {
        items.gameObject.SetActive(false);
        items.GetComponent<UnityEngine.UI.Image>().sprite = null;
        items.itemName = "";
    }
}
