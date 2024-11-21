using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public PlayerGrab pg;
    public OrderManager om;
    public GameObject checkoutArea;
    public Text scoreTxt;
    public int score = 0, index = -1;
    private bool orderCorrect = false;
    private void Update()
    {
        orderCorrect = false;
        if (pg.CurrentItem != null)
        {
            if (checkoutArea.GetComponent<Collider2D>().IsTouching(pg.transform.GetChild(0).GetComponent<Collider2D>()) && om.FindOrderIndex(pg.CurrentItem) != -1)
            {
                orderCorrect = true;
                Debug.Log(orderCorrect);
            }
        }
    }
    public void OnSpawnButtonPress()
    {
        if (orderCorrect)
        {
            om.RemoveOrder(pg.CurrentItem);
            Destroy(pg.CurrentItem);
            score += 250;
            scoreTxt.text = score.ToString();
        }
    }
}
