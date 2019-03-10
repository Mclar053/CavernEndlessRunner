using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : EntityTile
{
    private int score;
    public AudioClip gemSound;

    Gem()
    {
        score = 10;
        EntityName = EntityType.Gem;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            SoundManager.instance.ChangeSoundVolumeWithMultiplier(1);
            SoundManager.instance.PlaySingle(gemSound);
            coll.GetComponent<PlayerControls>().AddScore(score);
            Destroy(gameObject);
        }
    }
}
