using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class OrderManager : MonoBehaviour
{

    public List <DishMapping> ingredientList; // Final Dish

    public List <DishMapping> dishComponent; // Ingredients panel

    public List <GameObject> ingredientsImgs;

    public GameObject finalDish;

    public GameObject panel;
    

    // Reference to the OrderUI GameObject
    public List <GameObject> orderUIBtn; // Assign the OrderUI from the hierarchy
    public List <GameObject> exitBtn;
    // Time interval for showing the OrderUI
    public float orderSpawnInterval = 3f;

    void Start()
    {
        // Start the routine to show OrderUI periodically
        StartCoroutine(ShowOrderRoutine());
    }

    // Coroutine to show the OrderUI every `orderSpawnInterval` seconds
    private IEnumerator ShowOrderRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(orderSpawnInterval);
            ShowOrderUI();
        }
    }

    // Function to show the OrderUI
    private void ShowOrderUI()
    {
        foreach(GameObject orderBtn in orderUIBtn){
         if (!orderBtn.activeSelf)
         {
            orderBtn.SetActive(true);
            orderBtn.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>().sprite = ingredientList[Mathf.FloorToInt(UnityEngine.Random.Range(0, 2))].finalDish.GetComponent<SpriteRenderer>().sprite;
            break;
         }
        else
         {
            Debug.LogWarning("OrderUI reference is not set in the OrderManager!");
         }
        }
    }

    // Panel Images

    public void onOrderBtnClick(DishMapping dishPanel)
    {
        panel.SetActive(true);
        for (int i = 0; i < ingredientsImgs.Count; i++)
        {
            ingredientsImgs[i].GetComponent<UnityEngine.UI.Image>().sprite = dishPanel.ingredients[i].GetComponent<SpriteRenderer>().sprite;
        } 
    }
}
