using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public GameObject ExplosionGO;

    bool activated;
    float fuseTime;
    float activatedTime;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        fuseTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, Mathf.Lerp(0, 255, Time.time - (activatedTime + fuseTime)), Mathf.Lerp(0, 255, Time.time - (activatedTime + fuseTime)));
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
