using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public bool isPlayerLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)) {
        //     Debug.Log("1");
        // }

        foreach(Touch touch in Input.touches) {

            // Check the user has tapped the screen
            if (touch.phase == TouchPhase.Began) {
                // As the user has tapped the screen, they will move up one tile
                Vector2 totalMovement = new Vector2 (0,1);

                // Check if they move left or right
                bool userTouchedLeft = touch.position.x < Screen.width/2;

                if(!isPlayerLeft && userTouchedLeft) { // If the player is on the right and the user touched the left side
                    totalMovement.x = -3.5f;
                    isPlayerLeft = true; // Player is now on the left
                }
                else if(isPlayerLeft && !userTouchedLeft) { // If the player is on the left and the user touched the right side
                    totalMovement.x = 3.5f;
                    isPlayerLeft = false; // Player is now on the right
                }

                // Set the new position
                transform.position = new Vector2(transform.position.x + totalMovement.x, transform.position.y + totalMovement.y);
                // Debug.Log("First finger entered!");
            }
        }
    }
}
