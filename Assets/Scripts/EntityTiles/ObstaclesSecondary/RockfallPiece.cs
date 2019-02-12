using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockfallPiece : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
