using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    private Transform Follower;
    public Transform Followed;

    public bool followY;
    public bool followX;
    public float modifierX;
    public float modifierY;


    private void Start()
    {
        Follower = transform;
    }

    void FixedUpdate ()
    {
        Vector3 newFollowerPosition = new Vector3(0, 0, Follower.transform.position.z);

        // If the follower is following the X direction
        if(followX)
        {
            newFollowerPosition.x = Followed.position.x + modifierX;
        }

        // If the follower if following the Y direction
        if(followY)
        {
            newFollowerPosition.y = Followed.position.y + modifierY;
        }

        // Set the follower's position
        Follower.position = newFollowerPosition;
    }
}
