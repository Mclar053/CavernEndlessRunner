using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : Obstacle
{

    public GameObject PlayerGO;
    float speed; // 5 Seconds

    // Start is called before the first frame update
    void Start()
    {
        speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(PlayerGO.transform.position.y - transform.position.y) >= 10)
        {
            transform.position = new Vector2(0, PlayerGO.transform.position.y - 10);
        }

        transform.Translate(Vector2.up * Time.deltaTime * speed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
