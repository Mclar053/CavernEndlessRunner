using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius, explodeTime; 
    float createdTime;


    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(2 * radius, 2 * radius, 1);
        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (createdTime + explodeTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
