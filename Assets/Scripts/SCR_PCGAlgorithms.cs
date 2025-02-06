using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************************************************************************
*    Title: Random Walk Algorithm - P4 - Unity Procedural Generation of a 2D Dungeon
*    Author: Sunny Valley Studio - Peter https://www.youtube.com/@SunnyValleyStudio
*    Date: 18/12/2020
*    Availability: https://www.youtube.com/watch?v=F_Zc1nvtB0o&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=4
***************************************************************************************/

public static class SCR_PCGAlgorithms
{
    //Random walk algorithm is stored as a hash set as hash sets store unique values
    public static HashSet<Vector2Int> RandomWalk(Vector2Int StartPos, int walkLength)
    {
        HashSet<Vector2Int> returnData = new HashSet<Vector2Int>();

        returnData.Add(StartPos);

        var previousPos = StartPos;

        //Iterates through for every element in walk length, walks a new step in a random diretion and adds it to the return data
        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPos + Direction2D.GetRandomDirection();
            returnData.Add(newPosition);
            previousPos = newPosition;
        }
        return returnData;
    }
}

//Class for access to simple direction data
public static class Direction2D
{
    //Cardinal directions
    public static List<Vector2Int> directionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), //DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static Vector2Int GetRandomDirection()
    {
        return directionsList[Random.Range(0, directionsList.Count)];
    }
}