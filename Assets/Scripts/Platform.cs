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
    Dictionary<EntityType, int> obstacleCounter = new Dictionary<EntityType, int>(); // The number of obstacles catagorised by the type

    readonly int HEIGHT_OF_PLATFORM = 5;

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
        // Create a list of remaing positions
        List<float> remainingPositions = new List<float>();

        for(float i = -HEIGHT_OF_PLATFORM/2; i < HEIGHT_OF_PLATFORM/2; i+=0.5f)
        {
            remainingPositions.Add(i);
        }

        // Create a list of obstacle templates indices
        // These will be removed if it doesn't pass validation
        List<int> remainingObstacleTemplates = new List<int>();

        for(int i=0; i< availableObstaclesGOs.Count; i++)
        {
            remainingObstacleTemplates.Add(i);
        }

        // The number of times the platform has generated has increased
        timesGenerated++;

        // Calculate the number of obstacles to generate (minimum = 3, maximum = 6)
        obstaclesToAdd = Mathf.Min((int)Mathf.Floor(timesGenerated * 0.5f) + 2, 4);

        // For the number of obstacles to generate
        for (int i =0; i < obstaclesToAdd; i++)
        {
            List<int> removePositions = new List<int>();
            // Validate all obstacles
            foreach(int j in remainingObstacleTemplates)
            {
                if (!ValidateObstacle(availableObstaclesGOs[j].GetComponent<EntityTile>().EntityName))
                {
                    removePositions.Add(j);
                }
            }

            // For each position found
            removePositions.ForEach((int p) =>
            {
                // Remove from remaining positions
                remainingObstacleTemplates.Remove(p);
            });


            // Get a random obstacle from the remaining that have been validated
            int randomObstacle = remainingObstacleTemplates[Random.Range(0, remainingObstacleTemplates.Count)];
            EntityType obstacleName = availableObstaclesGOs[0].GetComponent<EntityTile>().EntityName;



            Vector2 placementPosition = new Vector2();

            // Find the y position
            int pendingPosition = (int)Random.Range(0, remainingPositions.Count);
            placementPosition.y = remainingPositions[pendingPosition];
            remainingPositions.RemoveAt(pendingPosition);

            // Instantiate the obstacle
            GameObject toInstantiate = Instantiate(availableObstaclesGOs[randomObstacle]);

            CavernPostion cavernPostion = (CavernPostion)Random.Range(0, 2); // Pick side
            if (cavernPostion == CavernPostion.Left)
            {
                placementPosition.x = -2;
            }
            else if (cavernPostion == CavernPostion.Right)
            {
                placementPosition.x = 2;
                toInstantiate.GetComponent<SpriteRenderer>().flipX = true;
            }

            // Set the parent to the current platform
            toInstantiate.transform.SetParent(transform);

            // Set the position of the obstacle relative to the platform
            toInstantiate.transform.localPosition = placementPosition;

            // Add the obstacle to the platforms list
            obstaclesGOs.Add(toInstantiate);


            // Add the number of obstacles to 
            if (!obstacleCounter.ContainsKey(obstacleName))
            {
                obstacleCounter.Add(obstacleName, 1);
            }
            else
            {
                obstacleCounter[obstacleName]++;
            }

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

        // Clear the obstacle counter
        obstacleCounter.Clear();

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

    /// <summary>
    /// On the trigger exit 2d.
    /// </summary>
    /// <param name="coll">Coll.</param>
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

    /*
     * Work to do:
     * - Platforms are limited to 1 rockfall
     * - Doesn't solve between 2 platforms though
     * - No more than 3 lasers together
     */
    private bool ValidateObstacle(EntityType obstacleName)
    {
        bool validate = true;

        if (obstacleCounter.ContainsKey(obstacleName))
        {
            if(obstacleName == EntityType.Rockfall && obstacleCounter[obstacleName] > 1)
            {
                validate = false;
            }
            else if (obstacleName == EntityType.Laser && obstacleCounter[obstacleName] > 3)
            {
                validate = false;
            }
        }

        return validate;
    }

    enum CavernPostion
    {
        Left,
        Right
    }
}