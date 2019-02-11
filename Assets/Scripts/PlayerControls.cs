using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

    public bool isPlayerLeft = true;
    bool moving = false;
    bool dead = false;
    bool switchSides = false;

    public Text textObject;
    int score;

    Vector2 newPosition;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetMouseButtonDown(0)) {
        //     Debug.Log("1");
        // }

        if (dead)
        {
            GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255,255,255);
        }

        if (!moving)
        {
            // Take the first input given by the user
            Touch touch = Input.touches[0];

            // Check the user has tapped the screen
            if (touch.phase == TouchPhase.Began)
            {
                t = 0;
                // As the user has tapped the screen, they will move up one tile
                Vector2 totalMovement = new Vector2(0, 0);

                totalMovement.y += 0.5f;

                // Check if they move left or right
                bool userTouchedLeft = touch.position.x < Screen.width / 2;

                if (!isPlayerLeft && userTouchedLeft)
                { // If the player is on the right and the user touched the left side
                    totalMovement.x = -4f;
                    isPlayerLeft = true; // Player is now on the left
                    switchSides = true;
                }
                else if (isPlayerLeft && !userTouchedLeft)
                { // If the player is on the left and the user touched the right side
                    totalMovement.x = 4f;
                    isPlayerLeft = false; // Player is now on the right
                    switchSides = true;
                }

                // Set the new position
                newPosition = new Vector2(transform.position.x + totalMovement.x, transform.position.y + totalMovement.y);

                // Increase score
                score++;
                textObject.text = "Score: " + score;

                moving = true;

                if (touch.position.y > 3f * Screen.height/4f)
                {
                    SceneManager.LoadScene("SampleScene");
                }

            }
        }
        else
        {
            t += Time.deltaTime / 0.1f;
            transform.position = Vector2.Lerp(transform.position, newPosition, t);
            if (transform.position.Equals(newPosition))
            {
                moving = false;
                switchSides = false;
            }
        }
    }

    public void KillPlayer()
    {
        if (!switchSides)
        {
            dead = true;
        }
    }

    public void ResetPlayer()
    {
        transform.position = new Vector2(-2, -1);
        dead = false;
        moving = false;
        isPlayerLeft = true;
    }
}
