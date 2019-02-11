using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    /* PUBLIC */
    public List<GameObject> availableObstaclesGOs; // Obstacle templates available to the platform

    /* PRIVATE */
    List<GameObject> obstaclesGOs; // Obstacles on the platform
    int timesGenerated; // Number of times the platform has regenerated
    int obstaclesToAdd; // Remaining number of obstacles to add (used during platfrom generation)

    readonly int HEIGHT_OF_PLATFORM = 10;

    // Start is called before the first frame update
    private void Start()
    {
        obstaclesGOs = new List<GameObject>();
        timesGenerated = 0;
        RefreshPlatform();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    /// <summary>
    /// Generates the platform terrain.
    /// </summary>
    private void GeneratePlatformTerrain()
    {
        // Create a list of taken positions
        List<int> takenPositions = new List<int>();

        List<int> remainingPositions = new List<int>();

        // Fill the remaining positions
        for(int i = -HEIGHT_OF_PLATFORM/2; i < HEIGHT_OF_PLATFORM/2; i++)
        {
            remainingPositions.Add(i);
        }

        timesGenerated++;
        obstaclesToAdd = Mathf.Min((int)Mathf.Floor(timesGenerated * 0.5f) + 2, 6);

        // For the number of obstacles to generate
        for (int i =0; i < obstaclesToAdd; i++)
        {
            int randomObstacle = Random.Range(0, availableObstaclesGOs.Count);

            Vector2 placementPosition = new Vector2();

            // Find the y position
            int pendingPosition = (int)Random.Range(0, remainingPositions.Count);
            placementPosition.y = remainingPositions[pendingPosition];
            remainingPositions.RemoveAt(pendingPosition);


            CavernPostion cavernPostion = (CavernPostion)Random.Range(0, 2); // Pick side
            if (cavernPostion == CavernPostion.Left)
            {
                placementPosition.x = -2;
            }
            else if(cavernPostion == CavernPostion.Right)
            {
                placementPosition.x = 2;
            }

            /*
             * Work to do:
             * - Platforms are limited to 1 rockfall
             * - Doesn't solve between 2 platforms though
             * - No more than 3 lasers together
             */

            // Instantiate the obstacle
            GameObject toInstantiate = Instantiate(availableObstaclesGOs[randomObstacle]);

            // Set the parent to the current platform
            toInstantiate.transform.SetParent(transform);

            // Set the position of the obstacle relative to the platform
            toInstantiate.transform.localPosition = placementPosition;

            // Add the obstacle to the platforms list
            obstaclesGOs.Add(toInstantiate);
        }
    }

    /// <summary>
    /// Refreshs the platform.
    /// </summary>
    private void RefreshPlatform()
    {
        // Remove all obstacles from the platform
        foreach(GameObject obstacle in obstaclesGOs)
        {
            Destroy(obstacle);
        }
        obstaclesGOs.Clear();

        GeneratePlatformTerrain();
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    private void Reset()
    {
        timesGenerated = 0;
        RefreshPlatform();
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Vector2 newPosition = transform.position;
            newPosition.y += HEIGHT_OF_PLATFORM * 3;
            transform.position = newPosition;
            RefreshPlatform();
        }
    }

    enum CavernPostion
    {
        Left,
        Right
    }
}
