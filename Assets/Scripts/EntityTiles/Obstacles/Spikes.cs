using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle
{

    Spikes()
    {
        EntityName = EntityType.Spikes;
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<PlayerControls>().KillPlayer();
        }
    }
}
