using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockfallPiece : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    void Start()
    {
        int randomSprite = Random.Range(0, sprites.Count);
        bool flip = Random.Range(0, 10) > 5;
        GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];
        GetComponent<SpriteRenderer>().flipX = flip;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
