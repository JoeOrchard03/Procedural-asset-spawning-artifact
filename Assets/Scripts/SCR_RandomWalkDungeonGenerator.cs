using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/***************************************************************************************
*    Title: Using Random Walk Algorithm - P5 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 18/12/2020
*    Availability: https://www.youtube.com/watch?v=LnbZLnCXSyI
***************************************************************************************/

public class SCR_RandomWalkDungeonGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int Iterations = 10;
    [SerializeField]
    public int walkLength = 10;
    [SerializeField]
    public bool startRandomlyEachIteration = true;

    /// <summary>
    /// Runs the random walk algorithm for the amount of times set in Iterations and then prints out all the positions that the combined random walk algorithms generate
    /// </summary>
    public void runProcGen()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk();
        foreach (var position in floorPositions) { Debug.Log(position); }
    }

    protected HashSet<Vector2Int> RunRandomWalk()
    {
        var currentPos = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < Iterations; i++)
        {
            var returnData = SCR_PCGAlgorithms.RandomWalk(currentPos, walkLength);
            //Union with adds the return data to floor positions but does not add any duplicates that are already in floorPositions
            floorPositions.UnionWith(returnData);
            if(startRandomlyEachIteration)
            {
                //Gets a random position inside floor positions to start the next random walk iteration from
                currentPos = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
