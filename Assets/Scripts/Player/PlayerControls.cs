using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{

    public bool isPlayerLeft = true;
    bool moving = false;
    bool dead = false;
    bool switchSides = false;
    float moveTime;

    public GameObject endGameDisplay;
    public Text textObject;
    public Text highScore;
    int score;
    public Text newHighScoreText;

    public AudioClip playerMove;
    public AudioClip playerJump;
    public AudioClip playerDeath1;
    public AudioClip playerDeath2;

    public Animator animator;

    Vector2 newPosition;
    float t;

    Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = new Scoreboard();
        score = 0;
        highScore.text = "Highscore: " + scoreboard.GetScore();
        animator.SetBool("switchSides", false);
    }

    // Update is called once per frame
    void Update()
    {

        // Take the first input given by the user
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (!moving && !dead)
            {
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
                        totalMovement.x = -3.64f;
                        isPlayerLeft = true; // Player is now on the left
                        switchSides = true;
                        animator.SetBool("isJumping", true);
                        moveTime = 0.2f;
                    }
                    else if (isPlayerLeft && !userTouchedLeft)
                    { // If the player is on the left and the user touched the right side
                        totalMovement.x = 3.64f;
                        isPlayerLeft = false; // Player is now on the right
                        switchSides = true;
                        animator.SetBool("isJumping", true);
                        moveTime = 0.2f;
                    }
                    else
                    {
                        animator.SetBool("isClimbing", true);
                        moveTime = 0.05f;
                    }

                    // Set the new position
                    newPosition = new Vector2(transform.position.x + totalMovement.x, transform.position.y + totalMovement.y);

                    // Increase score
                    AddScore(1);

                    moving = true;
                }
            }
        }

        if (!dead && moving)
        {
            t += Time.deltaTime / moveTime;
            transform.position = Vector2.Lerp(transform.position, newPosition, t);
            if (transform.position.Equals(newPosition))
            {
                // Play player moving sounds
                if (switchSides)
                {
                    SoundManager.instance.RandomizePlayerSfx(playerJump);
                    animator.SetBool("isJumping", false);
                }
                else
                {
                    SoundManager.instance.RandomizePlayerSfx(playerMove);
                    animator.SetBool("isClimbing", false);
                }

                moving = false;
                switchSides = false;
                GetComponent<SpriteRenderer>().flipX = !isPlayerLeft;
            }
        }
    }

    public void KillPlayer()
    {
        if (!switchSides & !dead)
        {
            dead = true;

            if (scoreboard.SetScore(score))
            {
                highScore.text = "Highscore: " + scoreboard.GetScore();
                highScore.color = new Color(0, 255, 0);
                newHighScoreText.gameObject.SetActive(true);
            }

            endGameDisplay.SetActive(true);
            SoundManager.instance.RandomizePlayerSfx(playerDeath1, playerDeath2);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().gravityScale = 1;
            AdManager.instance.ShowAdvert();
        }
    }

    public bool IsPlayerDead()
    {
        return dead;
    }

    public void ResetPlayer()
    {
        transform.position = new Vector2(-2, -1);
        dead = false;
        moving = false;
        isPlayerLeft = true;
    }

    public void AddScore(int _points)
    {
        score += _points;
        textObject.text = "Score: " + score;
    }
}
