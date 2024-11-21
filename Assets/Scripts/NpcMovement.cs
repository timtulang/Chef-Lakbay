using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public GameObject pointA; // Spawn Point
    public GameObject pointB; // Destination Point
    private Transform currentTarget; // Current target point
    public float speed = 7.0f; // Movement speed

    public float waitTimeAtPointB = 2.0f; // Time to wait at Point B before returning

    private bool isWaiting = false; // Track whether the NPC is waiting

    void Start()
    {
        currentTarget = pointB.transform; // Start moving towards Point B
    }

    void Update()
    {
        if (!isWaiting)
        {
            MoveToTarget();
        }
    }

    private void MoveToTarget()
    {
        // Move towards the current target
        Vector2 direction = (currentTarget.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Check if the NPC has arrived at the target
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            if (currentTarget == pointB.transform)
            {
                // Reached Point B, start waiting
                StartCoroutine(WaitAtPointB());
            }
            else
            {
                // Reached Point A, stop movement
                enabled = false; // Disable script if no further action is needed
            }
        }
    }

    private IEnumerator WaitAtPointB()
    {
        isWaiting = true; // Set waiting state
        yield return new WaitForSeconds(waitTimeAtPointB); // Wait for the specified time
        currentTarget = pointA.transform; // Set target back to Point A
        isWaiting = false; // Resume movement
    }
}