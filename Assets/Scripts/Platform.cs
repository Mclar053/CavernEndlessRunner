using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    List<GameObject> obstaclesGOs; // Obstacles on the platform
    List<GameObject> availableObstaclesGOs; // Obstacle templates available to the platform
    int timesGenerated; // Number of times the platform has regenerated
    int obstaclesToAdd; // Remaining number of obstacles to add (used during platfrom generation)

    readonly int HEIGHT_OF_PLATFORM = 10;

    // Start is called before the first frame update
    void Start()
    {
        obstaclesGOs = new List<GameObject>();
        timesGenerated = 0;
        GeneratePlatformTerrain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Generates the platform terrain.
    /// </summary>
    void GeneratePlatformTerrain()
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


            CavernPostion cavernPostion = (CavernPostion)Random.Range(0, 1); // Pick side
            if (cavernPostion == CavernPostion.Left)
            {
                placementPosition.x = -2;
            }
            else if(cavernPostion == CavernPostion.Right)
            {
                placementPosition.x = 2;
            }

            // Figure out where the obstacle goes
            // Instantiate the obstacle
            // Set the parent to this platform

            //GameObject toInstantiate = Instantiate(availableObstaclesGOs[randomObstacle],)
        }
    }

    /// <summary>
    /// Refreshs the platform.
    /// </summary>
    void RefreshPlatform()
    {
        // Remove all obstacles from the platform
        foreach(GameObject obstacle in obstaclesGOs)
        {
            Destroy(obstacle);
        }
        obstaclesGOs.Clear();
    }

    /// <summary>
    /// Resets the game
    /// </summary>
    private void Reset()
    {

    }

    enum CavernPostion
    {
        Left,
        Right
    }
}
