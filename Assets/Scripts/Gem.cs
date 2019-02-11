using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private int score;

    private void Start()
    {
        score = 10;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.GetComponent<PlayerControls>().addScore(score);
            Destroy(gameObject);
        }
    }
}
