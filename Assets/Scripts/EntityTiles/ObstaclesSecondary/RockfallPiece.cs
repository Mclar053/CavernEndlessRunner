using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockfallPiece : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    public Vector2 originalPosition;

    void Start()
    {
        int randomSprite = Random.Range(0, sprites.Count);
        bool flip = Random.Range(0, 10) > 5;
        GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];
        GetComponent<SpriteRenderer>().flipX = flip;
        originalPosition = new Vector2(transform.position.x, transform.position.y);
    }

    private void Update()
    {
        // If the rockpiece falls 20 tiles then clean itself up
        if(originalPosition.y - transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().KillPlayer();
            Destroy(gameObject);
        }
    }
}
