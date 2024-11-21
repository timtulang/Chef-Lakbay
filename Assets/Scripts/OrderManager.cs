using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public TimerBarManager timerBar;
    public ScoreManager scoreManager;
    public Canvas canvas;
    public Text orderName;
    private Queue<GameObject> orders = new Queue<GameObject>();
    public GameObject order;
    public List<DishIngredientMapping> map;
    public GameObject[] orderBtn;
    public UnityEngine.UI.Image finalDishImg;
    public UnityEngine.UI.Image[] images;
    public GameObject panel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RemoveOrder(order);
        }
    }

    private void Start()
    {
        StartCoroutine(generateOrders());
    }
    public void OpenOrder(orderBtnIdentifier order)
    {
        panel.SetActive(true);
        orderName.text = order.dish.name;
        for (int i = 0; i < order.dish.ingredients.Length; i++)
        {
            if (order.dish.ingredients[i] != null)
            {
                images[i].gameObject.SetActive(true);
                finalDishImg.gameObject.SetActive(true);
                images[i].sprite = order.dish.ingredients[i].GetComponent<SpriteRenderer>().sprite; //line 24
                images[i].preserveAspect = true;
                finalDishImg.sprite = order.dish.finalDish.GetComponent<SpriteRenderer>().sprite;
            }
            else
                images[i].gameObject.SetActive(false);
        }
    }
    public void CloseOrder()
    {
        panel.SetActive(false);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
            images[i].sprite = null;
            finalDishImg.gameObject.SetActive(false);
            finalDishImg.sprite = null;
        }
    }
    public int FindOrderIndex(GameObject dish)
    {
        int index = -1; // Default to -1 (not found)
        int currentIndex = 0;

        foreach (GameObject order in orders)
        {
            if (order.GetComponent<CraftingItems>().itemName == dish.GetComponent<CraftingItems>().itemName)
            {
                index = currentIndex;
                break;
            }
            currentIndex++;
        }

        return index; // Return the index if found, or -1 if not found
    }
    public void RemoveOrder(GameObject dish)
    {
        // Temporary queue to hold updated orders
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        bool removed = false;

        while (orders.Count > 0)
        {
            GameObject currentOrder = orders.Dequeue();

            if (!removed && currentOrder.GetComponent<CraftingItems>().itemName == dish.GetComponent<CraftingItems>().itemName)
            {
                // Mark as removed and handle the UI
                removed = true;

                // Update corresponding UI button
                for (int i = 0; i < orderBtn.Length; i++)
                {
                    if (orderBtn[i].activeSelf &&
                        orderBtn[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite.name == currentOrder.GetComponent<SpriteRenderer>().sprite.name)
                    {
                        orderBtn[i].SetActive(false);
                        orderBtn[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = null;
                        break;
                    }
                }
            }
            else
            {
                // Add back non-matching orders
                tempQueue.Enqueue(currentOrder);
            }
        }

        // Replace the original queue with the updated one
        orders = tempQueue;
    }
    public IEnumerator RemoveOrderAfterDelay(GameObject dish, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Ensure the order still exists before removing it
        int index = FindOrderIndex(dish);
        if (index != -1)
        {
            RemoveOrder(dish);
            scoreManager.score -= 100;
        }
    }
    IEnumerator generateOrders()
    {
        while (true)
        {
            DishIngredientMapping order = map[Random.Range(0, map.Count)];
            for (int i = 0; i < orderBtn.Length; i++)
            {
                if (!orderBtn[i].activeSelf)
                {
                    orders.Enqueue(order.finalDish);
                    orderBtn[i].GetComponent<TimerBar>().InitializeTimer(70f);
                    orderBtn[i].SetActive(true);
                    orderBtn[i].GetComponent<orderBtnIdentifier>().dish = order;
                    orderBtn[i].GetComponent<orderBtnIdentifier>().dishName = order.finalDish.name;
                    orderBtn[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = order.finalDish.GetComponent<SpriteRenderer>().sprite;
                    StartCoroutine(RemoveOrderAfterDelay(order.finalDish, 70f));
                    break;
                }
            }
            yield return new WaitForSeconds(Mathf.FloorToInt(Random.Range(10, 15)));
        }
    }
}