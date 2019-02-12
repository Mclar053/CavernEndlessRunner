using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public float lifespan;
    float activatedTime;

    // Start is called before the first frame update
    void Start()
    {
        lifespan = 1f;
        activatedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the laserbeam has lasted longer than it should have
        if (activatedTime + lifespan < Time.time)
        {
            // Destroy the laserbeam
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            coll.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
