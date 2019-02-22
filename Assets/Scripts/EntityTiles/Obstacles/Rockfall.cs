using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockfall : Obstacle
{
    public GameObject rockfallPieceGO;
    public Sprite RockfallCrackSprite;
    bool cracked;

    float crackTime = 0.5f;
    float timeActivated;

    Rockfall()
    {
        EntityName = EntityType.Rockfall;
    }

    private void Start()
    {
        cracked = false;
    }

    private void Update()
    {
        if (cracked && timeActivated + crackTime < Time.time)
        {
            // Create 3 rocks at the position of the parent but scattered 
            for (int i = 0; i < 3; i++)
            {
                GameObject newRockfallPiece = Instantiate(rockfallPieceGO, new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0f)), Quaternion.identity);
            }

            // The rocks have fallen
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Trigger only when the player enters the box collider and only if the rocks have not fallen
        if(coll.tag == "Player" && !cracked)
        {
            timeActivated = Time.time;
            cracked = true;
            GetComponent<SpriteRenderer>().sprite = RockfallCrackSprite;
        }
    }
}
