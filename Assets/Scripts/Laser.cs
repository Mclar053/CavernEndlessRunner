using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject LaserBeamGO;

    public float chargeTime;
    float lastActivated;

    // Start is called before the first frame update
    void Start()
    {
        chargeTime = 3f;
        lastActivated = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the laser has been fire within the last x number of seconds (charge time)
        if (lastActivated + chargeTime < Time.time)
        {
            // Create the laser beam
            Instantiate(LaserBeamGO, new Vector2(0, transform.position.y), Quaternion.identity);
            lastActivated = Time.time;
        }
    }
}
