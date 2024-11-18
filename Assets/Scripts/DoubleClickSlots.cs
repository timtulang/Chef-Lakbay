using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClickSlots : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Time (in seconds) between clicks to count as double-click

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime <= doubleClickThreshold)
        {
            RemoveOption();
        }
        lastClickTime = Time.time;
    }
    public void RemoveOption()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = null;
        gameObject.GetComponent<CraftingItems>().itemName = "";
    }
}
