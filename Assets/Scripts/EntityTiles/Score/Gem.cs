using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : EntityTile
{
    private int score;

    Gem()
    {
        score = 10;
        EntityName = EntityType.Gem;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.GetComponent<PlayerControls>().AddScore(score);
            Destroy(gameObject);
        }
    }
}
