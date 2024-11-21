using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public PlayerGrab pg;
    public OrderManager om;
    public GameObject checkoutArea;
    private int score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckoutArea"))
            om.RemoveOrder(pg.CurrentItem);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
