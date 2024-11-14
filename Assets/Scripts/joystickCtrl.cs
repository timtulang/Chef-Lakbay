using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickCtrl : MonoBehaviour
{

    public Joystick movementJoystick;
    public float playerSpeed;
    private Rigidbody2D rb;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
// Player Control Joystick

        if(movementJoystick.Direction.y != 0){
            rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
        } else {
            rb.velocity = Vector2.zero;
        }

// Player Direction 
    Vector2 direction = movementJoystick.Direction;

    if (direction.magnitude != 0)
    {
        rb.velocity = new Vector2(direction.x * playerSpeed, direction.y * playerSpeed);

        // Calculate the angle in radians and convert it to degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply an offset if the sprite's front is not aligned with the default forward direction
        rb.rotation = angle - 90f; // Adjust the offset angle as necessary
    }
    else
    {
        rb.velocity = Vector2.zero;
    }



    }
}
