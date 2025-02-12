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
    //Random walk for creating rooms algorithm is stored as a hash set as hash sets store unique values
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

    //Random walk algorithm for creating corridors - using List to access positions in order so can start the next corridor from the previous one's end point
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomDirection();
        var currentPos = startPosition;
        //Store the starting position
        corridor.Add(currentPos);
        for (int i = 0; i< corridorLength;i++)
        {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSpilt, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSpilt);
        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight*2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.min.y, room.min.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
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