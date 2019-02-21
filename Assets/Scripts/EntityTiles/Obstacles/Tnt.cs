using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : Obstacle
{
    public GameObject ExplosionGO;
    public Sprite InactiveSprite,ActivateSprite;

    bool activated;
    readonly float fuseTime;
    float activatedTime, flickerTime;
    bool flicker;

    Tnt()
    {
        flicker = false;
        activated = false;
        fuseTime = 1f;
        EntityName = EntityType.Tnt;
        flickerTime = 0f;
    }



    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            if (flickerTime + 0.5 < Time.time)
            {
                flicker = !flicker;
                if (flicker)
                {
                    GetComponent<SpriteRenderer>().sprite = ActivateSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = InactiveSprite;
                }
                flickerTime = Time.time;
            }


            if (activatedTime + fuseTime < Time.time)
            {
                Explode();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            activated = true;
            activatedTime = Time.time;
        }
    }

    private void Explode()
    {
        // Create the explosion
        Instantiate(ExplosionGO, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
