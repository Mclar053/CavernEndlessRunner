using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Obstacle
{
    public GameObject LaserBeamGO;
    public List<Sprite> LaserSprites;
    public AudioClip laserShot;

    public float chargeTime;
    float lastActivated, delay;
    bool playedSound;

    Laser()
    {
        EntityName = EntityType.Laser;
    }

    private void Start()
    {
        chargeTime = 3f;
        delay = Random.Range(0f, 1.5f);
        lastActivated = delay;
        playedSound = false;
    }

    // Update is called once per frame
    void Update()
    {

        // To combat Unity for Android's 0.5s sound delay, activating the sound 0.5s before event
        //if (lastActivated + chargeTime - 0.5f < Time.time && !playedSound)
        //{
        //    float playerYPos = GameObject.FindGameObjectWithTag("Player").transform.position.y;

        //    if( transform.position.y - playerYPos < 5 && transform.position.y - playerYPos > -3)
        //    {
        //        SoundManager.instance.ChangeSoundVolumeWithMultiplier(Mathf.Abs(1 / (playerYPos - transform.position.y + 1)));
        //        SoundManager.instance.RandomizeSfx(laserShot);
        //        playedSound = true;
        //    }
        //}

        // Check if the laser has been fire within the last x number of seconds (charge time)
        if (lastActivated + chargeTime < Time.time)
        {
            float playerYPos = GameObject.FindGameObjectWithTag("Player").transform.position.y;

            if (transform.position.y - playerYPos < 5 && transform.position.y - playerYPos > -3)
            {
                SoundManager.instance.ChangeSoundVolumeWithMultiplier(Mathf.Abs(1 / (playerYPos - transform.position.y + 1)));
                SoundManager.instance.RandomizeSfx(laserShot);
            }

            // Create the laser beam
            Instantiate(LaserBeamGO, new Vector2(0, transform.position.y), Quaternion.identity);
            lastActivated = Time.time + delay;
            //playedSound = false;
            GetComponent<SpriteRenderer>().sprite = LaserSprites[0];
        }
        else if (lastActivated + (chargeTime * 3 / 4) < Time.time)
        {
            GetComponent<SpriteRenderer>().sprite = LaserSprites[3];
        }
        else if (lastActivated + (chargeTime / 2) < Time.time)
        {
            GetComponent<SpriteRenderer>().sprite = LaserSprites[2];
        }
        else if (lastActivated + chargeTime / 4 < Time.time)
        {
            GetComponent<SpriteRenderer>().sprite = LaserSprites[1];
        }
    }
}
